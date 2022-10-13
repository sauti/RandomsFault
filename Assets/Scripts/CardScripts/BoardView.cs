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

        public void rotateCard(CardData card) {
            GameObject c = _findCard(card);
            c.GetComponent<CardView>().TurnAround();
        }

        public void removeCard(CardData card) {
            GameObject c = _findCard(card);
            Destroy(c);
            _cards.Remove(c);
        }

        public void addCard(CardData card) {
            Vector2Int coord = card.Coord;
            GameObject go = Instantiate(cardPrefab, _cells[coord.x, coord.y].transform);
            go.transform.rotation = Quaternion.Euler(0, 0, card.IsRotated ? 0 : 180);
            go.name = card.Id;
            go.GetComponent<CardView>().SetInitialData(card.Card);
            _cards.Add(go);
        }

        public void selectCard(CardData card) {
            foreach (var go in _cards) {
                Debug.Log("Select: " + go.name);
                if (go.name == card.Id) {
                    go.GetComponent<CardView>().SelectCard();
                } else {
                    go.GetComponent<CardView>().DeselectCard();
                }
            }
        }

        public void resetSelection() {
            foreach (var go in _cards) {
                go.GetComponent<CardView>().DeselectCard();
            }
        }

        // player attacks a card
        public void AttackCard(CardData card) {
            GameObject c = _findCard(card);
            c.GetComponent<CardView>().SetHealth(card.Card.Health);
        }

        // cards attacks a player
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
