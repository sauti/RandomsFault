using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Default
{
    [Serializable]
    public class CardTemplate
    {
        [SerializeField] 
        private CardId _cardId;
        
        [SerializeField] 
        private string _name;

        [SerializeField] 
        private string _description;

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

        public CardId CardId => _cardId;
        public GameObject Prefab => _prefab;
        public string Name => _name;
        public string Description => _description;
        public bool CanBeKilled => _canBeKilled;
        public bool CanHeal => _canHeal;
        public bool IsWeapon => _isWeapon;
        public bool IsTrap => _isTrap;
    }

    public class Card
    {
        public CardId CardId;
        public GameObject Prefab;
        public string Thought;
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