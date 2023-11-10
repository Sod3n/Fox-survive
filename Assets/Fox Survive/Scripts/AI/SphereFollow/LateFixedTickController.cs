using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace SphereFollow
{
    public class LateFixedTickController : IFixedTickable
    {
        private List<ILateFixedTickable> _lateFixedTickables;

        public LateFixedTickController(List<ILateFixedTickable> lateFixedTickables)
        {
            _lateFixedTickables = lateFixedTickables;
        }

        public virtual void FixedTick()
        {
            LateFixedTick();
        }

        public virtual async UniTask LateFixedTick()
        {
            await UniTask.WaitForFixedUpdate();
            foreach(var t in _lateFixedTickables)
            {
                t.LateFixedTick();
            }
        }
    }
}
