using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public interface IInteractable
    {
        public void Interact(Facade facade);
    }
}
