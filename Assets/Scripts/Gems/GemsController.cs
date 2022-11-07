using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Default {
    public class GemsController : MonoBehaviour
    {
        public List<GameObject> gemsGo;

        public GameObject goalSlotsGo;
        public GameObject bagSlotsGo;

        private Dictionary<int, List<GameObject>> slots = new Dictionary<int, List<GameObject>>();
        private Dictionary<int, List<GameObject>> bagSlots = new Dictionary<int, List<GameObject>>();

        private List<CardId> pickedUpGems = new List<CardId>();
        private List<int> goalGems = new List<int>();

        public List<CardId> allGemIds = new List<CardId>();

        void Start()
        {
            saveSlots(goalSlotsGo, slots);
            saveSlots(bagSlotsGo, bagSlots);

            pickUniqueColorsForSlots();

            InstantiateGems(slots, true);
            InstantiateGems(bagSlots, false);
        }

        public void PickUpGem(CardId cardId)
        {
            int gemIndex = allGemIds.FindIndex(x => x == cardId);
            int indexInGoal = goalGems.FindIndex(x => x == gemIndex);

            if (indexInGoal == -1) {
                Debug.Log("Cant pick up this gem");
                return;
            }

            var slots = bagSlots[indexInGoal];
            var i = 0;
            while (i < slots.Count) {
                i++;
                int randomIndex = Random.Range(0, slots.Count);
                Debug.Log("try put into index " + randomIndex);
                GameObject slot = slots[randomIndex];
                if (!slot.activeSelf) {
                    Debug.Log("Done put " + randomIndex);
                    slot.SetActive(true);
                    return;
                }
            }
        }

        private void pickUniqueColorsForSlots()
        {
            foreach (var slot in slots)
            {
                int randomIndex = Random.Range(0, allGemIds.Count);
                while (goalGems.IndexOf(randomIndex) > -1) {
                    randomIndex = Random.Range(0, allGemIds.Count);
                }
                Debug.Log("Add random Index " + randomIndex);
                goalGems.Add(randomIndex);
            }
        }

        private void saveSlots(GameObject _slotsParent, Dictionary<int, List<GameObject>> _targetList)
        {
            int i = 0;
            foreach (Transform childSlots in _slotsParent.transform)
            {
                List<GameObject> list = new List<GameObject>();
                foreach (Transform slot in childSlots.transform)
                {
                    list.Add(slot.gameObject);
                }
                _targetList.Add(i, list);
                i++;
            }
        }

        private void InstantiateGems(Dictionary<int, List<GameObject>> _slots, bool active) {
            foreach(var item in _slots)
            {
                int goalGemIndex = goalGems[item.Key];
                foreach (var slot in item.Value)
                {
                    var go = Instantiate(gemsGo[goalGemIndex], slot.transform);
                    go.layer = LayerMask.NameToLayer("Web");
                    slot.SetActive(active);
                }
            }
        }
    }
}
