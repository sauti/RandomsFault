using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Default {
    public class HandController : BoardController
    {
        private CardData _selectedCard;
        private GemsController gems;

        new void Start() {
            base.Start();
            gems = GameObject.Find("Gems").GetComponent<GemsController>();
        }

        public void OnClick(RaycastHit hit)
        {
            for (var i = 0; i < _cards.Count; i++) {
                if (_cards[i].Id != hit.transform.name)
                continue;

                SelectCard(_cards[i]);
                break;
            }
        }

        public void initCards(Vector2Int grid)
        {
            _cells = new bool[grid.x, grid.y];
            _cards = new List<CardData>();
            _view.GenerateBoard(grid, _cards);
        }

        public bool CanPickUp(CardData card) 
        {
            if (card.Card.IsGem) {
                return gems.isInGoal(card.Card.CardId);
            } else {
                Vector2Int coord = FindEmptyCoordInHand();
                return coord.x != -1;
            }
        }

        public void PickUp(CardData card)
        {
            if (card.Card.IsGem) {
                return;
            }
            Vector2Int coord = FindEmptyCoordInHand();
            card.Coord = coord;
            Debug.Log("Pick up: " + card.Card.CardId);
            _cards.Add(card);
            _cells[coord.x, coord.y] = true;
            _view.addCard(card);
        }

        public CardData GetSelectedCard()
        {
            return _selectedCard;
        }

        public void DeselectCard()
        {
            _view.resetSelection();
            _selectedCard = null;
        }

        public void Attack()
        {
            if (_selectedCard == null) {
                return;
            }
            if (_selectedCard.Card.Health > 1) {
                _selectedCard.Card.Health -= 1;
                _view.DealDamageToCard(_selectedCard);
            } else {
                RemoveSelectedCard();
            }
        }

        public void RemoveSelectedCard() {
            if (_selectedCard == null) {
                return;
            }
            base.RemoveCard(_selectedCard);
            _selectedCard = null;
        }

        public void UseCard() {
            if (_selectedCard == null) {
                return;
            }
        }

        public void UseHealCard() {
            if (_selectedCard != null && _selectedCard.Card.CanHeal) {
                _playerStats.Heal(_selectedCard.Card.Health);
                RemoveSelectedCard();
            }
        }

        private void SelectCard(CardData card)
        {
            if (_selectedCard != null && _selectedCard.Id == card.Id) {
                DeselectCard();
            } else {
                _selectedCard = card;
                _view.selectCard(card);
            }
        }

        private Vector2Int FindEmptyCoordInHand()
        {
            int rows = _cells.GetLength(1);
            int cols = _cells.GetLength(0);
            for (int j = 0; j < rows; j++)
            {
                for (int i = 0; i < cols; i++)
                {
                    // ignore last cell in hand
                    if (j == rows - 1 && i == cols -1) {
                        return new Vector2Int(-1, -1);
                    }

                    if (_cells[i, j] == false) {
                        return new Vector2Int(i, j);
                    }
                }
            }
            return new Vector2Int(-1, -1);
        }
    }
}
