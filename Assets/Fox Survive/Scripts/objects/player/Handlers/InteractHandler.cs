using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Player
{
    public class InteractHandler : MonoBehaviour
    {
        private InputState _inputState;
        private Facade _facade;

        [Inject]
        public void Inject(InputState inputState, Facade facade)
        {
            _inputState = inputState;
            _facade = facade;
        }


        private IInteractable _value;
        private void OnTriggerEnter(Collider other)
        {
            other.TryGetComponent(out _value); //maybe expensive
        }

        public void FixedUpdate()
        {
            if (!_inputState.Interact) return;

            _value?.Interact(_facade);
        }
    }
}
