using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Player
{
    public class OnGroundState : MonoBehaviour
    {
        public bool Value {  get; private set; }

        private Settings _settings;

        [Inject]
        public void Inject(Settings settings)
        {
            _settings = settings;
        }


        private void OnCollisionStay(Collision collision)
        {
            if(collision.GetContact(0).point.y < _settings.FeetY.position.y)
            {
                Value = true;
            }
        }
        private void OnCollisionExit(Collision collision)
        {
            Value = false;
        }

        [Serializable]
        public class Settings
        {
            public Transform FeetY;
        }
    }
}
