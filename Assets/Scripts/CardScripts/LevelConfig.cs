using System;
using System.Collections.Generic;
using UnityEngine;

namespace Default
{
    [Serializable]
    public class CardPerLevelData: CardChanceBase {
        [SerializeField] 
        private int _minAmount;

        [SerializeField] 
        private int _health;

        [SerializeField] 
        private int _damage;

        [SerializeField] 
        private List<CardChance> _spawnsEachTurn;

        [SerializeField] 
        private List<CardChance> _spawnsAfterDeath;

        public int MinAmount => _minAmount;
        public int Health => _health;
        public int Damage => _damage;
        public List<CardChance> SpawnsEachTurn => _spawnsEachTurn;
        public List<CardChance> SpawnsAfterDeath => _spawnsAfterDeath;
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