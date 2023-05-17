using Common.Scripts.ManagerService;
using UnityEngine;
using Zenject;

namespace Common.Infrastructure
{
    public class SessionServiceInstaller : MonoInstaller
    {
        [SerializeField] private SessionService sessionServicePrefab;
        public override void InstallBindings()
        {
            InstallSessionService();
        }

        private void InstallSessionService()
        {
            Container
                .Bind<ISessionService>()
                .FromComponentInNewPrefab(sessionServicePrefab)
                .UnderTransform(transform).AsSingle();
        }
    }
}