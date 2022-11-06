using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Default
{
    public class GameUI : MonoBehaviour
{
    public GameObject gameUI;
    public GameObject cardUI;
    public GameObject gemsGoal;
    public GameObject gemsBag;

    public TMP_Text levelLabel;
    public GameObject[] hearts;
    public GameObject avatarHighlight;

    public void EnterCardGame()
    {
        gemsGoal.SetActive(false);
        gameUI.SetActive(true);
    }

    public void EnterRoom()
    {
        gemsBag.SetActive(false);
        gameUI.SetActive(true);
    }

    public void EnterCardInspect()
    {
        gemsBag.SetActive(false);
        gameUI.SetActive(false);
    }

    public void ToggleGemsGoal()
    {
        gemsGoal.SetActive(!gemsGoal.activeSelf);
    }

    public void ToggleGemsBag()
    {
        gemsBag.SetActive(!gemsBag.activeSelf);
    }

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

    public void SelectCard(CardData card)
    {
        avatarHighlight.SetActive(card.Card.CanHeal);
    }

    public void DeselectCard()
    {
        avatarHighlight.SetActive(false);
    }
}
}
