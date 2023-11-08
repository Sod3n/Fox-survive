using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Player
{
    public class MovementHandler : IFixedTickable
    {
        private readonly Settings _settings;
        private readonly InputState _inputState;
        private Model _player;
        public MovementHandler(
            Settings settings,
            InputState inputState,
            Model player)
        {
            _settings = settings;
            _inputState = inputState;
            _player = player;
        }

        private Vector3 _relativeDirection;
        private Quaternion _relativeRotation;
        private float _relativeSpeed;
        public void FixedTick()
        {
            CalculateRelativeDirection();

            CalculateRelativeRotation();
            _player.Rigidbody.MoveRotation(_relativeRotation.normalized);

            if (_relativeDirection != Vector3.zero && 
                _player.Rigidbody.rotation != Quaternion.LookRotation(_relativeDirection, Vector3.up))
            {
                _relativeSpeed = _settings.Speed * _settings.SpeedScaleOnRotation;
            }
            else
            {
                _relativeSpeed = _settings.Speed;
            }

            _player.Rigidbody.MovePosition(_player.Rigidbody.position +
                _relativeSpeed * _relativeDirection * Time.deltaTime);
        }

        private void CalculateRelativeDirection()
        {
            _relativeDirection = Camera.main.transform.TransformDirection(_inputState.Direction);
            _relativeDirection.y = 0;
            _relativeDirection = _relativeDirection.normalized;
        }
        private void CalculateRelativeRotation()
        {
            if(_relativeDirection.sqrMagnitude == 0) return;

            _relativeRotation = Quaternion.RotateTowards(
                _player.Rigidbody.rotation,
                Quaternion.LookRotation(_relativeDirection, Vector3.up),
                _settings.RotationSpeed * Time.deltaTime).normalized;
        }

        [Serializable]
        public class Settings
        {
            [Tooltip("Unit per second.")]
            public float Speed;
            [Tooltip("Degrees per second.")]
            public float RotationSpeed;
            public float SpeedScaleOnRotation;
        }
    }
}
