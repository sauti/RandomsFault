using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Default {
    public class CardGameController : MonoBehaviour
    {
        public Vector2Int _gridSize = new Vector2Int(4, 3);
        public Vector2Int _handSize = new Vector2Int(4, 2);
        public GameObject _tableGo;
        public GameObject _handGo;

        private TableController _tableCtrl;
        private HandController _handCtrl;
        
        private BattleState state = BattleState.Start;

        void Start()
        {
            _tableCtrl = _tableGo.GetComponent<TableController>();
            _handCtrl = _handGo.GetComponent<HandController>();
            _tableCtrl.initCards(_gridSize);
            _handCtrl.initCards(_handSize);

            state = BattleState.PlayerTurn;
        }

        void Update()
        {
            if (state == BattleState.PlayerTurn) {
                OnClickListener();
            }
        }
        
        public void OnClickListener() {
            if (Input.GetMouseButtonDown(0)) {  
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);  
                RaycastHit hit;  
                if (!Physics.Raycast(ray, out hit)) {
                    return;
                }  

                _handCtrl.OnClickListener(hit);
                _tableCtrl.OnClickListener(hit);
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
            if (selectedCard == null) {
                return;
            }
            _tableCtrl.Attack(card, selectedCard.Card.Damage);
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
            Debug.Log("Enemy turn...");
            yield return _tableCtrl.AttackPlayerWithOpenCards();
            state = BattleState.PlayerTurn;
            Debug.Log("Your turn...");
        }
    }
}
