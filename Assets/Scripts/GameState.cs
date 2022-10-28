using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    private Entity entity;
    [SerializeField]
    private int level = 0;
    private int maxLevel = 4;

    private GameUI UI;

    void Start()
    {
        UI = GameObject.Find("GameCanvas").GetComponent<GameUI>();
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
}

public enum Entity {
    Chest,
    Ruins
}
