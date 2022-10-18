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
            
            List<CardId> types = GenerateRequiredCardTypes(conf, amount);
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

        public CardData GenerateCardByType(CardId cardId, Vector2Int coord, bool IsRotated = false) {
            CardTemplate template = cardsConfig.GetByType(cardId);
            CardPerLevelData cardLevelData = _levelConfig.GetCardConfig(cardId);

            var card = new Card()
            {
                CardId = template.CardId,
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

        private List<CardId> GenerateRequiredCardTypes(List<CardPerLevelData> list, int amount) {
            List<CardId> types = new List<CardId>();
            for (int i = 0; i < list.Count; i++)
            {
                CardPerLevelData current = list[i];
                if (current.MinAmount > 0) {
                    for (int j = 0; j < current.MinAmount; j++) {
                        types.Add(current.CardId);
                    }
                }
            }

            if (types.Count > amount) {
                throw new ArgumentOutOfRangeException("Can't generate more cards with minAmount than total cards for this level.");
            }

            return types;
        }

        private List<CardId> GetRandomTypesByChance(List<CardPerLevelData> list, int amount) {
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

            List<CardId> cardIds = new List<CardId>();
            for (var i = 0; i < amount; i++) {
                CardId? cardId = GetRandomTypeByChance(list, chanceSum);
                if (cardId is CardId) {
                    cardIds.Add((CardId)cardId);
                }
            }

            return cardIds;
        }

        private CardId? GetRandomTypeByChance(List<CardPerLevelData> list, int chanceSum)
        {
            int rand = UnityEngine.Random.Range(0, chanceSum);
            for (int i = 0; i < list.Count; i++)
            {
                CardPerLevelData current = list[i];
                if (rand >= current.minChance && rand < current.maxChance)
                {
                    return current.CardId;
                }
            }

            return null;
        }
    }
}
