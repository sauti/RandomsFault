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
        public int level = 1;

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

        protected override void OnClick(RaycastHit hit) {
            if (state == BattleState.PlayerTurn) {
                _handCtrl.OnClickListener(hit);
                _tableCtrl.OnClickListener(hit);
            }
        }

        protected override void OnLongClick(RaycastHit hit) {
            if (state == BattleState.PlayerTurn) {
                _handCtrl.OnLongClick(hit);
                _tableCtrl.OnLongClick(hit);
                state = BattleState.Closeup;
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

        public void SetPlayerTurn() {
            state = BattleState.PlayerTurn;
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
            state = BattleState.PlayerTurn;
            Debug.Log("Your turn...");
        }
    }
}
