using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestScript : MonoBehaviour
{
     public GameObject chest;

    private GameObject cardGame;
    private GameObject mainCamera;
    GameObject character;

        
   
    void Start()
    {
        character = GameObject.Find("Character");
        cardGame = GameObject.Find("CardGame");
        mainCamera = GameObject.Find("MainCamera");
    }
    
    void Update()
    {
        
            if((chest.transform.position.x - character.transform.position.x) == 0){
                 if((chest.transform.position.z - character.transform.position.z) == 0){
                Debug.Log("Tobi Pizda!");
                mainCamera.SetActive(false);
                Destroy(chest);                             
            }
        }         
    }     
}
