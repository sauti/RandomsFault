using System.Collections.Generic;
using UnityEngine;

namespace Default
{
    [CreateAssetMenu(menuName = "Cards/LevelsList")]
    public class LevelsList : ScriptableObject
    {
        [SerializeField] 
        private List<LevelConfig> list;

        public LevelConfig GetConfigForLevel(int level) {
            return list[level];
        }
    }
}