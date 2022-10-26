using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameUI : MonoBehaviour
{
    public TMP_Text levelLabel;
    public TMP_Text healthLabel;

    public void SetLevel(int level)
    {
        levelLabel.text = level.ToString();
    }
    
    public void SetHealth(int health)
    {
        healthLabel.text = health.ToString();
    }
}
