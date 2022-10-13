using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Default {
    public class CardGenerator: MonoBehaviour
    {
        [SerializeField]
        private CardsConfig _cardsConfig;

        public CardData GenerateRandomCard(Vector2Int coord, string name) {
            CardType type = GetRandomCardType();
            return GenerateCardByType(type, coord, name);
        }

        public CardData GenerateCardByType(CardType type, Vector2Int coord, string name, bool IsRotated = false) {
            Card card = _cardsConfig.GetByType(type) as Card;
            CardData data = new CardData()
            {
                Name = name + " " + type + " " + coord.x + " " + coord.y,
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
    }
}
