using System;
using UnityEngine;

namespace Default
{
    [Serializable]
    public class CardTemplate
    {
        [SerializeField] 
        private CardType _type;

        // [SerializeField] 
        // private int _health;

        // [SerializeField] 
        // private int _damage;

        [SerializeField] 
        private bool _canPickUp;

        [SerializeField] 
        private bool _canKill;

        [SerializeField] 
        private GameObject _prefab;

        public CardType Type => _type;
        public GameObject Prefab => _prefab;
        // public int Health => _health;
        // public int Damage => _damage;
        public bool CanPickUp => _canPickUp;
        public bool CanKill => _canKill;
    }

    public class Card
    {
        public CardType Type;
        public GameObject Prefab;
        public int Health;
        public int Damage;
        public bool CanPickUp;
        public bool CanKill;

        public void SetHealth(int val)
        {
            Health = val;
        }
    }
}