using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Default {
    public class CardCloseupController : MonoBehaviour
    {
        public GameObject canvas;
        public GameObject gameCanvas;
        public GameObject cardPrefab;
        public GameObject cardParent;

        private GameObject card;
        private CardView view;
        
        private float duration = 0.2f;

        private Vector3 inspectPos = new Vector3(15, 3, -1);
        private Quaternion inspectRotation = Quaternion.Euler(-90, 180, 180);
        private float targetScale = 5;
        private float scaleModifier = 1;
        
        private Vector3 defaultPos;
        private Quaternion defaultRotation;

        public void Open(CardData cardData) {
            card = GameObject.Find(cardData.Id);
            view = card.GetComponent<CardView>();
            defaultPos = card.transform.position;
            defaultRotation = card.transform.rotation;

            gameCanvas.SetActive(false);
            canvas.SetActive(true);
            SetLayer("Card");
            view.Inspect();
            Debug.Log(cardData.Card.Thought);
            StartCoroutine(LerpTransform(inspectPos, inspectRotation, targetScale, duration));
        }

        public void Close() {
            SetLayer("Default");
            canvas.SetActive(false);
            gameCanvas.SetActive(true);
            view.CloseInspect();
            view = null;
            StartCoroutine(LerpTransform(defaultPos, defaultRotation, 1, duration));
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

        IEnumerator LerpTransform(Vector3 targetPosition, Quaternion targetRotation, float targetScale, float duration)
        {
            float time = 0;
            float startValue = 1;
            Vector3 startPosition = card.transform.position;
            Quaternion startRotation = transform.rotation;
            Vector3 startScale = transform.localScale;

            while (time < duration)
            {
                card.transform.position = Vector3.Lerp(startPosition, targetPosition, time / duration);
                card.transform.rotation = Quaternion.Lerp(startRotation, targetRotation, time / duration);
                scaleModifier = Mathf.Lerp(startValue, targetScale, time / duration);
                card.transform.localScale = startScale * scaleModifier;
                time += Time.deltaTime;
                yield return null;
            }
        }
    }
}
