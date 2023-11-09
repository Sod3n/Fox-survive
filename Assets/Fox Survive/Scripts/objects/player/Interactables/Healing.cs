using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interactables
{
    public class Healing : MonoBehaviour, Player.IInteractable
    {
        [SerializeField] private float _healValue;
        public void Interact(Player.Facade facade)
        {
            facade.Heal(_healValue);
        }
    }
}
