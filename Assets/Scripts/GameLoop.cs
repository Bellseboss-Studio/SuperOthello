using TMPro;
using UnityEngine;

public class GameLoop : MonoBehaviour
{
    [SerializeField] private ClientMono clientMono;
    [SerializeField] private PrintTable printTable;
    [SerializeField] private TextMeshProUGUI statusText;
    private TeaTime _createGame, _turnPlayer1, _turnPlayer2, _endGame, _printBoard;
    private bool _isTurn;

    async void Start()
    {
        printTable.Config(this);
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
    }

    public void OnTileClicked(int x, int y)
    {
        if (_isTurn) return;
        clientMono.MakeMove(x, y);
        _isTurn = true;
    }
}