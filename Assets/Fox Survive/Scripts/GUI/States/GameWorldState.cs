using Player;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace GUI
{
    public class GameWorldState : ITickableState
    {
        private Player.Facade _player;
        private IStatesContainer _container;
        private Settings _settings;

        private bool _playerDied = false;
        private Vector3 _healthBarScale;
        private Vector3 _starveBarScale;

        public GameWorldState(Facade player, IStatesContainer statesContainer, Settings settings)
        {
            _player = player;
            _container = statesContainer;
            _settings = settings;
        }

        public void Enter()
        {
            _player.OnDie += OnPlayerDie;
            _playerDied = false;
            _healthBarScale = _settings.HealthBar.localScale;
            _starveBarScale = _settings.StarveBar.localScale;
        }

        public void Exit()
        {
            _player.OnDie -= OnPlayerDie;
        }
        private void OnPlayerDie()
        {
            _playerDied = true;
        }

        

        public void Tick()
        {
            _healthBarScale.x = _player.Health / _settings.BarsBaseValue;
            _settings.HealthBar.localScale = _healthBarScale;

            _starveBarScale.x = _player.Starve / _settings.BarsBaseValue;
            _settings.StarveBar.localScale = _starveBarScale;
        }

        public IState GetNextState()
        {
            if (_playerDied)
            {
                return _container.ResolveIState<DeathScreenState>();
            }

            return null;
        }

        [Serializable]
        public class Settings
        {
            public float BarsBaseValue = 100;

            public RectTransform HealthBar;
            public RectTransform HealthBarBackground;

            public RectTransform StarveBar;
            public RectTransform StarveBarBackground;
        }
    }
}
