using System;
using System.Collections.Generic;

namespace Infrastructure.States
{
    public class StateMachine
    {
        private IState _currentState;
        private readonly Dictionary<Type, IState> _states;

        public StateMachine(SceneLoader sceneLoader)
        {
            _states = new Dictionary<Type, IState>()
            {
                [typeof(EntryPointState)] = new EntryPointState(sceneLoader)
            };
        }

        public void Enter<TState>() where TState : IState
        {
            _currentState?.Exit();
            IState state = _states[typeof(TState)];
            _currentState = state;
            state.Enter();
        }
    }
}
