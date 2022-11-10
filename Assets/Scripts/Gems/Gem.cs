using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Default {
    [Serializable]
    public class Gem
    {
        [SerializeField]
        public CardId cardId;

        [SerializeField]
        public GameObject gemPrefab;

        [SerializeField]
        public Color color;
    }
}
