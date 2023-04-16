namespace Kernel.StateMachine
{
    public interface IStateMachine
    {
        void EnterTo<TState>() where TState : IState;
    }
}