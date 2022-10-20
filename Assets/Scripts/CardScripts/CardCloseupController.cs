using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Default {
    public class CardCloseupController : MonoBehaviour
    {
        public GameObject canvas;
        public GameObject cardPrefab;
        public GameObject cardParent;
        public Camera camera;

        private GameObject card;

        public void Open(CardData cardData) {
            Debug.Log("Open card");
            canvas.SetActive(true);

            card = Instantiate(cardPrefab, cardParent.transform);
            card.transform.parent = cardParent.transform;
            
            CardView view = card.GetComponent<CardView>();
            view.SetInitialData(cardData);
            view.PlayCloseupAnimation();
            SetLayerAllChildren(card, cardParent.layer);
        }

        public void Close() {
            Debug.Log("Close");
            canvas.SetActive(false);
            Destroy(card);
        }

        void SetLayerAllChildren(GameObject root, int layer)
        {
            card.layer = cardParent.layer;
            var children = root.transform.GetComponentsInChildren<Transform>(includeInactive: true);
            foreach (var child in children)
            {
                child.gameObject.layer = layer;
            }
        }
    }
}
