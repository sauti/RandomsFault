using System;
using System.Collections.Generic;
using UnityEngine;

namespace Default
{
    [Serializable]
    public class CardPerLevelData {
        [SerializeField] 
        private CardType _type;

        [SerializeField] 
        private int _health;

        [SerializeField] 
        private int _damage;

        public CardType Type => _type;
        public int Health => _health;
        public int Damage => _damage;
    }

    [CreateAssetMenu(menuName = "Cards/LevelConfig")]
    public class LevelConfig : ScriptableObject
    {
        [SerializeField] 
        private int minCards;

        [SerializeField] 
        private int maxCards;

        [SerializeField] 
        private List<CardPerLevelData> _cards;

        public int GetCardsAmount()
        {
            return minCards;
            // return Random.Range(minCards, maxCards);
        }

        public CardPerLevelData GetCardConfig(CardType type)
        {
            return _cards.Find(c => c.Type == type);
        }
    }
}