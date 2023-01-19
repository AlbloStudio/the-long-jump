using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine.Events;

public class StateMachine<StateType>
{

    public StateType CurrentState { get; private set; }

    public event PropertyChangedEventHandler OnStateChange;

    public UnityEvent<StateType, StateType> StateChanged { get; private set; } = new();

    public StateMachine(StateType initialState)
    {
        CurrentState = initialState;
    }

    public bool IsInState(params StateType[] states)
    {
        return new List<StateType>(states).Contains(CurrentState);
    }

    public void ChangeState(StateType newState)
    {
        StateType previousState = CurrentState;
        CurrentState = newState;

        StateChanged.Invoke(newState, previousState);
    }
}