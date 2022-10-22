using System.Collections.Generic;
using UnityEngine;

namespace Default
{
    
    [CreateAssetMenu(menuName = "Example/CellsConfig")]
    public class CellsConfig : ScriptableObject, ISerializationCallbackReceiver
    {
        [SerializeField] 
        private List<CellConfig> _configs;

        private Dictionary<CellType, GameObject> _lib;

        public void OnBeforeSerialize()
        {
        }

        public void OnAfterDeserialize()
        {
            _lib = new Dictionary<CellType, GameObject>();
            foreach (var config in _configs)
            {
                _lib.Add(config.Type, config.Prefab);
            }
        }

        public bool TryGet(CellType type, out GameObject prefab)
        {
            return _lib.TryGetValue(type, out prefab);
        }
    }
}