using System.Collections.Generic;
using UnityEngine;

namespace Default
{
    [CreateAssetMenu(menuName = "Cards/CardsConfig")]
    public class CardsConfig : ScriptableObject, ISerializationCallbackReceiver
    {
        [SerializeField] 
        private List<CardTemplate> _cards;

        private Dictionary<CardId, CardTemplate> _lib;

        public void OnBeforeSerialize()
        {
        }

        public void OnAfterDeserialize()
        {
            _lib = new Dictionary<CardId, CardTemplate>();
            foreach (var card in _cards)
            {
                _lib.Add(card.CardId, card);
            }
        }

        public bool TryGet(CardId cardId, out CardTemplate card)
        {
            return _lib.TryGetValue(cardId, out card);
        }

        public CardTemplate GetByType(CardId cardId)
        {
            return _lib[cardId];
        }
    }
}