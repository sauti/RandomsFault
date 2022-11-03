using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Default {
    public class GemsController : MonoBehaviour
    {
        public List<GameObject> gemsGo;

        public GameObject web;
        public GameObject bag;
        public GameObject slotsParent;
        public GameObject bagSlotsParent;

        private Dictionary<int, List<GameObject>> slots = new Dictionary<int, List<GameObject>>();
        private Dictionary<int, List<GameObject>> bagSlots = new Dictionary<int, List<GameObject>>();

        private List<CardId> pickedUpGems = new List<CardId>();
        private List<int> goalGems = new List<int>();

        private List<CardId> allGemIds = new List<CardId>
        {
            CardId.Gemstone_1,
            CardId.Gemstone_2,
            CardId.Gemstone_3,
            CardId.Gemstone_4,
            CardId.Gemstone_5,
        };

        void Start()
        {
            var i = 0;
            foreach (Transform childSlots in slotsParent.transform)
            {
                List<GameObject> list = new List<GameObject>();
                foreach (Transform slot in childSlots.transform)
                {
                    list.Add(slot.gameObject);
                }
                slots.Add(i, list);
                i++;
                int randomIndex = Random.Range(0, allGemIds.Count);
                goalGems.Add(randomIndex);
            }

            var j = 0;
            foreach (Transform childSlots in bagSlotsParent.transform)
            {
                List<GameObject> list = new List<GameObject>();
                foreach (Transform slot in childSlots.transform)
                {
                    list.Add(slot.gameObject);
                }
                bagSlots.Add(j, list);
                j++;
            }

            InstantiateGoalGems();
        }

        private void InstantiateGoalGems()
        {
            foreach(var item in slots)
            {
                int goalGemIndex = goalGems[item.Key];
                foreach (var slot in item.Value)
                {
                    Instantiate(gemsGo[goalGemIndex], slot.transform);
                }
            }

            foreach(var item in bagSlots)
            {
                int goalGemIndex = goalGems[item.Key];
                foreach (var slot in item.Value)
                {
                    Instantiate(gemsGo[goalGemIndex], slot.transform);
                    slot.SetActive(false);
                }
            }
        }

        // public void PickUpGem(CardId cardId)
        // {
        //     pickedUpGems.Add(cardId);
        // }

        // public void AddGemsToWeb()
        // {
        //     // Debug.Log("picked gems" + pickedUpGems.Count);
        //     // if (pickedUpGems.Count == 0) {
        //     //     return;
        //     // }
        //     // web.SetActive(!web.activeSelf);
        //     // foreach (CardId gem in pickedUpGems)
        //     // {
        //     //     AddGem();
        //     // }
        //     // pickedUpGems.Clear();
        // }

        public void PickUpGem(CardId cardId)
        {
            int gemIndex = allGemIds.FindIndex(x => x == cardId);
            int indexInGoal = goalGems.FindIndex(x => x == gemIndex);
            if (gemIndex == -1) {
                Debug.Log("Cant pick up this gem");
                return;
            }

            int i = 0;
            var slots = bagSlots[i];
            while (i < slots.Count) {
                i++;
                int randomIndex = Random.Range(0, slots.Count);
                GameObject slot = slots[randomIndex];
                if (!slot.activeSelf) {
                    slot.SetActive(true);
                    return;
                }
            }
        }

        public void toggleWeb() {
            bag.SetActive(false);
            web.SetActive(!web.activeSelf);
        }

        public void toggleBag() {
            web.SetActive(false);
            bag.SetActive(!bag.activeSelf);
        }
    }
}
