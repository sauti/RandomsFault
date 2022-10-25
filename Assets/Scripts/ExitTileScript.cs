using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Default{
public class ExitTileScript : MonoBehaviour
{
    public Transform exitTile;
    GameObject character;
    // public MapGenerator mapGen;
    // Start is called before the first frame update
    void Start()
    {
        character = GameObject.Find("Character");
        //mapGen = GameObject.Find("GameManager");
    }

    // Update is called once per frame
    void Update()
    {
        if((exitTile.transform.position.x - character.transform.position.x) == 0){
            if((exitTile.transform.position.z - character.transform.position.z) == 0)
        {
            Debug.Log("Exit!");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            // mapGen.MapCreation();
        }
    }
    }
}
}

