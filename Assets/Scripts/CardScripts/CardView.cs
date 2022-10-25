using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;

namespace Default {
    public class CardView : MonoBehaviour
    {
        [SerializeField] 
        private CardStatsConfig _statsTexturesConfig;

        private Renderer _renderer;
        private Animator _animator;
        private bool isFlipped = false;

        public void SetInitialData(CardData card) {
            gameObject.name = card.Id;
            GameObject cardGo = gameObject.transform.Find("Card").gameObject;

            GameObject item = Instantiate(card.Card.Prefab, cardGo.transform, false);
            item.transform.position = gameObject.transform.position;

            _renderer = cardGo.GetComponent<Renderer>();
            _animator = GetComponent<Animator>();

            if (card.IsRotated) {
                _animator.SetBool("isFlipped", true);
                // _animator.Play("card_state_default");
                isFlipped = true;
            }

            SetHealth(card.Card.Health);
            SetDamage(card.Card.Damage);
        }

        void OnEnable()
        {
            Debug.Log("Awake");
            if (isFlipped) {
                _animator.SetBool("isFlipped", true);
            }
        }

        public void Inspect() {
            _animator.Play("card_closeup");
        }

        public void CloseInspect() {
            _animator.Play("card_state_default");
        }

        public IEnumerator DealDamageToPlayer() {
            yield return new WaitForSeconds(0.5f);
            yield return PlayAnimationAndWait("card_attack_player");
        }

        public void Flip() {
            StartCoroutine(PlayAnimationAndWait("card_flip"));
        }

        public void PlayCloseupAnimation() {
            _animator.Play("card_closeup");
        }

        public void SelectCard() {
            _animator.SetBool("isSelected", true);
        }

        public void DeselectCard() {
            _animator.SetBool("isSelected", false);
        }

        public void SetHealth(int health) {
            SetStat("Health", health);
        }

        private void SetDamage(int damage) {
            SetStat("Damage", damage);
        }

        private void SetStat(string name, int value) {
            Material[] materials = _renderer.materials;
            for (int i = 0; i < materials.Length; i++)
            {
                string matName = materials[i].name;
                if (Regex.Match(matName, name).Success)
                {
                    {
                        if (!_statsTexturesConfig.TryGet(value, name, out var texture))
                        continue;
                        materials[i].SetTexture("_MainTex", texture);
                    }
                }
            }
        }

        private IEnumerator PlayAnimationAndWait(string anim) {
            _animator.Play(anim);
            float animationLength = _animator.GetCurrentAnimatorStateInfo(0).length;
            yield return new WaitForSecondsRealtime(animationLength);
        }
    }
}
