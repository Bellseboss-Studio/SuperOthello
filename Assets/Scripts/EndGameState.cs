using Cysharp.Threading.Tasks;
using UnityEngine;

public class EndGameState : StateBaseGameLoop
{
    public EndGameState(StateOfGame next, IGameLoop mediator) : base(next, mediator)
    {
    }

    public override async UniTask Enter()
    {
        Debug.Log($"State: {GetType()}");
        await gameLoop.GetClientMono().GetGameStatus();
    }

    public override async UniTask Doing()
    {
        await UniTask.WaitForSeconds(0.5f);
        //Debug.Log($"Is wana to exit {gameLoop.WantExit}");
    }

    public override async UniTask Exit()
    {
        await UniTask.NextFrame();
    }
}