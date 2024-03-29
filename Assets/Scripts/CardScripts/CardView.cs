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
        public GameObject Card;
        public GameObject outline;
        private bool isFlipped;  
        private CardGameController _cardGameController;      
        
        public void SetInitialData(CardData card) {
            gameObject.name = card.Id;
            GameObject cardGo = gameObject.transform.Find("Card").gameObject;
            GameObject itemParent = cardGo.transform.Find("Item").gameObject;

            GameObject item = Instantiate(card.Card.Prefab, itemParent.transform);
            // item.transform.position = itemParent.transform.position;

            _renderer = cardGo.GetComponent<Renderer>();
            _animator = GetComponent<Animator>();

            if (card.IsRotated) {
                _animator.SetBool("isFlipped", true);
                isFlipped = true;
            }

            SetHealth(card.Card.Health);
            SetDamage(card.Card.Damage);
        }

        void OnEnable()
        {
            if (isFlipped) {
                _animator.SetBool("isFlipped", true);
            }
        }

        public IEnumerator MoveGemToBag()
        {
            yield return StartCoroutine(AnimUtils.MoveToTarget(gameObject, new Vector3(15.92f, 1.595f, -2.495f), Quaternion.Euler(-30, 0, 0), 0.7f, 0.2f));
            yield return StartCoroutine(AnimUtils.MoveToTarget(gameObject, new Vector3(15.94f, 1.1f, -3.295f), Quaternion.Euler(-30, 0, 0), 0.5f, 0.1f));
        }

        public void Inspect() {
            _animator.Play("card_closeup");
        }

        public void CloseInspect() {
            _animator.Play("card_state_default");
        }

        public void FailPickup() {
            _animator.Play("card_fail_pickup");
        }

        public IEnumerator DealDamageToPlayer() {
            // yield return new WaitForSeconds(0.1f);
            yield return PlayAnimationAndWait("card_attack_player", 0.5f);
        }

        public void Flip() {
            StartCoroutine(PlayAnimationAndWait("card_flip", 0.7f));
            isFlipped = true;
        }

        public void PlayCloseupAnimation() {
            _animator.Play("card_closeup");
        }

        public void SelectCard() {
            _animator.SetBool("isSelected", true);
            // if (card.Card.IsWeapon){
            //             _tableController.OnBoardHighLight();
            //         }   
            
                //Debug.Log("hand light");
                // this.gameObject.GetComponentInChildren<Renderer>().sharedMaterial.EnableKeyword("_EMISSION");
        }         

        public void DeselectCard() {
            _animator.SetBool("isSelected", false);
            // this.gameObject.GetComponentInChildren<Renderer>().sharedMaterial.DisableKeyword("_EMISSION");                  
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

        public void OutlineActive(CardData card){
            Debug.Log("card view debug");
            outline.gameObject.SetActive(card.IsRotated);
            // outline.gameObject.SetActive(card.Card.CanBeKilled);
        }

        private IEnumerator PlayAnimationAndWait(string anim, float length) {
            _animator.Play(anim);
            // float animationLength = _animator.GetCurrentAnimatorStateInfo(0).length;
            yield return new WaitForSecondsRealtime(length);
        }
    }
}
