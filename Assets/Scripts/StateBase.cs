using Cysharp.Threading.Tasks;
using UnityEngine;

public abstract class StateBase : IGameState
{
    protected StateOfGame nextState;

    public StateBase(StateOfGame next)
    {
        nextState = next;
    }

    public abstract UniTask Enter();
    public abstract UniTask Doing();
    public abstract UniTask Exit();

    public StateOfGame NextState()
    {
        return nextState;
    }
}