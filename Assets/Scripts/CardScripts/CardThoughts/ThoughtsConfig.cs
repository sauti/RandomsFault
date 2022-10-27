using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Default
{
    // [CreateAssetMenu(menuName = "Cards/ThoughtsConfig")]
    public class ThoughtsConfig: ScriptableObject, ISerializationCallbackReceiver
    {
        private Dictionary<CardId, List<Thought>> list;
        private List<Thought> defaultList = new DefaultThoughts().GetList();

        public void OnBeforeSerialize() {}

        public void OnAfterDeserialize()
        {
            list = new Dictionary<CardId, List<Thought>>();

            list.Add(CardId.Protein, new ProteinThoughts().GetList());
            list.Add(CardId.Scratch, new ScratchThoughts().GetList());
        }

        public List<Thought> getListByCardId(CardId cardId) {
            if (list.TryGetValue(cardId, out List<Thought> value))
            {
                return value;
            }
            return defaultList;
        }

        public List<Thought> getDefaultList() {
            return defaultList;
        }
    }
}
