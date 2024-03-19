using Infrastructure.Input;
using Infrastructure.Level.EventsBus;
using UnityEngine;
using Zenject;

namespace DefaultNamespace
{
    public class GlobalInstaller: MonoInstaller
    {
        public override void InstallBindings()
        {
            CreateInputService();
            CreateEventBus();
        }

        private void CreateEventBus()
        {
            Container.BindInterfacesAndSelfTo<EventBus>().AsSingle();
        }

        private void CreateInputService()
        {
            if (Application.isEditor)
            {
                Container.BindInterfacesAndSelfTo<StandaloneInputService>().AsSingle();
            }
            else
            {
                Container.BindInterfacesAndSelfTo<MobileInputService>().AsSingle();
            }
        }
    }
}