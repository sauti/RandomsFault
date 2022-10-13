using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Default {
    public class CardGameController : MonoBehaviour
    {
        public Vector2Int _gridSize = new Vector2Int(4, 3);
        public Vector2Int _handSize = new Vector2Int(4, 2);
        public CardsConfig _cardsConfig;
        public GameObject _tableGo;
        public GameObject _handGo;
        public CardGenerator _cg;

        private BoardView _tableView;
        private BoardView _handView;

        private List<CardData> _cards;
        private List<CardData> _hand;
        private bool[,] _tableCells;
        private bool[,] _handCells;

        void Start()
        {
            _hand = new List<CardData>();
            _tableView = _tableGo.GetComponent<BoardView>();
            _handView = _handGo.GetComponent<BoardView>();
            _handCells = new bool[_handSize.x, _handSize.y];
            InitTableCards();
            InitHandCards();
        }

        void Update()
        {
            OnClickListener();
        }

        private void InitHandCards() {
            CardData card = _cg.GenerateCardByType(CardType.Attack, new Vector2Int(0, 0), "Hand", true);
            _hand.Add(card);
            _handCells[0, 0] = true;
            _handView.GenerateBoard(_handSize, _hand);
        }

        public void InitTableCards()
        {
            _tableCells = new bool[_gridSize.x, _gridSize.y];
            _cards = new List<CardData>();
            int maxCardsAmount = _gridSize.x * _gridSize.y;
            int amount = Random.Range(2, maxCardsAmount);

            for (var i = 0; i < amount; i++) {
                Vector2Int coord = FindRandomEmptyCoordOnTable();
                CardData card = _cg.GenerateRandomCard(coord, "Table");
                _tableCells[coord.x, coord.y] = true;
                _cards.Add(card);
            }

            _tableView.GenerateBoard(_gridSize, _cards);
        }
        
        public void OnClickListener() {
            if (Input.GetMouseButtonDown(0)) {  
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);  
                RaycastHit hit;  
                if (!Physics.Raycast(ray, out hit)) {
                    return;
                }  

                for (var i = 0; i < _hand.Count; i++) {
                    if (_hand[i].Name != hit.transform.name)
                    continue;

                    SelectCard(_hand[i]);
                    break;
                }

                for (var i = 0; i < _cards.Count; i++) {
                    if (_cards[i].Name != hit.transform.name)
                    continue;

                    if (!_cards[i].IsRotated) {
                        RotateCard(_cards[i], i);
                        break;
                    }

                    if (_cards[i].Card.CanPickUp) {
                        PickUp(_cards[i]);
                        break;
                    }
                
                    if (_cards[i].Card.Type == CardType.Exit) {
                        Debug.Log("Click Exit");
                        InitTableCards();
                        break;
                    }
                }
            } 
        }

        private void RotateCard(CardData card, int i) {
            Debug.Log("Rotate " + _cards[i].Card.Type + "  index: " + i);
            _cards[i].IsRotated = true;
            _tableView.rotateCard(card);
        }

        private void PickUp(CardData card)
        {
            _cards.Remove(card);
            _tableCells[card.Coord.x, card.Coord.y] = false;
            _tableView.removeCard(card);

            Vector2Int coord = FindEmptyCoordInHand();
            card.Name = "HandCard " + coord.x + " " + coord.y;
            card.Coord = coord;
            Debug.Log(card.Coord.x + " " + card.Coord.y);
            _hand.Add(card);
            _handCells[coord.x, coord.y] = true;
            _handView.addCard(card);
        }

        private void SelectCard(CardData card)
        {
            _handView.selectCard(card);
        }

        private Vector2Int FindEmptyCoordInHand()
        {
            for (int j = 0; j < _handSize.y; j++)
            {
                for (int i = 0; i < _handSize.x; i++)
                {
                    if (_handCells[i, j] == false) {
                        return new Vector2Int(i, j);
                    }
                }
            }
            return new Vector2Int(-1, -1);
        }

        private Vector2Int FindRandomEmptyCoordOnTable()
        {
            while (true)
            {
                var coord = new Vector2Int(
                    Random.Range(0, _gridSize.x),
                    Random.Range(0, _gridSize.y));
                if (_tableCells[coord.x, coord.y] == false)
                    return coord;
            }
        }
    }
}
