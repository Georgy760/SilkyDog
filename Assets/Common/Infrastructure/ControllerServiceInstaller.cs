using Common.Scripts.ManagerService;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Common.Scripts.ZenjectServices
{
    public class ControllerServiceInstaller : MonoInstaller
    {
        [SerializeField] private ControllerService controllerServicePrefab;
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
        }
    }
}