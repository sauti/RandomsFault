using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Default {
    public class GemsController : MonoBehaviour
    {
        public List<Gem> gemsConfig;
        public List<GameObject> goalOptionPrefabs;
        public GameObject goalParentGo;

        private GameObject goalSlotsGo;
        private Dictionary<int, List<GameObject>> slots = new Dictionary<int, List<GameObject>>();
        private List<CardId> goal = new List<CardId>();
        private List<Gem> pendingGems = new List<Gem>();

        void Start()
        {
            selectGoalForGame();
            saveSlots(goalSlotsGo, slots);
            pickUniqueColorsForSlots();

            InstantiateGoalSlots(slots);
        }

        private void selectGoalForGame()
        {
            int i = UnityEngine.Random.Range(0, goalOptionPrefabs.Count);
            GameObject go = Instantiate(goalOptionPrefabs[i], goalParentGo.transform);
            goalSlotsGo = go.transform.Find("Slots").gameObject;
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

            var _goalRow = slots[indexInGoal];
            Gem gem = getGemByCardId(cardId);
            pendingGems.Add(gem);

            // todo 
            InstantiatePendingGems();
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
                    slot.GetComponent<Renderer>().material.SetColor("_Color", gem.color);
                }
            }
        }

        private void InstantiatePendingGems() {
            foreach(Gem gem in pendingGems) {
                int indexInGoal = goal.IndexOf(gem.cardId);
                var row = slots[indexInGoal];
                if (row.Count == 0) {
                    Debug.Log("No free space for gem: " + gem.cardId);
                    continue;
                }
                
                int randomIndex = UnityEngine.Random.Range(0, row.Count);
                var slot = row[randomIndex];

                slot.GetComponent<Renderer>().material.color = Color.clear;
                slot.transform.localScale += new Vector3(0.3f, 0.3f, 0.3f);
                var go = Instantiate(gem.gemPrefab, slot.transform);
                go.layer = LayerMask.NameToLayer("Web");

                slots[indexInGoal].RemoveAt(randomIndex);
            }

            pendingGems.Clear();
        }

        private Gem getGemByCardId(CardId cardId)
        {
            return gemsConfig.Find(g => g.cardId == cardId);
        }
    }
}
