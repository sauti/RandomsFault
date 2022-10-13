using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Default {
    public class PlayerStats : MonoBehaviour
    {
        public int Health;
        public int MaxHealth;
        public TMP_Text textObj;

        public void Heal(int points) {
            Debug.Log("Heal: " + points);
            if (Health + points > MaxHealth) {
                Health = MaxHealth;
            } else {
                Health += points;
            }
            textObj.text = Health.ToString();
        }
        
        public void GetDamage(int points) {
            Debug.Log("Get damage: " + points);
            Health -= points;
            if (Health <= 0) {
                textObj.text = "You died :(";
            } else {
                textObj.text = Health.ToString();
            }
        }
    }
}