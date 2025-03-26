using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLoop : MonoBehaviour, IGameLoop
{
    [SerializeField] private ClientMono clientMono;
    [SerializeField] private PrintTable printTable;
    [SerializeField] private TextMeshProUGUI statusText;
    private TeaTime _createGame, _turnPlayer1, _turnPlayer2, _endGame, _printBoard;
    private bool _isTurn;
    private GameStateMachine _gameStateMachine;

    void Start()
    {
        _gameStateMachine = new GameStateMachine();
        _gameStateMachine.AddInitialState(StateOfGame.CREATE_GAME, new CreateGameState(StateOfGame.PRINT_BOARD, this));
        _gameStateMachine.AddState(StateOfGame.PRINT_BOARD, new PrintBoardState(StateOfGame.TURN_PLAYER_1, this));
        _gameStateMachine.AddState(StateOfGame.TURN_PLAYER_1, new TurnPlayerOneState(StateOfGame.PRINT_BOARD, this));
        _gameStateMachine.AddState(StateOfGame.TURN_PLAYER_2, new TurnPlayerTwoState(StateOfGame.PRINT_BOARD, this));
        _gameStateMachine.AddState(StateOfGame.END_GAME, new EndGameState(StateOfGame.EXIT, this));

        printTable.Config(this);
        /*
        _createGame = this.tt().Pause().Add(async () => { await clientMono.CreateGame(); })
            .Add(() => { _printBoard.Play(); });

        _turnPlayer1 = this.tt().Pause().Add(async () =>
        {
            await clientMono.GetGameState();
            await clientMono.GetValidMoves();
        }).Wait(() => _isTurn).Add(() => { _printBoard.Play(); });

        _turnPlayer2 = this.tt().Pause().Add(async () =>
        {
            await clientMono.GetGameState();
            await clientMono.GetValidMoves();
        }).Wait(() => _isTurn).Add(() => { _printBoard.Play(); });

        _endGame = this.tt().Pause().Add(async () => { await clientMono.GetGameStatus(); });

        _printBoard = this.tt().Pause().Add(async () =>
        {
            await clientMono.GetGameState();
            printTable.GenerateBoard(clientMono.Game.board);
            statusText.text = $"Turno de {clientMono.Game.currentPlayer}";
        }).Add(() =>
        {
            _isTurn = false;
            if (clientMono.Game.currentPlayer == "black")
            {
                _turnPlayer1.Play();
            }
            else
            {
                _turnPlayer2.Play();
            }
        });

        _createGame.Play();
        */

        _ = StartStateMachine();
    }

    private bool _whileTrue = true;
    private MoveRequest _move;

    private async Awaitable StartStateMachine()
    {
        var gameState = _gameStateMachine.GetInitialState();
        while (_whileTrue)
        {
            await gameState.Enter();
            await gameState.Doing();
            await gameState.Exit();
            var nextState = gameState.NextState();
            if (nextState == StateOfGame.EXIT)
            {
                break;
            }

            gameState = _gameStateMachine.GetState(nextState);
        }

        SceneManager.LoadScene(0);
    }

    public void OnTileClicked(int x, int y)
    {
        if (_isTurn) return;
        _move = new MoveRequest(x, y);
        Debug.Log($"Move: {_move.x}, {_move.y}");
        _isTurn = true;
    }

    public ClientMono GetClientMono()
    {
        return clientMono;
    }

    public PrintTable GetPrintTable()
    {
        return printTable;
    }

    public void SetStatusText(string text)
    {
        statusText.text = text;
    }

    public void SetIsTurn(bool isTurn)
    {
        _isTurn = isTurn;
    }

    public bool GetIsTurn()
    {
        return _isTurn;
    }

    public MoveRequest GetMoves()
    {
        return _move;
    }
}