using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Default {
    public class TableController : MonoBehaviour
    {
        public CardGenerator _cg;

        private BoardView _view;
        private PlayerStats _playerStats;

        private List<CardData> _cards;
        private bool[,] _cells;

        void Start()
        {
            _view = gameObject.GetComponent<BoardView>();
            _playerStats = GameObject.Find("Player").GetComponent<PlayerStats>();
        }

        void Update()
        {
        }

        public void initCards(Vector2Int grid)
        {
            _cells = new bool[grid.x, grid.y];
            _cards = new List<CardData>();
            _view = gameObject.GetComponent<BoardView>();

            int maxCardsAmount = grid.x * grid.y;
            int amount = Random.Range(2, maxCardsAmount);

            for (var i = 0; i < amount; i++) {
                Vector2Int coord = FindRandomEmptyCoordOnTable();
                CardData card = _cg.GenerateRandomCard(coord);
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
                    RotateCard(_cards[i], i);
                    break;
                }

                if (_cards[i].Card.CanPickUp) {
                    transform.parent.GetComponent<CardGameController>().TryPickUp(_cards[i]);
                    break;
                }

                if (_cards[i].Card.CanKill) {
                    transform.parent.GetComponent<CardGameController>().TryAttack(_cards[i]);
                    break;
                }
            
                if (_cards[i].Card.Type == CardType.Exit) {
                    Debug.Log("Click Exit");
                    initCards(new Vector2Int(_cells.GetLength(0), _cells.GetLength(1)));
                    break;
                }
            }
        }

        private void RotateCard(CardData card, int i) {
            Debug.Log("Rotate " + _cards[i].Card.Type + "  index: " + i);
            if (card.Card.Type == CardType.Trap) {
                _playerStats.GetDamage(card.Card.Damage);
            }
            _cards[i].IsRotated = true;
            _view.rotateCard(card);
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
                _view.AttackCard(card);
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

        private void RemoveCard(CardData card) {
            _cards.Remove(card);
            _cells[card.Coord.x, card.Coord.y] = false;
            _view.removeCard(card);
        }
    }
}
