using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Default {
    public class HandController : MonoBehaviour
    {
        public CardGenerator _cg;

        private BoardView _view;

        private List<CardData> _cards;
        private bool[,] _cells;
        private CardData _selectedCard;

        void Start()
        {
            _view = gameObject.GetComponent<BoardView>();
        }

        public void initCards(Vector2Int grid)
        {
            _cells = new bool[grid.x, grid.y];
            _cards = new List<CardData>();

            CardData card = _cg.GenerateCardByType(CardType.Attack, new Vector2Int(0, 0), true);
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
            Debug.Log("Pick up: " + card.Coord.x + " " + card.Coord.y);
            _cards.Add(card);
            _cells[coord.x, coord.y] = true;
            _view.addCard(card);
        }

        public void OnClickListener(RaycastHit hit)
        {
            for (var i = 0; i < _cards.Count; i++) {
                if (_cards[i].Id != hit.transform.name)
                continue;

                SelectCard(_cards[i]);
                break;
            }
        }

        public CardData GetSelectedCard()
        {
            return _selectedCard;
        }

        private void SelectCard(CardData card)
        {
            if (_selectedCard != null && _selectedCard.Id == card.Id) {
                _view.resetSelection();
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
