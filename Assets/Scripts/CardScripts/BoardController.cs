using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Default {
    public class BoardController : MonoBehaviour
    {
        public CardGenerator _cg;

        protected BoardView _view;
        protected PlayerStats _playerStats;
        private CardInspectController _inspectController;

        protected List<CardData> _cards;
        protected bool[,] _cells;
        protected int _level;

        protected void Start()
        {
            _view = gameObject.GetComponent<BoardView>();
            _playerStats = GameObject.FindWithTag("Player").GetComponent<PlayerStats>();
            _inspectController = GameObject.Find("CardInspectController").GetComponent<CardInspectController>();
        }

        public void OnLongClick(RaycastHit hit)
        {
            for (var i = 0; i < _cards.Count; i++) {
                if (_cards[i].Id != hit.transform.name)
                continue;

                if (!_cards[i].IsRotated) {
                    return;
                }
                _inspectController.Open(_cards[i]);
            }
        }

        protected void RemoveCard(CardData card) {
            _cards.Remove(card);
            _cells[card.Coord.x, card.Coord.y] = false;
            _view.removeCard(card);
        }
    }
}
