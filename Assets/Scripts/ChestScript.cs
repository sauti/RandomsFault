using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Default {
public class ChestScript : MonoBehaviour
{
     public GameObject chest;

    private GameObject cardGame;
    private GameObject mainCamera;
    private GameState gameState;
    GameObject character;

        
   
    void Start()
    {
        character = GameObject.Find("Character");
        cardGame = GameObject.Find("CardGame");
        mainCamera = GameObject.Find("MainCamera");
        gameState = GameObject.Find("GameManager").GetComponent<GameState>();
    }
    
    void Update()
    {
        
            if((chest.transform.position.x - character.transform.position.x) == 0){
                 if((chest.transform.position.z - character.transform.position.z) == 0){
                Debug.Log("Tobi Pizda!");
                gameState.SetCurrentEntity(Entity.Chest);
                mainCamera.SetActive(false);
                Destroy(chest);                             
            }
        }         
    }     
}
}
