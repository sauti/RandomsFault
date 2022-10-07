using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RuinScript : MonoBehaviour
{
    public GameObject Ruins;

    private Camera cardsCamera;
    private GameObject mainCamera;
    private GameObject exitBtn;
    GameObject character;

    public bool currRuinObj;
    
    // Start is called before the first frame update
    void Start()
    {
        character = GameObject.Find("Character");
        cardsCamera = GameObject.Find("CardsCamera").GetComponent<Camera> ();
        mainCamera = GameObject.Find("MainCamera");
        exitBtn = GameObject.Find("RayExitButton");
    }

    // Update is called once per frame
    void Update()
    {
            if((Ruins.transform.position.x - character.transform.position.x) == 0){
                 if((Ruins.transform.position.z - character.transform.position.z) == 0){
                Debug.Log("Tobi Pizda!");
                mainCamera.gameObject.SetActive(false);
                cardsCamera.gameObject.SetActive(true);
                // exitBtn.gameObject.SetActive(true);

                // GameObject.Find("CardsCamera").SetActive(true);
                // GameObject.Find("MainCamera").SetActive(false);
                // GameObject.Find("Button").SetActive(true);
            }
        }
    }

    public void Init(int id, string ruinName, bool currRuinObj){
        Debug.Log ($"Obj id {id} {gameObject.name}");
    }
}

