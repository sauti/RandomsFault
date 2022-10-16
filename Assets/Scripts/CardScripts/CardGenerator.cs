using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Default {
    [CreateAssetMenu(menuName = "Cards/CardGenerator")]
    public class CardGenerator: ScriptableObject
    {
        public CardsConfig cardsConfig;
        public LevelsList levelsListConfig;

        private LevelConfig _levelConfig;

        public void Init(int level) {
            _levelConfig = levelsListConfig.GetConfigForLevel(level);
        }

        public List<CardData> GenerateCardsForLevel() {
            int amount = _levelConfig.GetCardsAmount();
            List<CardPerLevelData> conf = _levelConfig.GetCardsConfig();
            List<CardData> cards = new List<CardData>();
            Vector2Int coord = new Vector2Int(0, 0);
            
            List<CardType> types = GenerateRequiredCardTypes(conf, amount);
            if (types.Count < amount) {
                types.AddRange(GetRandomTypesByChance(conf, amount - types.Count));
            }

            string debugStr = ""; 
            for (var i = 0; i < types.Count; i++) {
                debugStr += (types[i] + ", ");
                CardData card = GenerateCardByType(types[i], coord);
                cards.Add(card);
            }
            Debug.Log(debugStr);

            return cards;
        }

        public CardData GenerateCardByType(CardType type, Vector2Int coord, bool IsRotated = false) {
            CardTemplate template = cardsConfig.GetByType(type);
            CardPerLevelData cardLevelData = _levelConfig.GetCardConfig(type);

            var card = new Card()
            {
                Type = template.Type,
                Prefab = template.Prefab,
                Health = cardLevelData != null ? cardLevelData.Health : 0,
                Damage = cardLevelData != null ? cardLevelData.Damage : 0,
                CanBeKilled = template.CanBeKilled,
                CanHeal = template.CanHeal,
                IsWeapon = template.IsWeapon,
                IsTrap = template.IsTrap,
            };

            CardData data = new CardData()
            {
                Id = GetUuid(),
                Card = card,
                Coord = coord,
                IsRotated = IsRotated || false
            };
            return data;
        }

        private string GetUuid()
        {
            return System.Guid.NewGuid().ToString("N");
        }

        private List<CardType> GenerateRequiredCardTypes(List<CardPerLevelData> list, int amount) {
            List<CardType> types = new List<CardType>();
            for (int i = 0; i < list.Count; i++)
            {
                CardPerLevelData current = list[i];
                if (current.MinAmount > 0) {
                    for (int j = 0; j < current.MinAmount; j++) {
                        types.Add(current.Type);
                    }
                }
            }

            if (types.Count > amount) {
                throw new ArgumentOutOfRangeException("Can't generate more cards with minAmount than total cards for this level.");
            }

            return types;
        }

        private List<CardType> GetRandomTypesByChance(List<CardPerLevelData> list, int amount) {
            int chanceSum = 0;
            for (int i = 0; i < list.Count; i++)
            {
                CardPerLevelData current = list[i];
                chanceSum += current.Chance;
                if (i == 0) {
                    current.minChance = 0;
                    current.maxChance = current.Chance;
                } else {
                    current.minChance = list[i - 1].maxChance;
                    current.maxChance = current.minChance + current.Chance;
                }
            }

            List<CardType> types = new List<CardType>();
            for (var i = 0; i < amount; i++) {
                CardType? type = GetRandomTypeByChance(list, chanceSum);
                if (type is CardType) {
                    types.Add((CardType)type);
                }
            }

            return types;
        }

        private CardType? GetRandomTypeByChance(List<CardPerLevelData> list, int chanceSum)
        {
            int rand = UnityEngine.Random.Range(0, chanceSum);
            for (int i = 0; i < list.Count; i++)
            {
                CardPerLevelData current = list[i];
                if (rand >= current.minChance && rand < current.maxChance)
                {
                    return current.Type;
                }
            }

            return null;
        }
    }
}
