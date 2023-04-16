using System;

namespace Kernel.StateMachine
{
    public class StateMachine : IStateMachine, IDisposable
    {
        private readonly IStateFactory _stateFactory;
        
        private IState _currentState;
        
        public StateMachine(IStateFactory stateFactory)
        {
            _stateFactory = stateFactory;
        }
        
        public void EnterTo<TState>() where TState : IState
        {
            _currentState?.Exit();
            _currentState = _stateFactory.Create<TState>();
            _currentState.Enter();
        }

        public void Dispose()
        {
            _currentState?.Exit();
        }
    }
}