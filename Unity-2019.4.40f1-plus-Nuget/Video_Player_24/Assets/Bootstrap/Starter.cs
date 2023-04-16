using Kernel;
using Kernel.StateMachine;
using Zenject;

namespace Bootstrap
{
    public class Starter : IInitializable
    {
        private readonly IStateMachine _stateMachine;
        
        public Starter(IStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }
        
        public void Initialize()
        {
            _stateMachine.EnterTo<BootstrapState>();
        }
    }
}