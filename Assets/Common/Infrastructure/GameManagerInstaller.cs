
using Common.GameManager.Scripts;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Common.Infrastructure
{
    public class GameManagerInstaller : MonoInstaller
    {
        public GameObject GameManagerPrefab;
        public GameObject UIPrefab;
        public override void InstallBindings()
        {
            InstallGameManager();
        }

        private void InstallGameManager()
        {
            Container.BindInstance(UIPrefab).AsSingle();
            Container
                .Bind<IGameManager>()
                .FromComponentInNewPrefab(GameManagerPrefab)
                .UnderTransform(transform).AsSingle();
            Container.InstantiatePrefab(UIPrefab);
            
            
        }
    }
}