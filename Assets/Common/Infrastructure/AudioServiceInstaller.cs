using Common.Scripts;
using Common.Scripts.AudioService;
using UnityEngine;
using Zenject;

public class AudioServiceInstaller : MonoInstaller
{
    public AudioService AudioServicePrefab;
    public override void InstallBindings()
    {
        BindAudioService();
    }

    private void BindAudioService()
    {
        Container
            .Bind<IAudioService>()
            .FromComponentInNewPrefab(AudioServicePrefab)
            .UnderTransform(transform).AsSingle();
        //Container.InstantiatePrefab(AudioServicePrefab);
    }
}