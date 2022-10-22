using System;
using UnityEngine;

namespace Default
{
    [Serializable]
    public class CellConfig
    {
        [SerializeField] 
        private CellType _type;

        [SerializeField] 
        private GameObject _prefab;

        public CellType Type => _type;
        public GameObject Prefab => _prefab;
    }
}