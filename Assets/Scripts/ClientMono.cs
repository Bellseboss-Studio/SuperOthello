using System;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;

public class ClientMono : MonoBehaviour
{
    [SerializeField] private TMP_InputField x, y;
    private ClientHttpFacade _clientHttpFacade;
    private CreateGameResponse _game;
    public CreateGameResponse Game => _game;

    private void Awake()
    {
        _clientHttpFacade = new ClientHttpFacade("https://backend.othello.bellseboss.com");
        //_clientHttpFacade = new ClientHttpFacade("http://localhost:3000");
    }

    public async UniTask CreateGame()
    {
        try
        {
            _game = await _clientHttpFacade.CreateGame();
            //Debug.Log($"Game: {_game.board}");
        }
        catch (Exception e)
        {
            Debug.LogError(e.Message);
        }
    }

    public async UniTask GetGameState()
    {
        //Debug.Log($"Game: 0");
        GetGameResponse game = await _clientHttpFacade.GetGameState(_game.id);
        //Debug.Log($"Game: 7");
        //Debug.Log($"Game: {game.board}");
        _game.board = game.board;
        _game.currentPlayer = game.currentPlayer;
        //Debug.Log($"Game: {game.board}");
    }

    public async UniTask GetValidMoves()
    {
        ValidMoveResponse validMoves = await _clientHttpFacade.GetValidMoves(_game.id);
        Debug.Log($"Valid moves: {validMoves.validMoves.Length}");
    }

    public async UniTask GetGameStatus()
    {
        StatusGameResponse status = await _clientHttpFacade.GetGameStatus(_game.id);
        Debug.Log($"Status: {status.status}");
    }

    public async UniTask MakeMove(MoveRequest getMoves)
    {
        MoveResponse response = await _clientHttpFacade.MakeMove(_game.id, getMoves);
        Debug.Log($"Move: {response.status}");
    }
}