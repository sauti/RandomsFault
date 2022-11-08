using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Default {
    [Serializable]
    public class Gem
    {
        [SerializeField]
        public CardId cardId;

        [SerializeField]
        public GameObject gemPrefab;

        [SerializeField]
        public Color color;
    }

    public class GemsController : MonoBehaviour
    {
        public List<Gem> gemsConfig;
        public GameObject goalSlotsGo;
        public GameObject bagSlotsGo;

        private Dictionary<int, List<GameObject>> slots = new Dictionary<int, List<GameObject>>();
        private Dictionary<int, List<GameObject>> bagSlots = new Dictionary<int, List<GameObject>>();

        private List<CardId> goal = new List<CardId>();

        void Start()
        {
            saveSlots(goalSlotsGo, slots);
            saveSlots(bagSlotsGo, bagSlots);

            pickUniqueColorsForSlots();

            InstantiateGoalSlots(slots);
            InstantiateGems(bagSlots);
        }

        public bool isInGoal(CardId cardId)
        {
            return goal.IndexOf(cardId) > -1;
        }

        public void PickUpGem(CardId cardId)
        {
            int indexInGoal = goal.IndexOf(cardId);
            if (indexInGoal == -1) {
                Debug.Log("Cant pick up this gem: " + cardId);
                return;
            }

            var slots = bagSlots[indexInGoal];
            var i = 0;
            while (i < slots.Count) {
                i++;
                int randomIndex = UnityEngine.Random.Range(0, slots.Count);
                GameObject slot = slots[randomIndex];
                if (!slot.activeSelf) {
                    slot.SetActive(true);
                    return;
                }
            }
        }

        private void pickUniqueColorsForSlots()
        {
            foreach (var slot in slots)
            {
                int i = UnityEngine.Random.Range(0, gemsConfig.Count);
                while (goal.IndexOf(gemsConfig[i].cardId) > -1) {
                    i = UnityEngine.Random.Range(0, gemsConfig.Count);
                }
                goal.Add(gemsConfig[i].cardId);
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

        private void InstantiateGoalSlots(Dictionary<int, List<GameObject>> _slots) {
            foreach(var item in _slots)
            {
                CardId cardId = goal[item.Key];
                Gem gem = getGemByCardId(cardId);
                foreach (var slot in item.Value)
                {
                    // todo instantiate gems
                    // var go = Instantiate(gem.gemPrefab, slot.transform);
                    // go.layer = LayerMask.NameToLayer("Web");
                    slot.GetComponent<Renderer>().material.SetColor("_Color", gem.color);
                }
            }
        }

        private void InstantiateGems(Dictionary<int, List<GameObject>> _slots) {
            foreach(var item in _slots)
            {
                CardId cardId = goal[item.Key];
                Gem gem = getGemByCardId(cardId);
                foreach (var slot in item.Value)
                {
                    var go = Instantiate(gem.gemPrefab, slot.transform);
                    go.layer = LayerMask.NameToLayer("Web");
                    slot.SetActive(false);
                }
            }
        }

        private Gem getGemByCardId(CardId cardId)
        {
            return gemsConfig.Find(g => g.cardId == cardId);
        }
    }
}
