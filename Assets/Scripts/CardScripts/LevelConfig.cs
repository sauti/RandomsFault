using System;
using System.Collections.Generic;
using UnityEngine;

namespace Default
{
    // public class Utilities: MonoBehaviour
    // {
    //     public GetChanceItem(List<CardPerLevelData> list)
    //     {
    //         int chanceSum = 0;
    //         for (int i = 0; i < list.Count; i++)
    //         {
    //             var current = list[i];
    //             chanceSum += current.Chance;
                
    //             if (i == 0) {
    //                 current.minChance = 0;
    //                 current.maxChance = current.Chance;
    //             } else {
    //                 current.minChance = list[i - 1].maxChance;
    //                 current.maxChance = current.minChance + current.Chance;
    //             }
    //         }

    //         int rand = Random.Random(0, chanceSum);
    //         for (int i = 0; i < list.Count; i++)
    //         {
    //             current = list[i];
    //             if (rand >= current.minChance && rand < current.maxChance)
    //             {
    //                 return current;
    //             }
    //         }

    //         return null;
    //     }
    // }

    [Serializable]
    public class CardPerLevelData {
        [SerializeField] 
        private CardType _type;

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

        public CardType Type => _type;
        public int MinAmount => _minAmount;
        public int Chance => _chance;
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
            return UnityEngine.Random.Range(minCards, maxCards);
        }

        public CardPerLevelData GetCardConfig(CardType type)
        {
            return _cards.Find(c => c.Type == type);
        }

        public List<CardPerLevelData> GetCardsConfig()
        {
            return _cards;
        }
    }
}