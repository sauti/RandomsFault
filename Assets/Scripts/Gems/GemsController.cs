using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Default {
    public class GemsController : MonoBehaviour
    {
        public List<GameObject> gemsGo;

        public GameObject web;
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

            var i = 0;
            foreach (Transform childSlots in bagSlotsParent.transform)
            {
                List<GameObject> list = new List<GameObject>();
                foreach (Transform slot in childSlots.transform)
                {
                    list.Add(slot.gameObject);
                }
                bagSlots.Add(i, list);
                i++;
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
            // for (int i = 0; i < slots.Count; i++) 
            // {
            //     List<GameObject> list = slots[i];
            //     foreach (GameObject slot in list)
            //     {
            //         int goalGemIndex = goalGems[i];
            //         Instantiate(gemsGo[goalGemIndex], slot.transform);
            //     }
            // }
        }

        public void PickUpGem(CardId cardId)
        {
            pickedUpGems.Add(cardId);
        }

        public void AddGemsToWeb()
        {
            // Debug.Log("picked gems" + pickedUpGems.Count);
            // if (pickedUpGems.Count == 0) {
            //     return;
            // }
            // web.SetActive(!web.activeSelf);
            // foreach (CardId gem in pickedUpGems)
            // {
            //     AddGem();
            // }
            // pickedUpGems.Clear();
        }

        public void AddGem()
        {
            // int i = 0;
            // while (i < slotsB.Count) {
            //     i++;
            //     int randomIndex = Random.Range(0, slotsB.Count);
            //     GameObject slot = slotsB[randomIndex];
            //     if (!slot.activeSelf) {
            //         slot.SetActive(true);
            //         return;
            //     }
            // }
        }

        public void toggleWeb() {
            web.SetActive(!web.activeSelf);
        }
    }
}
