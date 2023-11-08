using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Player
{
    public class InputState : ITickable, IInitializable
    {
        public Vector3 Direction { get; private set; }

        private UniTask _waitNextFixedTick; //maybe split on two but ok for now
        private bool _interact;
        private bool _jump;

        public bool Jump 
        { 
            get => _jump; 
            private set
            {
                if (_waitNextFixedTick.Status != UniTaskStatus.Succeeded) return;
                if (value == _jump) return;

                _jump = value;
                _waitNextFixedTick = UniTask.WaitForFixedUpdate().ToUniTask();
            }
        }
        public bool Interact 
        {
            get => _interact;
            private set
            {
                if (_waitNextFixedTick.Status != UniTaskStatus.Succeeded) return;
                if (value == _interact) return;

                _interact = value;
                _waitNextFixedTick = UniTask.WaitForFixedUpdate().ToUniTask();
            }
        }


        private readonly StandartControlls _controlls;

        public InputState(StandartControlls controlls)
        {
            _controlls = controlls;
        }
        public void Initialize()
        {
            _controlls.Enable();
        }

        public void Tick()
        {
            Direction = _controlls.Gameplay.Direction.ReadValue<Vector3>();
            Jump = _controlls.Gameplay.Jump.WasReleasedThisFrame();
            Interact = _controlls.Gameplay.Interact.WasReleasedThisFrame();
        }
        
    }
}
