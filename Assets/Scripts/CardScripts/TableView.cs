using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Default {
    public class TableView : MonoBehaviour
    {
        public GameObject cellPrefab;
        public GameObject cardPrefab;
        public Vector2 padding = new Vector2(0.6f, 0.9f);

        private List<GameObject> _cards = new List<GameObject>();
        private GameObject[,] _cells;

        private void Awake()
        {
            _cards = new List<GameObject>();
        }

        private void ClearTable()
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

        public void GenerateTable(Vector2Int gridSize, List<CardData> cards) {
            ClearTable();
            GenerateCells(gridSize);
            GenerateCards(cards);
        }

        public void rotateCard(int index) {
            _cards[index].GetComponent<CardView>().TurnAround();
        }

        private void GenerateCells(Vector2Int gridSize)
        {
            _cells = new GameObject[gridSize.x, gridSize.y];
            for (var i = 0; i != gridSize.x; i++)
            {
                for (var j = 0; j != gridSize.y; j++) {
                    Vector3 pos = new Vector3(i * padding.x, 0, j * padding.y);
                    GameObject go = Instantiate(cellPrefab, pos, Quaternion.Euler(0, 0, 0), gameObject.transform);
                    _cells[i, j] = go;
                }
            }
        }

        private void GenerateCards(List<CardData> cards)
        {
            foreach (var card in cards) {
                Vector2Int coord = card.Coord;
                // Vector3 pos = new Vector3(coord.x * padding.x, 0, coord.y * padding.y);
                GameObject go = Instantiate(cardPrefab, _cells[coord.x, coord.y].transform);
                go.transform.rotation = Quaternion.Euler(0, 0, 180);
                go.name = card.Name;
                go.GetComponent<CardView>().SetInitialData(card.Card);
                _cards.Add(go);
            }
        }
    }
}
