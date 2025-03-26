using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class GameStateMachine
{
    private StateOfGame _currentState;
    private readonly Dictionary<StateOfGame, IGameState> _states;

    public GameStateMachine()
    {
        _states = new Dictionary<StateOfGame, IGameState>();
    }

    public void AddInitialState(StateOfGame index, IGameState state)
    {
        _currentState = index;
        AddState(index, state);
    }

    public void AddState(StateOfGame index, IGameState state)
    {
        _states.Add(index, state);
    }

    public IGameState GetState(StateOfGame index)
    {
        Assert.IsTrue(_states.ContainsKey(index), $"State with id {index} do not exit");
        return _states[index];
    }

    public IGameState GetInitialState()
    {
        return GetState(_currentState);
    }
}