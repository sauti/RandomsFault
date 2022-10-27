using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Default {
    public class HandController : BoardController
    {
        private CardData _selectedCard;

        public void OnClickListener(RaycastHit hit)
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
            _view = gameObject.GetComponent<BoardView>();
            _cells = new bool[grid.x, grid.y];
            _cards = new List<CardData>();

            // _cg.Init(0);
            CardData card = _cg.GenerateCardByType(CardId.Scratch, new Vector2Int(0, 0), true);
            _cards.Add(card);
            _cells[0, 0] = true;
            _view.GenerateBoard(grid, _cards);
        }

        public bool CanPickUp() 
        {
            Vector2Int coord = FindEmptyCoordInHand();
            return coord.x != -1;
        }

        public void PickUp(CardData card)
        {
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
            
            Debug.Log(_selectedCard.Card.CanHeal);
            if (_selectedCard.Card.CanHeal) {
                UseHealCard();
            }
        }

        private void UseHealCard() {
            Debug.Log("Heal: " + _selectedCard.Card.Health);
            _playerStats.Heal(_selectedCard.Card.Health);
            RemoveSelectedCard();
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
            for (int j = 0; j < _cells.GetLength(1); j++)
            {
                for (int i = 0; i < _cells.GetLength(0); i++)
                {
                    if (_cells[i, j] == false) {
                        return new Vector2Int(i, j);
                    }
                }
            }
            return new Vector2Int(-1, -1);
        }
    }
}
