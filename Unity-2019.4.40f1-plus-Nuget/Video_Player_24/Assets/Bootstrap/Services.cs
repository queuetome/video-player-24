using Kernel;
using Kernel.Data;
using Kernel.Logger;
using Kernel.Scene;
using Kernel.Server;
using Kernel.StateMachine;
using Kernel.Videos;
using Kernel.Videos.Updater;
using UnityEditor.VersionControl;
using Zenject;

namespace Bootstrap
{
    public class Services : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindStarter();
            BindLogger();
            BindStateMachine();
            BindSceneLoader();
            BindStaticData();
            BindVideoFactory();
            BindVideoProvider();
            BindVideoUpdater();
        }
        
        private void BindStarter() => Container.BindInterfacesTo<Starter>().AsSingle();
        private void BindLogger() => Container.BindInterfacesTo<DebugLogger>().AsSingle();
        private void BindStateMachine() =>
            Container
                .BindInterfacesTo<StateMachine>()
                .FromSubContainerResolve()
                .ByInstaller<StateMachineInstaller>()
                .AsSingle();
        private void BindSceneLoader() => Container.BindInterfacesTo<SceneLoader>().AsSingle();
        private void BindStaticData() => Container.BindInterfacesTo<StaticData>().AsSingle();
        private void BindVideoFactory() => Container.BindInterfacesTo<VideoFactory>().AsSingle();
        private void BindVideoProvider() => Container.BindInterfacesTo<VideoProvider>().AsSingle();
        private void BindVideoUpdater() =>
            Container
                .BindInterfacesTo<VideoUpdater>()
                .FromSubContainerResolve()
                .ByInstaller<VideoUpdaterInstaller>()
                .AsSingle();
    }

    public class StateMachineInstaller : Installer<StateMachineInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<StateMachine>().AsSingle();
            Container.BindInterfacesTo<StateFactory>().AsSingle();
        }
    }

    public class VideoUpdaterInstaller : Installer<VideoUpdaterInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<VideoUpdater>().AsSingle();
            Container.BindInterfacesTo<YandexDiskServer>().AsSingle();
        }
    }
}