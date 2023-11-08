using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace GUI
{
    public class InputState : ITickable
    {
        public bool AnyButton { get; private set; }

        private StandartControlls _controlls;
        public InputState(StandartControlls controlls)
        {
            _controlls = controlls;
        }

        public void Tick()
        {
            AnyButton = _controlls.GUI.AnyButton.IsPressed();
        }
    }
}
