using System.Collections.Generic;
using UnityEngine;

namespace Default
{
    [CreateAssetMenu(menuName = "Cards/LevelsList")]
    public class LevelsList : ScriptableObject
    {
        [SerializeField] 
        private List<LevelConfig> ruinsList;

        [SerializeField] 
        private List<LevelConfig> chestsList;

        public LevelConfig GetRuinsConfigForLevel(int level) {
            return ruinsList[level];
        }

        public LevelConfig GetChestsConfigForLevel(int level) {
            return chestsList[level];
        }
    }
}