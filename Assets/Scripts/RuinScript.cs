using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Default {
public class RuinScript : MonoBehaviour
{
    public GameObject Ruins;

    private GameObject cardGame;
    private GameObject mainCamera;
    private GameState gameState;
    GameObject character;
    GameObject mainCam;
    //public RuinID ruinID;

        
   
    void Start()
    {
        character = GameObject.Find("Character");
        //cardGame = GameObject.Find("CardGame");
        mainCamera = GameObject.Find("MainCamera");
        gameState = GameObject.Find("GameManager").GetComponent<GameState>();
    }
    
    void Update()
    {                   
            if((Ruins.transform.position.x - character.transform.position.x) == 0){
                 if((Ruins.transform.position.z - character.transform.position.z) == 0){
                Debug.Log("Tobi Pizda!");
                mainCamera.SetActive(false); 
                gameState.SetCurrentEntity(Entity.Ruins);
                Destroy(Ruins);                              
            }
        }         
    }     
}
}
