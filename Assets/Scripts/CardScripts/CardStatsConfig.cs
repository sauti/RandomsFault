using System.Collections.Generic;
using UnityEngine;

namespace Default
{
    [CreateAssetMenu(menuName = "Cards/CardStatsConfig")]
    public class CardStatsConfig : ScriptableObject, ISerializationCallbackReceiver
    {
        [SerializeField] 
        private List<Texture2D> _dmgTextures;

        [SerializeField] 
        private List<Texture2D> _healthTextures;

        private Dictionary<int, Texture2D> _dmgLib;
        private Dictionary<int, Texture2D> _healthLib;

        public void OnBeforeSerialize()
        {
        }

        public void OnAfterDeserialize()
        {
            _dmgLib = new Dictionary<int, Texture2D>();
            for (int i = 0; i < _dmgTextures.Count; i++)
            {
                _dmgLib.Add(i, _dmgTextures[i]);
            }

            _healthLib = new Dictionary<int, Texture2D>();
            for (int i = 0; i < _healthTextures.Count; i++)
            {
                _healthLib.Add(i, _healthTextures[i]);
            }
        }

        public bool TryGet(int i, string name, out Texture2D texture)
        {
            if (name == "Health") {
                return _healthLib.TryGetValue(i, out texture);
            }

            return _dmgLib.TryGetValue(i, out texture);
        }
    }
}