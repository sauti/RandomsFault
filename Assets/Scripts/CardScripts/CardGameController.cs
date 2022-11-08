using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Default {
    public class CardGameController : OnClickListener
    {
        public Vector2Int gridSize = new Vector2Int(4, 3);
        public Vector2Int handSize = new Vector2Int(4, 2);
        public GameObject tableGo;
        public GameObject handGo;

        private TableController _tableCtrl;
        private HandController _handCtrl;
        
        private BattleState state = BattleState.Start;

        void Start()
        {
            _tableCtrl = tableGo.GetComponent<TableController>();
            _handCtrl = handGo.GetComponent<HandController>();
            _tableCtrl.initCards(gridSize);
            _handCtrl.initCards(handSize);

            state = BattleState.PlayerTurn;
        }

        protected override void OnClick(RaycastHit hit) {
            if (state == BattleState.PlayerTurn) {
                _handCtrl.OnClick(hit);
                _tableCtrl.OnClick(hit);
            }
        }

        protected override void OnLongClick(RaycastHit hit) {
            if (state == BattleState.PlayerTurn) {
                _handCtrl.OnLongClick(hit);
                _tableCtrl.OnLongClick(hit);
            }
        }

        public void TryPickUp(CardData card) {
            if (_handCtrl.CanPickUp(card)) {
                StartCoroutine(_tableCtrl.PickUp(card));
                _handCtrl.PickUp(card);
                // TriggerEnemyTurn();
            } else {
                _tableCtrl.FailPickup(card);
            }
        }

        public void TryAttack(CardData card) {
            CardData selectedCard = _handCtrl.GetSelectedCard();
            if (selectedCard == null || !selectedCard.Card.IsWeapon) {
                return;
            }
            _tableCtrl.Attack(card, selectedCard.Card.Damage);
            _handCtrl.Attack();
            // TriggerEnemyTurn();
        }

        public void SetPlayerTurn() {
            state = BattleState.PlayerTurn;
        }

        public void SetInspectState() {
            state = BattleState.Inspect;
        }

        public void FlipCard(CardData card) {
            _tableCtrl.FlipCard(card);
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
            yield return _tableCtrl.SpawnTurnCards();
            state = BattleState.PlayerTurn;
            Debug.Log("Your turn...");
        }
    }
}
