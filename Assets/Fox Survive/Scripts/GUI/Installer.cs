using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace GUI
{
    public class Installer : MonoInstaller
    {
        public Player.Facade Player;
        public DeathScreenState.Settings DeathScreenSettings;
        public GameWorldState.Settings GameWorldSettings;
        public override void InstallBindings()
        {
            Container.BindInstances(DeathScreenSettings, Player, GameWorldSettings);

            Container.BindInterfacesAndSelfTo<InputState>().AsSingle();

            Container.BindInterfacesTo<StateMachine>().AsSingle();

            Container.BindInterfacesAndSelfTo<GameWorldState>().AsSingle();
            Container.BindInterfacesAndSelfTo<DeathScreenState>().AsSingle();
        }
    }
}
