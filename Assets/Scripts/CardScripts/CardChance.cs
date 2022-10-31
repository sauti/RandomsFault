using System;
using System.Collections.Generic;
using UnityEngine;

namespace Default
{
    [Serializable]
    public class CardChanceBase
    {
        [SerializeField] 
        private CardId _cardId;

        [SerializeField]
        public int _chance;

        [HideInInspector]
        public int minChance;

        [HideInInspector]
        public int maxChance;

        public CardId CardId => _cardId;
        public int Chance => _chance;
    }

    [Serializable]
    public class CardChance : CardChanceBase
    {
    }
}
