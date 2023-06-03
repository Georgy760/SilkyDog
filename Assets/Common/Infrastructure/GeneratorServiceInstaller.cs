using Common.Scripts.ManagerService;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using Zenject;

public class GeneratorServiceInstaller : MonoInstaller
{
    [SerializeField] private ServiceGeneratorEnemy serviceGeneratorEnemy;
    public override void InstallBindings()
    {
        InstallControlSheme();
    }

    private void InstallControlSheme()
    {
        Container
            .Bind<IServiceGeneratorEnemy>()
            .FromComponentInNewPrefab(serviceGeneratorEnemy)
            .UnderTransform(transform).AsSingle();
    }
}
