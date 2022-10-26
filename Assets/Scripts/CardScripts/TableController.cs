using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Default {
    public class TableController : BoardController
    {
        public GameObject mainCamera;
        public GameObject cardGame;
        public GameObject Swipe;
        private MapGenerator mapG;
        private GameState gameState;

        void OnEnable()
        {
            gameState = GameObject.Find("GameManager").GetComponent<GameState>();
            initCards(new Vector2Int(_cells.GetLength(0), _cells.GetLength(1)));
        }

        public void initCards(Vector2Int grid)
        {
            // _level = gameState.getLevel(); // TODO
            _level = 0;
            _cells = new bool[grid.x, grid.y];
            _cards = new List<CardData>();
            _view = gameObject.GetComponent<BoardView>();

            _cg.Init(_level, gameState.getCurrentEntity());
            var cards = _cg.GenerateCardsForLevel();
            foreach (CardData card in cards)
            {
                Vector2Int coord = FindRandomEmptyCoordOnTable();
                card.Coord = coord;
                _cells[coord.x, coord.y] = true;
                _cards.Add(card);
            }

            _view.GenerateBoard(grid, _cards);
        }

        public void OnClickListener(RaycastHit hit) {
            for (var i = 0; i < _cards.Count; i++) {
                if (_cards[i].Id != hit.transform.name)
                continue;

                if (!_cards[i].IsRotated) {
                    transform.parent.GetComponent<CardGameController>().FlipCard(_cards[i]);
                    break;
                }

                if (_cards[i].Card.CanPickUp) {
                    transform.parent.GetComponent<CardGameController>().TryPickUp(_cards[i]);
                    break;
                }

                if (_cards[i].Card.CanBeKilled) {
                    transform.parent.GetComponent<CardGameController>().TryAttack(_cards[i]);
                    break;
                }
            
                if (_cards[i].Card.CardId == CardId.Exit) {
                    Debug.Log("Click Exit");
                    OnCardGameExit();
                    break;
                }
            }
        }

        public void FlipCard(CardData card) {
            Debug.Log("Flip " + card.Card.CardId);
            card.IsRotated = true;
            _view.FlipCard(card);
            if (card.Card.IsTrap) {
                _playerStats.GetDamage(card.Card.Damage);
            }
        }

        public void PickUp(CardData card)
        {
            RemoveCard(card);
        }

        // player attacks a card on the table
        public void Attack(CardData card, int damage) {
            Debug.Log("Attack " + damage);
            card.Card.SetHealth(card.Card.Health - damage);
            if (card.Card.Health <= 0) {
                RemoveCard(card);
            } else {
                _view.DealDamageToCard(card);
            }
        }

        // cards attack a player
        public IEnumerator AttackPlayerWithOpenCards() {
            List<CardData> enemyCards = _cards.FindAll(card => card.IsRotated && card.Card.CanBeKilled);
            Debug.Log("Found enemy cards:" + enemyCards.Count);
            if (enemyCards.Count > 0) {
                yield return new WaitForSeconds(0.5f);
                foreach (CardData card in _cards) {
                    if (!card.IsRotated || !card.Card.CanBeKilled) continue;
                    _playerStats.GetDamage(card.Card.Damage);
                    yield return _view.DealDamageWithCard(card);
                }
                Debug.Log("Done all damage");
            }
        }

        private Vector2Int FindRandomEmptyCoordOnTable()
        {
            while (true)
            {
                var coord = new Vector2Int(
                    Random.Range(0, _cells.GetLength(0)),
                    Random.Range(0, _cells.GetLength(1))
                );
                if (_cells[coord.x, coord.y] == false)
                    return coord;
            }
        }

        public void OnCardGameExit()
        {
            Debug.Log("Exit Card Game");        
            mainCamera.SetActive(true);
            cardGame.gameObject.SetActive(false);
            Swipe.gameObject.SetActive(true);
        }
    }
}
