using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitTileScript : MonoBehaviour
{
    public Transform exitTile;
    GameObject character;
    // Start is called before the first frame update
    void Start()
    {
        character = GameObject.Find("Character");
    }

    // Update is called once per frame
    void Update()
    {
        if((exitTile.transform.position.x - character.transform.position.x) == 0){
            if((exitTile.transform.position.z - character.transform.position.z) == 0)
        {
            Debug.Log("Exit!");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
    }
   
}
