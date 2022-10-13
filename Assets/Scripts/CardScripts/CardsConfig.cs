using System.Collections.Generic;
using UnityEngine;

namespace Default
{
    [CreateAssetMenu(menuName = "Cards/CardsConfig")]
    public class CardsConfig : ScriptableObject, ISerializationCallbackReceiver
    {
        [SerializeField] 
        private List<CardTemplate> _cards;

        private Dictionary<CardType, CardTemplate> _lib;

        public void OnBeforeSerialize()
        {
        }

        public void OnAfterDeserialize()
        {
            _lib = new Dictionary<CardType, CardTemplate>();
            foreach (var card in _cards)
            {
                _lib.Add(card.Type, card);
            }
        }

        public bool TryGet(CardType type, out CardTemplate card)
        {
            return _lib.TryGetValue(type, out card);
        }

        public CardTemplate GetByType(CardType type)
        {
            return _lib[type];
        }
    }
}