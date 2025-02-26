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
        _clientHttpFacade = new ClientHttpFacade("https://backend.superothello.peryloth.com");
    }

    public async UniTask CreateGame()
    {
        try
        {
            _game = await _clientHttpFacade.CreateGame();
            Debug.Log($"Game: {_game.board}");
        }
        catch (Exception e)
        {
            Debug.LogError(e.Message);
        }
    }

    public async UniTask GetGameState()
    {
        GetGameResponse game = await _clientHttpFacade.GetGameState(_game.id);
        _game.board = game.board;
        _game.currentPlayer = game.currentPlayer;
        Debug.Log($"Game: {game.board}");
    }

    public async UniTask GetValidMoves()
    {
        ValidMoveResponse validMoves = await _clientHttpFacade.GetValidMoves(_game.id);
        Debug.Log($"Valid moves: {validMoves.validMoves.Length}");
    }

    public async void MakeMove(int x, int y)
    {
        MoveRequest move = new MoveRequest(x, y);
        MoveResponse response = await _clientHttpFacade.MakeMove(_game.id, move);
        Debug.Log($"Move: {response.status}");
    }

    public async UniTask GetGameStatus()
    {
        StatusGameResponse status = await _clientHttpFacade.GetGameStatus(_game.id);
        Debug.Log($"Status: {status.status}");
    }
}