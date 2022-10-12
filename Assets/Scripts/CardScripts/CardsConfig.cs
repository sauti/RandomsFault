using System.Collections.Generic;
using UnityEngine;

namespace Default
{
    [CreateAssetMenu(menuName = "Example/CardsConfig")]
    public class CardsConfig : ScriptableObject, ISerializationCallbackReceiver
    {
        [SerializeField] 
        private List<Card> _cards;

        private Dictionary<CardType, Card> _lib;

        public void OnBeforeSerialize()
        {
        }

        public void OnAfterDeserialize()
        {
            _lib = new Dictionary<CardType, Card>();
            foreach (var card in _cards)
            {
                _lib.Add(card.Type, card);
            }
        }

        public bool TryGet(CardType type, out Card card)
        {
            return _lib.TryGetValue(type, out card);
        }

        public Card GetByType(CardType type)
        {
            return _lib[type];
        }
    }
}