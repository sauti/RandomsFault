using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Default{
public class ExitTileScript : MonoBehaviour
{
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
            if (gameState.isNextLevelValid()) {
                gameState.StartNextLevel();
                mapGen.CreateNewLevel();
            } else {
                SceneManager.LoadScene("MainMenu");
            }
        }
    }
    }
}
}

