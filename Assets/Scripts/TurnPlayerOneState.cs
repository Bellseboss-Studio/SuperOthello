using Cysharp.Threading.Tasks;
using UnityEngine;

public class TurnPlayerOneState : StateBaseGameLoop
{
    public TurnPlayerOneState(StateOfGame next, IGameLoop mediator) : base(next, mediator)
    {
    }

    public override async UniTask Enter()
    {
        Debug.Log($"State: {GetType()}");
        await gameLoop.GetClientMono().GetGameState();
        await gameLoop.GetClientMono().GetValidMoves();
    }

    public override async UniTask Doing()
    {
        while (true)
        {
            await UniTask.WaitForSeconds(0.5f);
            if (gameLoop.GetIsTurn())
            {
                await gameLoop.GetClientMono().MakeMove(gameLoop.GetMoves());
                break;
            }
        }
    }

    public override async UniTask Exit()
    {
        await UniTask.NextFrame();
    }
}