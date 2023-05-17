using Common.Scripts.UserData;

namespace Common.Infrastructure
{
    using UnityEngine;
    using Zenject;

    public class UserDataInstaller : MonoInstaller
    {
        public GameObject UserDataPrefab;
        public override void InstallBindings()
        {
            InstallUserData();
        }

        private void InstallUserData()
        {
            Container
                .Bind<IUserData>()
                .FromComponentInNewPrefab(UserDataPrefab)
                .UnderTransform(transform).AsSingle();
        }
    }
}