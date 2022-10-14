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
            var cards = new List<CardData>();

            for (var i = 0; i < amount; i++) {
                CardData card = GenerateRandomCard(new Vector2Int(0, 0));
                cards.Add(card);
            }

            return cards;
        }

        public CardData GenerateRandomCard(Vector2Int coord) {
            CardType type = GetRandomCardType();
            return GenerateCardByType(type, coord);
        }

        public CardData GenerateCardByType(CardType type, Vector2Int coord, bool IsRotated = false) {
            CardTemplate template = cardsConfig.GetByType(type);
            CardPerLevelData cardLevelData = _levelConfig.GetCardConfig(type);

            var card = new Card()
            {
                Type = template.Type,
                Prefab = template.Prefab,
                Health = cardLevelData != null ? cardLevelData.Health : 1,
                Damage = cardLevelData != null ? cardLevelData.Damage : 1,
                CanPickUp = template.CanPickUp,
                CanKill = template.CanKill,
                CanHeal = template.CanHeal,
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

        private CardType GetRandomCardType()
        {
            return (CardType)Random.Range(0, System.Enum.GetValues(typeof(CardType)).Length);
        }

        private string GetUuid()
        {
            return System.Guid.NewGuid().ToString("N");
        }
    }
}
