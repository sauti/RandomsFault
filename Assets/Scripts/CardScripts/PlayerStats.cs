using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Default {
    public class PlayerStats : MonoBehaviour
    {
        public int Health;
        public TMP_Text textObj;

        public void Heal(int points) {
            Debug.Log("Heal: " + points);
            Health += points;
            textObj.text = Health.ToString();
        }
        
        public void GetDamage(int points) {
            Debug.Log("Get damage: " + points);
            Health -= points;
            textObj.text = Health.ToString();
        }
    }
}
