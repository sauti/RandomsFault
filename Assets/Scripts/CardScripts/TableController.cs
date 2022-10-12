using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Default {
    public class TableController : MonoBehaviour
    {
        public GameObject cellPrefab;
        public GameObject cardPrefab;
        public Vector2 padding = new Vector2(2f, 3f);

        private CardData[,] _cards;
        private List<GameObject> _cardsGo;

        [SerializeField] 
        private Vector2Int _gridSize = new Vector2Int(4, 3);
        [SerializeField]
        private CardsConfig _cardsConfig;

        // Start is called before the first frame update
        void Start()
        {
            GenerateField();
        }

        void Update()
        {
            OnClickListener();
        }

        public void GenerateField()
        {
            _cards = new CardData[_gridSize.x, _gridSize.y];
            _cardsGo = new List<GameObject>();

            for (var i = 0; i != _gridSize.x; i++)
            {
                for (var j = 0; j != _gridSize.y; j++)
                {
                    Vector3 pos = new Vector3(i * padding.x, 0, j * padding.y);
                    var cellObj = Instantiate(cellPrefab, pos, Quaternion.Euler(0, 0, 0), gameObject.transform);

                    bool hasCard = Random.Range(0, 1f) > 0.3;
                    if (hasCard) {
                        CardType type = GetRandomCardType();
                        Card card = _cardsConfig.GetByType(type) as Card;
                        string name = "Card " + i + " " + j;

                        CardData data = new CardData()
                        {
                            Name = name,
                            Card = card,
                            Coord = pos,
                            IsRotated = false
                        };
                        _cards[i, j] = data;

                        GameObject cardGo = Instantiate(cardPrefab, pos, Quaternion.Euler(0, 0, 180), cellObj.transform);
                        cardGo.name = name;
                        cardGo.GetComponent<CardView>().SetInitialData(data.Card);
                        _cardsGo.Add(cardGo);
                    }
                }
            }
        }
        
        public void OnClickListener() {
            if (Input.GetMouseButtonDown(0)) {  
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);  
                RaycastHit hit;  
                if (Physics.Raycast(ray, out hit)) {  
                    foreach (var go in _cardsGo) {
                        if (go.name == hit.transform.name) {
                            go.GetComponent<CardView>().TurnAround();
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
