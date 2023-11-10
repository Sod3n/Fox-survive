using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SphereFollow
{
    [Serializable]
    public class Model
    {
        public Transform Transform;
        public Transform Target;
        public float DistanceToTarget;
    }
}
