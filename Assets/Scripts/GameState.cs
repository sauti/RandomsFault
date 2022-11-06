using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Default {
public class GameState : MonoBehaviour
{
    public GameObject mainCamera;
    public GameObject cardGame;
    public GameObject swipe;

    private GameUI UI;

    private Entity entity;
    [SerializeField]
    private int level = 0;
    private int maxLevel = 4;

    void Start()
    {
        UI = GameObject.Find("UI").GetComponent<GameUI>();
        UI.SetLevel(level);
    }

    public void StartNextLevel()
    {
        SetLevel(level + 1);
    }

    public int getLevel()
    {
        return level;
    }

    public void SetCurrentEntity(Entity _entity)
    {
        entity = _entity;
    }

    public Entity getCurrentEntity()
    {
        return entity;
    }

    public bool isNextLevelValid()
    {
        return level + 1 <= maxLevel;
    }

    private void SetLevel(int lvl)
    {
        level = lvl > maxLevel ? maxLevel : lvl;
        UI.SetLevel(level);
    }

    public void OnCardGameExit()
    {
        Debug.Log("Exit Card Game");
        mainCamera.SetActive(true);
        cardGame.SetActive(false);
        swipe.gameObject.SetActive(true);
        UI.EnterRoom();
    }

    public void OnCardGameStart()
    {
        swipe.gameObject.SetActive(false);
        cardGame.SetActive(true);
        mainCamera.SetActive(false);
        UI.EnterCardGame();
    }
}

public enum Entity {
    Chest,
    Ruins
}
}
