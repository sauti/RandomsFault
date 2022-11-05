using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

namespace Default {
    public class PlayerStats : MonoBehaviour
    {
        public int Health;
        public int MaxHealth;
        public TMP_Text textObj;
        
        private GameUI UI;

        void Awake()
        {
            UI = GameObject.Find("GameCanvas").GetComponent<GameUI>();
            UI.SetHealth(Health);
        }

        public void Heal(int points) {
            Debug.Log("Heal: " + points);
            if (Health + points > MaxHealth) {
                Health = MaxHealth;
            } else {
                Health += points;
            }
            UI.SetHealth(Health);
            UI.DeselectCard();
        }
        
        public void GetDamage(int points) {
            Debug.Log("Get damage: " + points);
            Health -= points;
            UI.SetHealth(Health);

            if (Health <= 0) {
                Delay();
                SceneManager.LoadScene("MainMenu");
            }
        }

        public IEnumerator Delay(){
            yield return new WaitForSeconds(1f);
        }
    }
}
