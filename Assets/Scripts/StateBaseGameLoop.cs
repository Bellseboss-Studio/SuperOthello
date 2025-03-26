public abstract class StateBaseGameLoop : StateBase
{
    protected IGameLoop gameLoop;
    protected StateBaseGameLoop(StateOfGame next, IGameLoop mediator) : base(next)
    {
        gameLoop = mediator;
    }
}