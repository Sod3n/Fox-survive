using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GUI
{
    public interface IStatesContainer
    {
        public T ResolveIState<T>() where T : class, IState;
    }
}
