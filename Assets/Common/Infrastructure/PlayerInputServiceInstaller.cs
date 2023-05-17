using Common.Scripts.ManagerService;
using UnityEngine;
using Zenject;

namespace Common.Infrastructure
{
    public class PlayerInputServiceInstaller : MonoInstaller
    {
        [SerializeField] private PlayerInputService playerInputServicePrefab;
        public override void InstallBindings()
        {
            InstallControlSheme();
        }

        private void InstallControlSheme()
        {
            Container
                .Bind<IPlayerInputService>()
                .FromComponentInNewPrefab(playerInputServicePrefab)
                .UnderTransform(transform).AsSingle();
        }
    }
}