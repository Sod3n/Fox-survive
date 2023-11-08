using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Player
{
    public class Facade : MonoBehaviour
    {
        private Model _model;

        [Inject]
        public void Inject(Model model)
        {
            _model = model;
        }

        public float MaxHealth { get => _model.MaxHealth; }
        public float Health { get => _model.Health; }

        public void Heal(float value)
        {
            _model.Health += value;
        }

        public void Damage(float value)
        {
            _model.Health -= value;
        }
        public void Respawn()
        {
            Heal(MaxHealth / 2);
            _model.Rigidbody.position = _model.RespawnPoint.position;
            _model.Rigidbody.gameObject.SetActive(true);
        }

        public Action OnDie = () => { };
    }
}
