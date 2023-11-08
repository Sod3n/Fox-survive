using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace GameController
{
    public class Installer : MonoInstaller
    {
        public PauseHandler.Settings PauseSettings;

        public override void InstallBindings()
        {
            Container.BindInstances(PauseSettings);

            Container.BindInterfacesAndSelfTo<PauseHandler>().AsSingle();
        }
    }
}
