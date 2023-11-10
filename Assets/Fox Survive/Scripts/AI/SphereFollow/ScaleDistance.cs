using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Plastic.Newtonsoft.Json.Linq;
using UnityEngine;
using Zenject;

namespace SphereFollow
{
    public class ScaleDistance : IFixedTickable
    {
        private InputState _input;
        private float _distance;
        private float _scaleDelta;
        private Settings _settings;
        private Model _model;

        public ScaleDistance(InputState input, Settings settings, Model model)
        {
            _input = input;
            _settings = settings;
            _model = model;
        }
        public void FixedTick()
        {
            _scaleDelta = _input.DistanceScaleDelta * _settings.Sensetivity;

            _distance += _scaleDelta;

            if (_distance > _settings.MaxDistance) _distance = _settings.MaxDistance;

            if (_distance < _settings.MinDistance) _distance = _settings.MinDistance;

            
            _model.DistanceToTarget = _distance;
        }

        [Serializable]
        public class Settings
        {
            public float Sensetivity;
            public float MaxDistance;
            public float MinDistance;
        }
    }
}
