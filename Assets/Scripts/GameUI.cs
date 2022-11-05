using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameUI : MonoBehaviour
{
    public TMP_Text levelLabel;
    public GameObject[] hearts;

    public void SetLevel(int level)
    {
        levelLabel.text = (level + 1).ToString();
    }
    
    public void SetHealth(int health)
    {
        Debug.Log(health);
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < health) {
                hearts[i].SetActive(true);
            } else {
                hearts[i].SetActive(false);
            }
        }
    }
}
