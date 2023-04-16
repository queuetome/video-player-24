using UnityEngine;
using Zenject;

namespace Kernel.Scene
{
    public class SceneInstaller : MonoInstaller
    {
        [SerializeField] private Mediator _mediator;
        
        public override void InstallBindings()
        {
            BindMediator();
        }

        private void BindMediator() => Container.BindInterfacesTo<Mediator>().FromInstance(_mediator).AsSingle();
    }
}