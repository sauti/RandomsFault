using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Default {
    public class TableController : MonoBehaviour
    {
        private List<CardData> _cards;
        private List<CardData> _hand;
        private bool[,] _tableCells;
        private bool[,] _handCells;

        [SerializeField] 
        private Vector2Int _gridSize = new Vector2Int(4, 3);
        [SerializeField] 
        private Vector2Int _handSize = new Vector2Int(4, 2);
        [SerializeField]
        private CardsConfig _cardsConfig;

        [SerializeField] 
        private TableView _tableView;
        [SerializeField] 
        private TableView _handView;

        // Start is called before the first frame update
        void Start()
        {
            _hand = new List<CardData>();
            _tableCells = new bool[_gridSize.x, _gridSize.y];
            _handCells = new bool[_handSize.x, _handSize.y];
            InitTableCards();
            InitHandCards();
        }

        void Update()
        {
            OnClickListener();
        }

        private void InitHandCards() {
            Card card = _cardsConfig.GetByType(CardType.Attack) as Card;
            CardData data = new CardData()
            {
                Name = "HandCard " + 0 + " " + 0,
                Card = card,
                Coord = new Vector2Int(0, 0),
                IsRotated = true
            };
            _hand.Add(data);
            _handCells[0, 0] = true;
            _handView.GenerateTable(_handSize, _hand);
        }

        public void InitTableCards()
        {
            _cards = new List<CardData>();

            for (var i = 0; i != _gridSize.x; i++)
            {
                for (var j = 0; j != _gridSize.y; j++)
                {
                    bool hasCard = Random.Range(0, 1f) > 0.3;
                    if (hasCard) {
                        CardType type = GetRandomCardType();
                        Card card = _cardsConfig.GetByType(type) as Card;
                        CardData data = new CardData()
                        {
                            Name = "Card " + i + " " + j,
                            Card = card,
                            Coord = new Vector2Int(i, j),
                            IsRotated = false
                        };
                        _tableCells[i, j] = true;
                        _cards.Add(data);
                    }
                }
            }

            _tableView.GenerateTable(_gridSize, _cards);
        }
        
        public void OnClickListener() {
            if (Input.GetMouseButtonDown(0)) {  
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);  
                RaycastHit hit;  
                if (!Physics.Raycast(ray, out hit)) {
                    return;
                }  
                for (var i = 0; i < _cards.Count; i++) {
                    if (_cards[i].Name != hit.transform.name)
                    continue;

                    // turn around
                    if (!_cards[i].IsRotated) {
                        RotateCard(_cards[i], i);
                        break;
                    }

                    if (_cards[i].Card.CanPickUp) {
                        Debug.Log("Pick up " + _cards[i].Card.Type);
                        PickUp(_cards[i]);
                        break;
                    }

                    Debug.Log(_cards[i].Card.Type);
                    if (_cards[i].Card.Type == CardType.Exit) {
                        Debug.Log("Click Exit");
                        InitTableCards();
                        break;
                    }
                }
            } 
        }

        private CardType GetRandomCardType()
        {
            return (CardType)Random.Range(0, System.Enum.GetValues(typeof(CardType)).Length);
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
    }
}
