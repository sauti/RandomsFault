using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RuinScript : MonoBehaviour
{
    public Transform Ruins;

    private GameObject cardsCamera;
    private GameObject mainCamera;
    private GameObject exitBtn;
    // public Camera cardsCamera;
    // public Camera mainCamera;
    // public Button exitBtn;

    // Start is called before the first frame update
    void Start()
    {
        cardsCamera = GameObject.Find("Cards Camera");
        mainCamera = GameObject.Find("Main Camera");
        exitBtn = GameObject.Find("Button");
    }

    // Update is called once per frame
    void Update()
    {
        if((Ruins.transform.position.x - GameObject.Find("Character").transform.position.x) == 0){
            if((Ruins.transform.position.z - GameObject.Find("Character").transform.position.z) == 0){
            Debug.Log("Tobi Pizda!");
            cardsCamera.gameObject.SetActive(true);
            mainCamera.gameObject.SetActive(false);
            exitBtn.gameObject.SetActive(true);

            //GameObject.Find("Cards Camera").SetActive(true);
            //GameObject.Find("Main Camera").SetActive(false);
            //GameObject.Find("Button").SetActive(true);
        }else{
            
        }
    }
}
}

