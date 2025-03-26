using Cysharp.Threading.Tasks;
using UnityEngine;

public class PrintBoardState : StateBaseGameLoop
{
    private CreateGameResponse game;

    public PrintBoardState(StateOfGame next, IGameLoop mediator) : base(next, mediator)
    {
    }

    public override async UniTask Enter()
    {
        Debug.Log($"State: {GetType()}");
        //Debug.Log($"Save Board");
        await gameLoop.GetClientMono().GetGameState();
        game = gameLoop.GetClientMono().Game;
        //Debug.Log($"Save Board");
    }

    public override async UniTask Doing()
    {
        await UniTask.NextFrame();
        gameLoop.SetStatusText($"Printing board...");
        gameLoop.GetPrintTable().GenerateBoard(game.board);
        gameLoop.SetStatusText($"Turno de {game.currentPlayer}");
    }

    public override async UniTask Exit()
    {
        await UniTask.NextFrame();
        gameLoop.SetIsTurn(false);
        if (gameLoop.GetClientMono().Game.currentPlayer == "black")
        {
            nextState = StateOfGame.TURN_PLAYER_1;
        }
        else
        {
            nextState = StateOfGame.TURN_PLAYER_2;
        }
    }
}