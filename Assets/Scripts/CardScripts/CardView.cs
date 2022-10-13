using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;

namespace Default {
    public class CardView : MonoBehaviour
    {
        // public int health;
        // public int damage;
        // public GameObject Item;

        [SerializeField] 
        private CardStatsConfig _statsTexturesConfig;

        private Renderer _renderer;

        // Start is called before the first frame update
        void Start()
        {

        }

        void Update()
        {
            // SetStats();
        }

        public void TurnAround() {
            gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        public void SetInitialData(Card card) {
            var item = Instantiate(card.Prefab, gameObject.transform, false);
            item.transform.position = gameObject.transform.position;
       
            _renderer = GetComponent<Renderer>();
            SetHealth(card.Health);
            SetDamage(card.Damage);
        }

        public IEnumerator DealDamageToPlayer() {
            gameObject.transform.position += new Vector3(0, 0.1f, 0.2f);
            yield return new WaitForSeconds(1.5f);
            gameObject.transform.position += new Vector3(0, -0.1f, -0.2f);
        }

        public void SelectCard() {
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, 0.2f, gameObject.transform.position.z);
        }

        public void DeselectCard() {
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, 0, gameObject.transform.position.z);
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
                        if (!_statsTexturesConfig.TryGet(value, out var texture))
                        continue;
                        materials[i].SetTexture("_MainTex", texture);
                    }
                }
            }
        }
    }
}
