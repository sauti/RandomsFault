using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Default {
    public class GemsController : MonoBehaviour
    {
        public GameObject gem;
        
        public GameObject web;
        public GameObject slotsAParent;
        public GameObject slotsBParent;

        private List<GameObject> slotsA = new List<GameObject>();
        private List<GameObject> slotsB = new List<GameObject>();

        private List<CardId> pickedUpGems = new List<CardId>();

        void Start()
        {
            foreach (Transform child in slotsAParent.transform)
            {
                slotsA.Add(child.gameObject);
            }
            foreach (Transform child in slotsBParent.transform)
            {
                slotsB.Add(child.gameObject);
            }
        }

        public void PickUpGem(CardId cardId)
        {
            pickedUpGems.Add(cardId);
        }

        public void AddGemsToWeb()
        {
            Debug.Log("picked gems" + pickedUpGems.Count);
            if (pickedUpGems.Count == 0) {
                return;
            }
            web.SetActive(!web.activeSelf);
            foreach (CardId gem in pickedUpGems)
            {
                AddGem();
            }
            pickedUpGems.Clear();
        }

        public void AddGem()
        {
            int i = 0;
            while (i < slotsB.Count) {
                i++;
                int randomIndex = Random.Range(0, slotsB.Count);
                GameObject slot = slotsB[randomIndex];
                if (!slot.activeSelf) {
                    slot.SetActive(true);
                    return;
                }
            }
        }

        public void toggleWeb() {
            web.SetActive(!web.activeSelf);
        }
    }
}
