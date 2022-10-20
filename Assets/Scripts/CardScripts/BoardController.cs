using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Default {
    public class BoardController : MonoBehaviour
    {
        public CardGenerator _cg;

        protected BoardView _view;
        protected PlayerStats _playerStats;

        protected List<CardData> _cards;
        protected bool[,] _cells;
        protected int _level;
        private CardCloseupController _closeupController;

        void Start()
        {
            _view = gameObject.GetComponent<BoardView>();
            _playerStats = GameObject.Find("Player").GetComponent<PlayerStats>();
            _closeupController = GameObject.Find("CloseupController").GetComponent<CardCloseupController>();
        }

        public void OnLongClick(RaycastHit hit)
        {
            for (var i = 0; i < _cards.Count; i++) {
                if (_cards[i].Id != hit.transform.name)
                continue;
                
                _view.LongClickCard(_cards[i]);
                _closeupController.Open(_cards[i]);
            }
        }

        protected void RemoveCard(CardData card) {
            _cards.Remove(card);
            _cells[card.Coord.x, card.Coord.y] = false;
            _view.removeCard(card);
        }
    }
}
