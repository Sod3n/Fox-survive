using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interactables
{
    public class Healing : MonoBehaviour, Player.IInteractable
    {
        public void Interact(Player.Facade facade)
        {
            facade.Damage(10);
        }
    }
}
