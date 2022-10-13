using System.Collections.Generic;
using UnityEngine;

namespace Default
{
    [CreateAssetMenu(menuName = "Cards/CardStatsConfig")]
    public class CardStatsConfig : ScriptableObject, ISerializationCallbackReceiver
    {
        [SerializeField] 
        private List<Texture2D> _textures;

        private Dictionary<int, Texture2D> _lib;

        public void OnBeforeSerialize()
        {
        }

        public void OnAfterDeserialize()
        {
            _lib = new Dictionary<int, Texture2D>();
            for (int i = 0; i < _textures.Count; i++)
            {
                _lib.Add(i, _textures[i]);
            }
        }

        public bool TryGet(int i, out Texture2D texture)
        {
            return _lib.TryGetValue(i, out texture);
        }
    }
}