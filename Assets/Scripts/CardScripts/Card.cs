using System;
using UnityEngine;

namespace Default
{
    [Serializable]
    public class CardTemplate
    {
        [SerializeField] 
        private CardType _type;

        [SerializeField] 
        private bool _canBeKilled;

        [SerializeField] 
        private bool _isWeapon;

        [SerializeField] 
        private bool _canHeal;

        [SerializeField] 
        private bool _isTrap;

        [SerializeField] 
        private GameObject _prefab;

        public CardType Type => _type;
        public GameObject Prefab => _prefab;
        public bool CanBeKilled => _canBeKilled;
        public bool CanHeal => _canHeal;
        public bool IsWeapon => _isWeapon;
        public bool IsTrap => _isTrap;
    }

    public class Card
    {
        public CardType Type;
        public GameObject Prefab;
        public int Health;
        public int Damage;
        public bool CanBeKilled;
        public bool CanHeal;
        public bool IsWeapon;
        public bool IsTrap;

        public bool CanPickUp => CanHeal || IsWeapon;

        public void SetHealth(int val)
        {
            Health = val;
        }
    }
}