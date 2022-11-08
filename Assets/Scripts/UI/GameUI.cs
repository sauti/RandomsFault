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
    public GameObject cardGameUI;
    public GameObject gemsGoal;
    public GameObject gemsBag;
    public GameObject gameLight;
    public GameObject cardGameLight;

    public TMP_Text levelLabel;
    public GameObject[] hearts;
    public GameObject avatarHighlight;

    private bool isCardGame;
    public bool isUIOverlay;

    public void EnterRoom()
    {
        isCardGame = false;
        gemsBag.SetActive(false);
        cardGameUI.SetActive(false);
        gameUI.SetActive(true);
    }

    public void EnterCardGame()
    {
        isCardGame = true;
        gemsGoal.SetActive(false);
        gameUI.SetActive(true);
        gameLight.SetActive(true);
        cardGameLight.SetActive(true);
    }

    public void EnterCardInspect()
    {
        gemsBag.SetActive(false);
        gameUI.SetActive(false);
        cardGameUI.SetActive(false);
        gameLight.SetActive(false);
        cardGameLight.SetActive(false);
    }

    public void ToggleGemsGoal()
    {
        if (!isCardGame) {
            isUIOverlay = !gemsGoal.activeSelf;
            gemsGoal.SetActive(!gemsGoal.activeSelf);
        }
    }

    public void OpenGemsBag()
    {
        if (!gemsBag.activeSelf) {
            gemsBag.SetActive(true);
            isUIOverlay = true;
        }
    }

    public void CloseGemsBag()
    {
        gemsBag.SetActive(false);
        isUIOverlay = false;
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
        cardGameUI.SetActive(true);
        avatarHighlight.SetActive(card.Card.CanHeal);
    }

    public void DeselectCard()
    {
        cardGameUI.SetActive(false);
        avatarHighlight.SetActive(false);
    }

    public void SetIsUIOverlay(bool isActive)
    {
        isUIOverlay = isActive;
    }
}
}
