using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace SphereFollow
{
    public class SphereRotation : ILateTickable
    {
        private Model _model;
        private InputState _input;
        private Settings _settings;
        private Vector3 _rotation;
        private float _angle;

        public SphereRotation(Model model, InputState input, Settings settings)
        {
            _model = model;
            _input = input;
            _settings = settings;
        }

        public void LateTick()
        {
            _rotation = _input.PositionDelta.normalized * _settings.Sensitivity * Time.deltaTime;

            _model.Transform.Rotate(Vector3.up, _rotation.x, Space.World);
            RotateY(_rotation.y);

            _angle = Vector3.Angle(_model.Transform.TransformDirection(Vector3.up), Vector3.up);

            if (_angle < _settings.MinAngle)
            {
                RotateY(_settings.MinAngle - _angle);
            }

            if (_angle > _settings.MaxAngle)
            {
                RotateY(_settings.MaxAngle - _angle);
            }
        }

        private void RotateY(float value)
        {
            _model.Transform.Rotate(Vector3.right, value, Space.Self);
        }

        [Serializable]
        public class Settings
        {
            public Vector2 Sensitivity;

            [Header("Y limit angle")]
            [Range(0, 180)]
            public float MinAngle;
            [Range(0, 180)]
            public float MaxAngle;
        }
    }
}
