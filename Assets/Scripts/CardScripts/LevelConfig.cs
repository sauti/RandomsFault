using System;
using System.Collections.Generic;
using UnityEngine;

namespace Default
{
    [Serializable]
    public class CardPerLevelData {
        [SerializeField] 
        private CardId _cardId;

        [SerializeField] 
        private int _health;

        [SerializeField] 
        private int _damage;

        [SerializeField] 
        private int _minAmount;

        [SerializeField] 
        private int _chance;

        [HideInInspector]
        public int minChance;
        [HideInInspector]
        public int maxChance;

        [SerializeField] 
        private List<ChildCard> _spawnsEachTurn;

        [SerializeField] 
        private List<ChildCard> _spawnsAfterDeath;

        public CardId CardId => _cardId;
        public int MinAmount => _minAmount;
        public int Chance => _chance;
        public int Health => _health;
        public int Damage => _damage;
        public List<ChildCard> SpawnsEachTurn => _spawnsEachTurn;
        public List<ChildCard> SpawnsAfterDeath => _spawnsAfterDeath;
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
            return UnityEngine.Random.Range(minCards, maxCards);
        }

        public CardPerLevelData GetCardConfig(CardId cardId)
        {
            return _cards.Find(c => c.CardId == cardId);
        }

        public List<CardPerLevelData> GetCardsConfig()
        {
            return _cards;
        }
    }
}