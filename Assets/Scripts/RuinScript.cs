using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RuinScript : MonoBehaviour
{
    public GameObject Ruins;

    private GameObject cardGame;
    private GameObject mainCamera;
    GameObject character;
    GameObject mainCam;
    //public RuinID ruinID;

        
   
    void Start()
    {
        character = GameObject.Find("Character");
        //cardGame = GameObject.Find("CardGame");
        mainCamera = GameObject.Find("MainCamera");
    }
    
    void Update()
    {                   
            if((Ruins.transform.position.x - character.transform.position.x) == 0){
                 if((Ruins.transform.position.z - character.transform.position.z) == 0){
                Debug.Log("Tobi Pizda!");
                mainCamera.SetActive(false); 
                Destroy(Ruins);                              
            }
        }         
    }     
}

