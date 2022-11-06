using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Default {
public class ChestScript : MonoBehaviour
{
     public GameObject chest;
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
        
            if((chest.transform.position.x - character.transform.position.x) == 0){
                 if((chest.transform.position.z - character.transform.position.z) == 0){
                Debug.Log("Tobi Pizda!");
                mainCamera.SetActive(false);
                Destroy(chest);
                gameState.SetCurrentEntity(Entity.Chest);                             
            }
        }         
    }     
}
}
