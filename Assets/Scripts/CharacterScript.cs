using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterScript : MonoBehaviour
{
public GameObject Ruins;

    public GameObject cardsCamera;
    public GameObject mainCamera;
    public GameObject exitBtn;
    public GameObject character;

    public bool currRuinObj;
    
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
            if((Ruins.transform.position.x - character.transform.position.x) == 0){
                 if((Ruins.transform.position.z - character.transform.position.z) == 0){
                Debug.Log("Tobi Pizda!");
                mainCamera.SetActive(false);
                cardsCamera.SetActive(true);
                exitBtn.gameObject.SetActive(true);

                // GameObject.Find("CardsCamera").SetActive(true);
                // GameObject.Find("MainCamera").SetActive(false);
                // GameObject.Find("Button").SetActive(true);
            }
        }
    }
}
