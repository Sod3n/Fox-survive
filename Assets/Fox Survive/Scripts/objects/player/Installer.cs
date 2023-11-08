using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Player
{
    public class Installer : MonoInstaller
    {
        public Model Player;
        public MovementHandler.Settings MovementSettings;
        public JumpHandler.Settings JumpSettings;
        public OnGroundState.Settings OnGroundSettings;
        public HealthHandler.Settings HealthSettings;
        public StarveHandler.Settings StarveSettings;

        public override void InstallBindings()
        {
            Container.BindInstances(
                Player, MovementSettings, JumpSettings, 
                OnGroundSettings, HealthSettings, StarveSettings
                );

            Container.BindInterfacesAndSelfTo<InputState>().AsSingle();

            var onGroundState = Container.InstantiateComponent<OnGroundState>(Player.Rigidbody.gameObject);
            Container.BindInstance(onGroundState).AsSingle();

            var interactHandler = Container.InstantiateComponent<InteractHandler>(Player.Rigidbody.gameObject);
            Container.BindInstance(interactHandler).AsSingle();

            Container.BindInterfacesAndSelfTo<MovementHandler>().AsSingle();
            Container.BindInterfacesAndSelfTo<JumpHandler>().AsSingle();
            Container.BindInterfacesAndSelfTo<HealthHandler>().AsSingle();
            Container.BindInterfacesAndSelfTo<StarveHandler>().AsSingle();
        }
    }
}
