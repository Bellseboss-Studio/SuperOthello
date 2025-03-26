using Cysharp.Threading.Tasks;
using UnityEngine;

public class CreateGameState : StateBaseGameLoop
{
    public CreateGameState(StateOfGame next, IGameLoop mediator) : base(next, mediator)
    {
    }

    public override async UniTask Enter()
    {
        Debug.Log($"State: {GetType()}");
        await UniTask.NextFrame();
    }

    public override async UniTask Doing()
    {
        await gameLoop.GetClientMono().CreateGame();
    }

    public override async UniTask Exit()
    {
        await UniTask.NextFrame();
    }
}