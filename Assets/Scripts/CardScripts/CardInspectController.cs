using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Default {
    public class CardInspectController : MonoBehaviour
    {
        public GameObject inspectView;
        public GameObject cardPrefab;
        public GameObject cardParent;
        public TMP_Text thought;

        private GameObject card;
        private CardView view;
        
        private float duration = 0.15f;

        private Vector3 inspectPos = new Vector3(15, 3, -1);
        private Quaternion inspectRotation = Quaternion.Euler(-90, 180, 180);
        private float targetScale = 5;
        
        private Vector3 defaultPos;
        private Quaternion defaultRotation;

        private CardGameController _cardController;
        private GameUI UI;

        void Start() {
            _cardController = GameObject.Find("CardGameController").GetComponent<CardGameController>();
            UI = GameObject.Find("UI").GetComponent<GameUI>();
        }

        public void Open(CardData cardData) {
            card = GameObject.Find(cardData.Id);
            view = card.GetComponent<CardView>();
            defaultPos = card.transform.position;
            defaultRotation = card.transform.rotation;

            inspectView.SetActive(true);

            _cardController.SetInspectState();
            UI.EnterCardInspect();
            SetLayer("Card");
            view.Inspect();
            thought.text = cardData.Card.Thought;
            StartCoroutine(AnimUtils.MoveToTarget(card, inspectPos, inspectRotation, targetScale, duration));
        }

        public void Close() {
            inspectView.SetActive(false);

            _cardController.SetPlayerTurn();
            UI.EnterCardGame();
            SetLayer("Default");
            view.CloseInspect();
            view = null;
            StartCoroutine(AnimUtils.MoveToTarget(card, defaultPos, defaultRotation, 1, duration));
        }

        private void SetLayer(string layer)
        {
            var layerIndex = LayerMask.NameToLayer(layer);
            card.layer = layerIndex;
            var children = card.transform.GetComponentsInChildren<Transform>(includeInactive: true);
            foreach (var child in children)
            {
                child.gameObject.layer = layerIndex;
            }
        }
    }
}
