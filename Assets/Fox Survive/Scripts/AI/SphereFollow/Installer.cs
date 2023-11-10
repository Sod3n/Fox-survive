using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace SphereFollow
{
    public class Installer : MonoInstaller
    {
        public Model Model;
        public ScaleDistance.Settings DistanceScaleSettings;
        public SphereRotation.Settings RotationScales;
        public override void InstallBindings()
        {
            Container.BindInstances(DistanceScaleSettings, Model, RotationScales);

            Container.BindInterfacesAndSelfTo<InputState>().AsSingle();

            Container.BindInterfacesTo<ScaleDistance>().AsSingle();

            Container.BindInterfacesTo<KeepDistance>().AsSingle();
            Container.BindInterfacesTo<SphereRotation>().AsSingle();


            //Execute InputState.FixedTick after others cause it resets values there
            //Execute at 90+ so after movement of target
            Container.BindLateTickableExecutionOrder<SphereRotation>(90);
            Container.BindLateTickableExecutionOrder<KeepDistance>(100);

            Container.BindFixedTickableExecutionOrder<ScaleDistance>(100);
            Container.BindFixedTickableExecutionOrder<InputState>(110);

            Container.BindInterfacesTo<LateFixedTickController>().AsSingle();
        }
    }
}
