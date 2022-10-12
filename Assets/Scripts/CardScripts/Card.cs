using System;
using UnityEngine;

namespace Default
{
    [Serializable]
    public class Card
    {
        [SerializeField] 
        private CardType _type;

        [SerializeField] 
        private int _health;

        [SerializeField] 
        private int _damage;

        [SerializeField] 
        private bool _canPickUp;

        [SerializeField] 
        private GameObject _prefab;

        public CardType Type => _type;
        public GameObject Prefab => _prefab;
        public int Health => _health;
        public int Damage => _damage;
        public bool CanPickUp => _canPickUp;
    }
}