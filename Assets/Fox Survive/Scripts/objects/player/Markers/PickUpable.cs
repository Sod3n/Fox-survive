using Objects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PickUpable : MonoBehaviour
    {
        [SerializeField] private readonly GameObjectType _type;

        public GameObjectType Type => _type;
    }
}
