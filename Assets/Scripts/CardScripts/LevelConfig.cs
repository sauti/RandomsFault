using System.Collections.Generic;
using UnityEngine;

namespace Default
{
    [CreateAssetMenu(menuName = "Cards/LevelConfig")]
    public class LevelConfig : ScriptableObject
    {
        [SerializeField] 
        private int minCards;

        [SerializeField] 
        private int maxCards;

        public int GetCardsAmount() {
            return Random.Range(minCards, maxCards);
        }

        // private Dictionary<int, Texture2D> _lib;

        // public void OnBeforeSerialize()
        // {
        // }

        // public void OnAfterDeserialize()
        // {
        //     _lib = new Dictionary<int, Texture2D>();
        //     for (int i = 0; i < _textures.Count; i++)
        //     {
        //         _lib.Add(i, _textures[i]);
        //     }
        // }

        // public bool TryGet(int i, out Texture2D texture)
        // {
        //     return _lib.TryGetValue(i, out texture);
        // }
    }
}