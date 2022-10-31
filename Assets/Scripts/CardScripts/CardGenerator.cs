using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Default {
    [CreateAssetMenu(menuName = "Cards/CardGenerator")]
    public class CardGenerator: ScriptableObject
    {
        public CardsConfig cardsConfig;
        public LevelsList levelsListConfig;
        public ThoughtsConfig thoughtsConfig;
        public LevelConfig handConfig;

        private LevelConfig _levelConfig;
        private int _level;
        private List<Thought> _defaultThoughts;

        public void Init(int level, Entity entity) {
            _level = level;
            if (entity == Entity.Ruins) {
                _levelConfig = levelsListConfig.GetRuinsConfigForLevel(level);
            } else {
                _levelConfig = levelsListConfig.GetChestsConfigForLevel(level);
            }
            _defaultThoughts = thoughtsConfig.getDefaultList();
        }

        public List<CardData> GenerateCardsForLevel() {
            int amount = _levelConfig.GetCardsAmount();
            List<CardPerLevelData> conf = _levelConfig.GetCardsConfig();
            IEnumerable<CardChanceBase> chanceConf = conf;
            List<CardData> cards = new List<CardData>();
            Vector2Int coord = new Vector2Int(0, 0);
            
            List<CardId> types = GenerateRequiredCardTypes(conf, amount);
            if (types.Count < amount) {
                types.AddRange(GetRandomTypesByChance(chanceConf.ToList(), amount - types.Count));
            }

            string debugStr = ""; 
            for (var i = 0; i < types.Count; i++) {
                debugStr += (types[i] + ", ");
                CardData card = GenerateCardByType(types[i], coord);
                cards.Add(card);
            }
            Debug.Log(debugStr);

            return cards;
        }

        public CardData GenerateHandCard()
        {
            return GenerateCardByType(CardId.Scratch, new Vector2Int(0, 0), true, true);
        }

        public CardData GenerateCardByType(CardId cardId, Vector2Int coord, bool IsRotated = false, bool IsHand = false) {
            CardTemplate template = cardsConfig.GetByType(cardId);
            CardPerLevelData cardLevelData = IsHand ? handConfig.GetCardConfig(cardId) : _levelConfig.GetCardConfig(cardId);

            var card = new Card()
            {
                CardId = template.CardId,
                Prefab = template.Prefab,
                Health = cardLevelData != null ? cardLevelData.Health : 0,
                Damage = cardLevelData != null ? cardLevelData.Damage : 0,
                CanBeKilled = template.CanBeKilled,
                CanHeal = template.CanHeal,
                IsWeapon = template.IsWeapon,
                IsTrap = template.IsTrap,
                IsGem = template.IsGem,
                SpawnsAfterDeath = cardLevelData != null ? cardLevelData.SpawnsAfterDeath : new List<CardChance>(),
                SpawnsEachTurn = cardLevelData != null ? cardLevelData.SpawnsEachTurn : new List<CardChance>(),
                Thought = GetRandomThought(cardId),
            };

            CardData data = new CardData()
            {
                Id = GetUuid(),
                Card = card,
                Coord = coord,
                IsRotated = IsRotated || false
            };
            return data;
        }

        private string GetUuid()
        {
            return System.Guid.NewGuid().ToString("N");
        }

        private string GetRandomThought(CardId cardId)
        {
            List<Thought> cardThoughts = thoughtsConfig.getListByCardId(cardId);
            List<string> thoughts = GetThoughtsByLevel(cardThoughts);

            if (thoughts.Count < 1) {
                thoughts = GetThoughtsByLevel(_defaultThoughts);
            }

            int rand = UnityEngine.Random.Range(0, thoughts.Count);
            return thoughts[rand];
        }

        private List<string> GetThoughtsByLevel(List<Thought> allThoughts)
        {
            List<string> thoughts = new List<string>();
            foreach (Thought t in allThoughts) {
                if (t.levels.Contains(_level)) {
                    thoughts.Add(t.text);
                }
            }
            return thoughts;
        }

        private List<CardId> GenerateRequiredCardTypes(List<CardPerLevelData> list, int amount) {
            List<CardId> types = new List<CardId>();
            for (int i = 0; i < list.Count; i++)
            {
                CardPerLevelData current = list[i];
                if (current.MinAmount > 0) {
                    for (int j = 0; j < current.MinAmount; j++) {
                        types.Add(current.CardId);
                    }
                }
            }

            if (types.Count > amount) {
                throw new ArgumentOutOfRangeException("Can't generate more cards with minAmount than total cards for this level.");
            }

            return types;
        }

        public CardData GetChildCardByChance(List<CardChance> list, Vector2Int coord) {
            CardData childCard = null;
            IEnumerable<CardChanceBase> chanceConf = list;
            List<CardChanceBase> baseList = list.ToList<CardChanceBase>();

            if (baseList.Count > 0) {
                List<CardId> cardIds = GetRandomTypesByChance(baseList, 1);
                Debug.Log("Spawn card with id " + cardIds[0] + " " + (cardIds[0] == CardId.None));
                if (cardIds[0] != CardId.None) {
                    childCard = GenerateCardByType(cardIds[0], coord, true);
                }
            }
            return childCard;
        }

        public List<CardId> GetRandomTypesByChance(List<CardChanceBase> list, int amount) {
            int chanceSum = 0;
            for (int i = 0; i < list.Count; i++)
            {
                CardChanceBase current = list[i];
                chanceSum += current.Chance;
                if (i == 0) {
                    current.minChance = 0;
                    current.maxChance = current.Chance;
                } else {
                    current.minChance = list[i - 1].maxChance;
                    current.maxChance = current.minChance + current.Chance;
                }
            }

            List<CardId> cardIds = new List<CardId>();
            for (var i = 0; i < amount; i++) {
                CardId? cardId = GetRandomTypeByChance(list, chanceSum);
                if (cardId is CardId) {
                    cardIds.Add((CardId)cardId);
                }
            }

            return cardIds;
        }

        private CardId? GetRandomTypeByChance(List<CardChanceBase> list, int chanceSum)
        {
            int rand = UnityEngine.Random.Range(0, chanceSum);
            for (int i = 0; i < list.Count; i++)
            {
                CardChanceBase current = list[i];
                if (rand >= current.minChance && rand < current.maxChance)
                {
                    return current.CardId;
                }
            }

            return null;
        }
    }
}
