using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Default{
public class ExitTileScript : MonoBehaviour
{
    // public GameObject exitTile;
    private GameObject character;
    private MapGenerator mapGen;
    private GameState gameState;

    void Start()
    {
        character = GameObject.Find("Character");
        var gm = GameObject.Find("GameManager");
        mapGen = gm.GetComponent<MapGenerator>();
        gameState = gm.GetComponent<GameState>();
    }

    void Update()
    {
        if((gameObject.transform.position.x - character.transform.position.x) == 0){
            if((gameObject.transform.position.z - character.transform.position.z) == 0)
        {
            Debug.Log("Exit!");
            Debug.Log(mapGen == null);
            gameState.StartNextLevel();
            mapGen.CreateNewLevel();
        }
    }
    }
}
}

