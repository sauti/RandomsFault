using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Default {
    public class CardGameController : MonoBehaviour
    {
        public Vector2Int gridSize = new Vector2Int(4, 3);
        public Vector2Int handSize = new Vector2Int(4, 2);
        public GameObject tableGo;
        public GameObject handGo;
        public int level = 1;
        bool clicking = false;
        float totalDownTime = 0;
        public float ClickDuration = 1;

        private TableController _tableCtrl;
        private HandController _handCtrl;
        
        private BattleState state = BattleState.Start;

        void Start()
        {
            _tableCtrl = tableGo.GetComponent<TableController>();
            _handCtrl = handGo.GetComponent<HandController>();
            _tableCtrl.initCards(gridSize, level);
            _handCtrl.initCards(handSize);

            state = BattleState.PlayerTurn;
        }

        void Update()
        {
            if (state == BattleState.PlayerTurn) {
                OnClickListener();
            }
        }
        
        public void OnClickListener() {
            if (Input.GetMouseButtonDown(0))
            {
                totalDownTime = 0;
                clicking = true;
            }

            if (clicking && Input.GetMouseButton(0))
            {
                totalDownTime += Time.deltaTime;
                if (totalDownTime >= ClickDuration)
                {
                    Debug.Log("Long click");
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);  
                    RaycastHit hit;  
                    if (!Physics.Raycast(ray, out hit)) {
                        return;
                    }  

                    _handCtrl.OnLongClick(hit);
                    _tableCtrl.OnLongClick(hit);

                    clicking = false;
                }
            }

            if (clicking && Input.GetMouseButtonUp(0))
            {
                if (totalDownTime < ClickDuration)
                {
                    Debug.Log("Short click");
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);  
                    RaycastHit hit;  
                    if (!Physics.Raycast(ray, out hit)) {
                        return;
                    }  

                    _handCtrl.OnClickListener(hit);
                    _tableCtrl.OnClickListener(hit);
                    clicking = false;
                }
            }
        }

        public void TryPickUp(CardData card) {
            if (_handCtrl.CanPickUp()) {
                _tableCtrl.PickUp(card);
                _handCtrl.PickUp(card);
                TriggerEnemyTurn();
            }
        }

        public void TryAttack(CardData card) {
            CardData selectedCard = _handCtrl.GetSelectedCard();
            if (selectedCard == null || !selectedCard.Card.IsWeapon) {
                return;
            }
            _tableCtrl.Attack(card, selectedCard.Card.Damage);
            _handCtrl.RemoveSelectedCard();
            TriggerEnemyTurn();
        }

        public void RotateCard(CardData card) {
            _tableCtrl.RotateCard(card);
            TriggerEnemyTurn();
        }

        private void TriggerEnemyTurn() {
            StartCoroutine(StartEnemyTurn());
        }

        private IEnumerator StartEnemyTurn() {
            state = BattleState.EnemyTurn;
            _handCtrl.DeselectCard();
            Debug.Log("Enemy turn...");
            yield return _tableCtrl.AttackPlayerWithOpenCards();
            state = BattleState.PlayerTurn;
            Debug.Log("Your turn...");
        }
    }
}
