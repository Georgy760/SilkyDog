using Common.Scripts.ManagerService;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Common.Infrastructure
{
    public class PlayerInputServiceInstaller : MonoInstaller
    {
        [SerializeField] private PlayerInputService playerInputServicePrefab;
        [SerializeField] private SessionService sessionServicePrefab;
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
            Container
               .Bind<ISessionService>()
               .FromComponentInNewPrefab(sessionServicePrefab)
               .UnderTransform(transform).AsSingle();
        }
    }
}