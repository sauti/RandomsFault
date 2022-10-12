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

        // private void OnMouseDown()
        // {
        //     TurnAround();
        // }

        public void TurnAround() {
            gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        public void SetInitialData(Card card) {
            var item = Instantiate(card.Prefab, gameObject.transform, false);
            item.transform.position = gameObject.transform.position;
       
            _renderer = GetComponent<Renderer>();
            SetStats(card.Health, card.Damage);
        }

        private void SetStats(int health, int damage) {
            Material[] materials = _renderer.materials;
            for (int i = 0; i < _renderer.materials.Length; i++)
            {
                string matName = materials[i].name;
                if (Regex.Match(matName, "Health").Success)
                {
                    if (!_statsTexturesConfig.TryGet(health, out var healthTexture))
                    continue;
                    materials[i].SetTexture("_MainTex", healthTexture);
                }
                
                if (Regex.Match(matName, "Damage").Success)
                {
                    {
                    if (!_statsTexturesConfig.TryGet(damage, out var damageTexture))
                    continue;
                    materials[i].SetTexture("_MainTex", damageTexture);
                }
                }
            }
        }
    }
}
