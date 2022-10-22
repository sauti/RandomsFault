using System.Collections.Generic;
using UnityEngine;

namespace Default
{
    [CreateAssetMenu(menuName = "Example/EntitiesConfig")]
    public class EntitiesConfig : ScriptableObject, ISerializationCallbackReceiver
    {
        [SerializeField] 
        private List<EntityView> _prefabs;

        private Dictionary<EntityType, EntityView> _lib;

        public void OnBeforeSerialize()
        {
        }

        public void OnAfterDeserialize()
        {
            _lib = new Dictionary<EntityType, EntityView>();
            foreach (var prefab in _prefabs)
            {
                _lib.Add(prefab.Type, prefab);
            }
        }

        public bool TryGet(EntityType type, out EntityView entityView)
        {
            return _lib.TryGetValue(type, out entityView);
        }
    }
}