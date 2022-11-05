using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Default {
    public class BoardView : MonoBehaviour
    {
        public GameObject parent;
        public GameObject cellPrefab;
        public GameObject cardPrefab;
        public Vector2 padding = new Vector2(0.6f, 0.9f);

        private List<GameObject> _cards = new List<GameObject>();
        private GameObject[,] _cells;

        private GameUI UI;

        void Start()
        {
            UI = GameObject.Find("GameCanvas").GetComponent<GameUI>();
        }

        private void ClearBoard()
        {
            if (_cells != null) {
                for (var i = 0; i != _cells.GetLength(0); i++)
                {
                    for (var j = 0; j != _cells.GetLength(1); j++)
                    {
                        Destroy(_cells[i, j]);
                    }
                }
            }
            
            foreach (var card in _cards)
            {
                Destroy(card);
            }
        }

        public void GenerateBoard(Vector2Int gridSize, List<CardData> cards) {
            ClearBoard();
            GenerateCells(gridSize);
            GenerateCards(cards);
        }

        public void FlipCard(CardData card) {
            GameObject c = _findCard(card);
            c.GetComponent<CardView>().Flip();
        }

        public IEnumerator PickUpGem(CardData card) {
            GameObject c = _findCard(card);
            yield return c.GetComponent<CardView>().MoveGemToBag();
        }

        public void removeCard(CardData card) {
            GameObject c = _findCard(card);
            Destroy(c);
            _cards.Remove(c);
        }

        public void addCard(CardData card) {
            Vector2Int coord = card.Coord;
            GameObject go = Instantiate(cardPrefab, _cells[coord.x, coord.y].transform);
            go.GetComponent<CardView>().SetInitialData(card);
            _cards.Add(go);
        }

        public void selectCard(CardData card) {
            foreach (var go in _cards) {
                if (go.name == card.Id) {
                    Debug.Log("Do select");
                    UI.SelectCard(card);
                    go.GetComponent<CardView>().SelectCard();
                } else {
                    Debug.Log("UNDO elect");
                    UI.DeselectCard();
                    go.GetComponent<CardView>().DeselectCard();
                }
            }
        }

        public void resetSelection() {
            UI.DeselectCard();
            foreach (var go in _cards) {
                go.GetComponent<CardView>().DeselectCard();
            }
        }

        // player attacks a card
        public void DealDamageToCard(CardData card) {
            GameObject c = _findCard(card);
            c.GetComponent<CardView>().SetHealth(card.Card.Health);
        }

        // card attacks a player
        public IEnumerator DealDamageWithCard(CardData card) {
            GameObject c = _findCard(card);
            yield return c.GetComponent<CardView>().DealDamageToPlayer();
        }

        private void GenerateCells(Vector2Int gridSize)
        {
            _cells = new GameObject[gridSize.x, gridSize.y];
            for (var i = 0; i != gridSize.x; i++)
            {
                for (var j = 0; j != gridSize.y; j++) {
                    Vector3 pos = new Vector3(i * padding.x, 0, j * -padding.y);
                    GameObject go = Instantiate(cellPrefab, parent.transform);
                    go.transform.localPosition = pos;
                    _cells[i, j] = go;
                }
            }
        }

        private void GenerateCards(List<CardData> cards)
        {
            _cards = new List<GameObject>();
            foreach (var card in cards) {
                addCard(card);
            }
        }

        private GameObject _findCard(CardData card)
        {
            return _cards.Find(go => go.name == card.Id);
        }
    }
}
