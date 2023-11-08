using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using static UnityEditor.Experimental.GraphView.GraphView;

namespace Player
{
    public class JumpHandler : IFixedTickable, IInitializable
    {
        private readonly Settings _settings;
        private Model _player;
        private InputState _inputState;
        private OnGroundState _onGroundState;
        public JumpHandler(Settings settings, Model player, InputState inputState, OnGroundState onGroundState)
        {
            _settings = settings;
            _player = player;
            _inputState = inputState;
            _onGroundState = onGroundState;
        }

        private bool _canJump = true;
        private float _force = 0;
        private UniTask _jumpReloadTask;

        public void Initialize()
        {
            CalculateForce();
        }

        public void FixedTick()
        {
            if (_onGroundState.Value && JumpReloaded()) _canJump = true;

            if (!(_canJump && _inputState.Jump && JumpReloaded())) return;

            _canJump = false;
            _player.Rigidbody.AddForce(Vector3.up * _force, ForceMode.VelocityChange);
            _jumpReloadTask = UniTask.WaitForSeconds(_settings.ReloadTime);
        }
        private bool JumpReloaded()
        {
            if (_jumpReloadTask.Status == UniTaskStatus.Succeeded) return true; 

            return false;
        }

        private void CalculateForce()
        {
            //formula from physics book
            _force = _settings.Height / _settings.Time;
            _force += Physics.gravity.magnitude * _settings.Time / 2;
        }

        

        [Serializable]
        public class Settings
        {
            [Tooltip("In units.")]
            public float Height;
            [Tooltip("In seconds.")]
            public float Time;
            [Tooltip("In seconds.")]
            public float ReloadTime;
        }
    }
}
