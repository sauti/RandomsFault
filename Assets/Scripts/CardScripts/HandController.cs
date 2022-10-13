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

        void Start()
        {
            _view = gameObject.GetComponent<BoardView>();
        }

        public void initCards(Vector2Int grid)
        {
            _cells = new bool[grid.x, grid.y];
            _cards = new List<CardData>();

            CardData card = _cg.GenerateCardByType(CardType.Attack, new Vector2Int(0, 0), "Hand", true);
            _cards.Add(card);
            _cells[0, 0] = true;
            _view.GenerateBoard(grid, _cards);
        }

        public void OnClickListener(RaycastHit hit) {
            for (var i = 0; i < _cards.Count; i++) {
                if (_cards[i].Name != hit.transform.name)
                continue;

                SelectCard(_cards[i]);
                break;
            }
        }

        private void SelectCard(CardData card)
        {
            _view.selectCard(card);
        }
    }
}
