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
    public GemsUI gemsUI;
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
        gemsUI.OpenBag(false);
        cardGameUI.SetActive(false);
        gameUI.SetActive(true);
    }

    public void EnterCardGame()
    {
        isCardGame = true;
        gameUI.SetActive(true);
        gameLight.SetActive(true);
        cardGameLight.SetActive(true);
        gemsUI.CloseBag();
    }

    public void EnterCardInspect()
    {
        gemsUI.CloseBag();
        gameUI.SetActive(false);
        cardGameUI.SetActive(false);
        gameLight.SetActive(false);
        cardGameLight.SetActive(false);
    }

    public void OpenGemsBag()
    {
        gemsUI.OpenBag(true);
        isUIOverlay = true;
    }

    public void CloseGemsBag()
    {
        if (isCardGame) {
            gemsUI.CloseBag();
            isUIOverlay = false;
        }
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
