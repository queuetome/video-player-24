namespace Kernel.StateMachine
{
    public interface IStateFactory
    {
        TState Create<TState>() where TState : IState;
    }
}