using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace SphereFollow
{
    public class KeepDistance : ILateTickable
    {
        private Model _model;

        public KeepDistance(Model model)
        {
            _model = model;
        }


        public void LateTick()
        {
            _model.Transform.position = _model.Target.position;
            _model.Transform.position -= _model.Transform.forward * _model.DistanceToTarget;
        }
    }
}
