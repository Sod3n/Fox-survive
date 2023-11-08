using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    [Serializable]
    public class Model
    {
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private float _maxHealth;
        [SerializeField] private float _maxStarve;
        [SerializeField] private Transform _respawnPoint;
        private float _health;
        private float _starve;

        public Rigidbody Rigidbody { get => _rigidbody; set => _rigidbody = value; }
        public float MaxHealth { get => _maxHealth; set => _maxHealth = value; }
        public float Health { get => _health; set => _health = value; }
        public float MaxStarve { get => _maxStarve; set => _maxStarve = value; }
        public float Starve { get => _starve; set => _starve = value; }
        public Transform RespawnPoint { get => _respawnPoint; set => _respawnPoint = value; }
    }
}
