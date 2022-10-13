using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Default {
    public class CardGenerator: MonoBehaviour
    {
        [SerializeField]
        private CardsConfig _cardsConfig;

        public CardData GenerateRandomCard(Vector2Int coord) {
            CardType type = GetRandomCardType();
            return GenerateCardByType(type, coord);
        }

        public CardData GenerateCardByType(CardType type, Vector2Int coord, bool IsRotated = false) {
            CardTemplate template = _cardsConfig.GetByType(type);
            var card = new Card()
            {
                Type = template.Type,
                Prefab = template.Prefab,
                Health = template.Health,
                Damage = template.Damage,
                CanPickUp = template.CanPickUp,
                CanKill = template.CanKill,
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
