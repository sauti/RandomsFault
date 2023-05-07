using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Default {
public class RuinScript : MonoBehaviour
{
    public GameObject Ruins;

    private GameObject cardGame;
    private GameState gameState;
    GameObject character;
    GameObject mainCamera;
   
    void Start()
    {
        mainCamera = GameObject.Find("MainCamera");
        character = GameObject.Find("Character");
        gameState = GameObject.Find("GameManager").GetComponent<GameState>();
    }
    
    void Update()
    {                   
            if(Ruins.transform.position == character.transform.position){                // (Ruins.transform.position.x - character.transform.position.x) == 0                 
                Debug.Log("Tobi Pizda!");
                mainCamera.SetActive(false);
                gameState.SetCurrentEntity(Entity.Ruins);
                gameState.OnCardGameStart();
                Destroy(Ruins);                              
            }
        }         
    }     
}

