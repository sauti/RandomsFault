using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Default {
    public class TableController : BoardController
    {
        private MapGenerator mapG;
        private GameState gameState;
        private GemsController gems;
        private CardGameController _cardController;

        private void Awake() {        
            _cardController = GameObject.Find("CardGameController").GetComponent<CardGameController>();
        }

        void OnEnable()
        {
            gameState = GameObject.Find("GameManager").GetComponent<GameState>();
            gems = GameObject.Find("Gems").GetComponent<GemsController>();
            initCards(new Vector2Int(_cells.GetLength(0), _cells.GetLength(1)));
        }

        public void initCards(Vector2Int grid)
        {
            _level = gameState.getLevel();
            _cells = new bool[grid.x, grid.y];
            _cards = new List<CardData>();
            _view = gameObject.GetComponent<BoardView>();

            _cg.Init(_level, gameState.getCurrentEntity());
            var cards = _cg.GenerateCardsForLevel();
            foreach (CardData card in cards)
            {
                Vector2Int coord = FindRandomEmptyCoordOnTable();
                card.Coord = coord;
                _cells[coord.x, coord.y] = true;
                _cards.Add(card);
            }

            _view.GenerateBoard(grid, _cards);
        }

        public void OnClick(RaycastHit hit) {
            for (var i = 0; i < _cards.Count; i++) {
                if (_cards[i].Id != hit.transform.name)
                continue;

                if (!_cards[i].IsRotated) {
                    transform.parent.GetComponent<CardGameController>().FlipCard(_cards[i]);
                    break;
                }

                if (_cards[i].Card.CanPickUp) {
                    transform.parent.GetComponent<CardGameController>().TryPickUp(_cards[i]);
                    break;
                }

                if (_cards[i].Card.CanBeKilled) {
                    transform.parent.GetComponent<CardGameController>().TryAttack(_cards[i]);
                    break;
                }
            
                if (_cards[i].Card.CardId == CardId.WayOut) {
                    Debug.Log("Click WayOut");
                    OnCardGameExit();
                    break;
                }
            }
        }

        public void OnBoardHighLight(CardData card){  
                foreach (CardData childCard in _cards){                
                    if (childCard.IsRotated == true && childCard.Card.CanBeKilled == true){
                            Debug.Log(childCard.Card.CardId); 
                            _cardController.OnHandCardSelect(card);
                }                
            }            
        }        

        public void FlipCard(CardData card) {
            Debug.Log("Flip " + card.Card.CardId);
            card.IsRotated = true;
            _view.FlipCard(card);
            if (card.Card.IsTrap) {
                _playerStats.GetDamage(card.Card.Damage);
            }
        }

        public IEnumerator PickUp(CardData card)
        {
            if (card.Card.IsGem) {
                yield return _view.PickUpGem(card);
                gems.PickUpGem(card.Card.CardId);
                // RemoveCard(card);
            } else {
                RemoveCard(card);
            }
        }

        public void FailPickup(CardData card)
        {
            _view.FailPickup(card);
        }

        // player attacks a card on the table
        public void Attack(CardData card, int damage) {
            Debug.Log("Attack " + damage);
            card.Card.SetHealth(card.Card.Health - damage);
            if (card.Card.Health <= 0) {
                CardData childCard = null;
                childCard = _cg.GetChildCardByChance(card.Card.SpawnsAfterDeath, card.Coord);

                RemoveCard(card);

                if (childCard is CardData) {
                    _cards.Add(childCard);
                    _cells[childCard.Coord.x, childCard.Coord.y] = true;
                    _view.addCard(childCard);
                }
            } else {
                _view.DealDamageToCard(card);
            }
        }

        public IEnumerator SpawnTurnCards()
        {
            List<CardData> parentCards = _cards.FindAll(card =>
                card.IsRotated && card.Card.SpawnsEachTurn.Count > 0
            );

            if (parentCards.Count > 0) {
                foreach (CardData c in parentCards) {
                    Vector2Int coord = FindRandomEmptyCoordOnTable();
                    if (coord.x == -1) {
                        Debug.Log("No free space for spawn.");
                        continue;
                    }

                    CardData childCard = null;
                    childCard = _cg.GetChildCardByChance(c.Card.SpawnsEachTurn, coord);

                    if (childCard is CardData) {
                        yield return new WaitForSeconds(0.3f);
                        _cards.Add(childCard);
                        _cells[coord.x, coord.y] = true;
                        _view.addCard(childCard);
                    }
                }
            }
        }

        // cards attack a player
        public IEnumerator AttackPlayerWithOpenCards() {
            List<CardData> enemyCards = _cards.FindAll(card =>
                card.IsRotated && card.Card.CanBeKilled && card.Card.Damage > 0
            );
            if (enemyCards.Count > 0) {
                yield return new WaitForSeconds(0.7f);
                foreach (CardData card in enemyCards) {
                    _playerStats.GetDamage(card.Card.Damage);
                    yield return _view.DealDamageWithCard(card);
                }
                Debug.Log("Done all damage");
            }
        }

        private Vector2Int FindRandomEmptyCoordOnTable()
        {
            int iterations = _cells.GetLength(0) * _cells.GetLength(1);
            int i = 0;
            while (i <= iterations)
            {
                i++;
                var coord = new Vector2Int(
                    Random.Range(0, _cells.GetLength(0)),
                    Random.Range(0, _cells.GetLength(1))
                );
                if (_cells[coord.x, coord.y] == false)
                    return coord;
            }

            return new Vector2Int(-1, -1);
        }


        public void OnCardGameExit()
        {
            gameState.OnCardGameExit();
        }
    }
}
