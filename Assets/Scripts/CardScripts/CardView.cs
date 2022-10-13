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

        public void decreaseHealth(int damage) {
            // var item = Instantiate(card.Prefab, gameObject.transform, false);
            // item.transform.position = gameObject.transform.position;
       
            // _renderer = GetComponent<Renderer>();
            // SetStats(card.Health, card.Damage);
        }

        private void SetHealth(int health) {
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
