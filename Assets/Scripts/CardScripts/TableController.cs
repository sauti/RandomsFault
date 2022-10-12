using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Default {
    public class TableController : MonoBehaviour
    {
        private List<CardData> _cards;

        [SerializeField] 
        private Vector2Int _gridSize = new Vector2Int(4, 3);
        [SerializeField]
        private CardsConfig _cardsConfig;

        [SerializeField] 
        private TableView _view;

        // Start is called before the first frame update
        void Start()
        {
            GenerateTableCards();
        }

        void Update()
        {
            OnClickListener();
        }

        public void GenerateTableCards()
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
                        _cards.Add(data);
                    }
                }
            }

            _view.GenerateTable(_gridSize, _cards);
        }
        
        public void OnClickListener() {
            if (Input.GetMouseButtonDown(0)) {  
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);  
                RaycastHit hit;  
                if (Physics.Raycast(ray, out hit)) {  
                    for (var i = 0; i < _cards.Count; i++) {
                        if (_cards[i].Name != hit.transform.name)
                        continue;

                        // turn around
                        if (!_cards[i].IsRotated) {
                            Debug.Log("Rotate " + _cards[i].Card.Type);
                            _cards[i].IsRotated = true;
                            _view.rotateCard(i);
                            break;
                        }

                        Debug.Log(_cards[i].Card.Type);
                        if (_cards[i].Card.Type == CardType.Exit) {
                            Debug.Log("Click Exit");
                            GenerateTableCards();
                            break;
                        }
                    }
                }  
            } 
        }

        private CardType GetRandomCardType()
        {
            return (CardType)Random.Range(0, System.Enum.GetValues(typeof(CardType)).Length);
        }
    }
}
