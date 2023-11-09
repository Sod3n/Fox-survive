using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interactables
{
    public class Feeding : MonoBehaviour, Player.IInteractable
    {
        [SerializeField] private float _feedValue;
        public void Interact(Player.Facade player)
        {
            player.Feed(_feedValue);
        }
    }
}
