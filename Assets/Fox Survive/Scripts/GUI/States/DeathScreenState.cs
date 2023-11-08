using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace GUI
{
    public class DeathScreenState : ITickableState
    {
        private Settings _settings;
        private InputState _inputState;
        private IStatesContainer _container;
        private Player.Facade _player;

        private bool _anyButtonPressed;

        public DeathScreenState(Settings settings, InputState inputState, IStatesContainer container, Player.Facade player)
        {
            _settings = settings;
            _inputState = inputState;
            _container = container;
            _player = player;
        }

        public void Enter()
        {
            _settings.Menu.enabled = true;
            _anyButtonPressed = false;
        }

        public void Exit()
        {
            _settings.Menu.enabled = false;
        }

        public IState GetNextState()
        {
            if (_anyButtonPressed)
            {
                _player.Respawn();
                return _container.ResolveIState<GameWorldState>();
            }

            return null;
        }

        public void Tick()
        {
            if (_inputState.AnyButton) _anyButtonPressed = true;
        }

        [Serializable]
        public class Settings
        {
            public Canvas Menu;
        }
    }
}
