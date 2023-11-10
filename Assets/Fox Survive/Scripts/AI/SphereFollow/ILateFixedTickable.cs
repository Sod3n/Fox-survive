using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SphereFollow
{
    public interface ILateFixedTickable
    {
        public void LateFixedTick();
    }
}
