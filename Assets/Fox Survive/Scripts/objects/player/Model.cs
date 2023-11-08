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
        [SerializeField] private Transform _respawnPoint;
        private float _health;
        

        public Rigidbody Rigidbody { get => _rigidbody; set => _rigidbody = value; }
        public float MaxHealth { get => _maxHealth; set => _maxHealth = value; }
        public float Health { get => _health; set => _health = value; }
        public Transform RespawnPoint { get => _respawnPoint; set => _respawnPoint = value; }
    }
}
