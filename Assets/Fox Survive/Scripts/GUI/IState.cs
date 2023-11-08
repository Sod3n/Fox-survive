using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GUI
{
    public interface IState
    {
        public void Enter();
        public void Exit();
        public IState GetNextState();
    }
    public interface ITickableState : IState
    {
        public void Tick();
    }
}
