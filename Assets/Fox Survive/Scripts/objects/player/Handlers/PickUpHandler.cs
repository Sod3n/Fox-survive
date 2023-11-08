using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PickUpHandler : MonoBehaviour
    {
        private PickUpable _value;

        public PickUpable Value { get => _value; }
        private void OnTriggerEnter(Collider other)
        {
            other.TryGetComponent(out _value); //maybe expensive
        }
    }
}
