using Common.Scripts.ManagerService;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Common.Scripts.ZenjectServices
{
    public class ControllerServiceInstaller : MonoInstaller
    {
        [SerializeField] private ControllerService controllerServicePrefab;
        [SerializeField] private SessionService sessionServicePrefab;
        public override void InstallBindings()
        {
            InstallControlSheme();
        }

        private void InstallControlSheme()
        {
            Container
                .Bind<IControllerService>()
                .FromComponentInNewPrefab(controllerServicePrefab)
                .UnderTransform(transform).AsSingle();
            Container
               .Bind<ISessionService>()
               .FromComponentInNewPrefab(sessionServicePrefab)
               .UnderTransform(transform).AsSingle();
        }
    }
}