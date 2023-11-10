using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace SphereFollow
{
    public class InputState : ITickable, IFixedTickable
    {
        public Vector2 PositionDelta {  get; private set; }
        public float DistanceScaleDelta { get; private set; }
        
        private StandartControlls _controls;

        public InputState(StandartControlls controls)
        {
            _controls = controls;
        }

        public void Tick()
        {
            PositionDelta += _controls.Gameplay.CameraPositionDelta.ReadValue<Vector2>();
            DistanceScaleDelta += _controls.Gameplay.CameraDistanceScaleDelta.ReadValue<float>();
        }

        public void FixedTick()
        {
            PositionDelta = Vector2.zero;
            DistanceScaleDelta = 0;
        }
    }
}
