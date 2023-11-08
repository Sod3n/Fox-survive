using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Player
{
    public class StarveHandler : IFixedTickable, IInitializable
    {
        private Model _model;
        private Settings _settings;

        public StarveHandler(Model model, Settings settings)
        {
            _model = model;
            _settings = settings;
        }

        public void Initialize()
        {
            _model.Starve = _model.MaxStarve;
        }

        public void FixedTick()
        {
            if(_model.Starve > 0)
            {
                _model.Starve -= _settings.StarveDecrease * Time.deltaTime;
            }
            else
            {
                _model.Health -= _settings.HealthDecrease * Time.deltaTime;
            }
        }

        

        [Serializable]
        public class Settings
        {
            [Tooltip("Per second.")]
            public float StarveDecrease = 1.0f;
            [Tooltip("Per second. When starving.")]
            public float HealthDecrease = 1.0f;
        }
    }
}
