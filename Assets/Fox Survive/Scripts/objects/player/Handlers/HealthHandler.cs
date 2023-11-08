using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Player
{
    public class HealthHandler : IFixedTickable, IInitializable
    {
        
        private Settings _settings;
        private Model _model;
        private Facade _facade;


        public HealthHandler(Settings settings, Model model, Facade facade)
        {
            _settings = settings;
            _model = model;
            _facade = facade;
        }

        public void Initialize()
        {
            _model.Health = _model.MaxHealth;
        }

        public void FixedTick()
        {
            if (!_model.Rigidbody.gameObject.activeSelf) return;

            if (_model.Health <= 0) Die();
        }

        private void Die()
        {
            _model.Health = 0;
            _settings.DeathParticle.transform.position = _model.Rigidbody.position;
            _settings.DeathParticle.Play();
            _model.Rigidbody.gameObject.SetActive(false);
            _facade.OnDie.Invoke();
        }

        

        [Serializable]
        public class Settings
        {
            public ParticleSystem DeathParticle;
        }
    }
}
