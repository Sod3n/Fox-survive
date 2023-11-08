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

        public GameWorldState(Facade player, IStatesContainer statesContainer, Settings settings)
        {
            _player = player;
            _container = statesContainer;
            _settings = settings;
        }

        private bool _playerDied = false;

        public void Enter()
        {
            _player.OnDie += OnPlayerDie;
            _playerDied = false;
            _healthBarSizeDelta = _settings.HealthBar.sizeDelta;
        }

        public void Exit()
        {
            _player.OnDie -= OnPlayerDie;
        }
        private void OnPlayerDie()
        {
            _playerDied = true;
        }

        private Vector2 _healthBarSizeDelta;

        public void Tick()
        {
            _healthBarSizeDelta.x = _player.Health * _settings.UIScale;
            _settings.HealthBar.sizeDelta = _healthBarSizeDelta;
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
            [Tooltip("Scale for health parameter to equal a rect transform width.")]
            public float UIScale;

            public RectTransform HealthBar;
            public RectTransform HealthBarBackground;

        }
    }
}
