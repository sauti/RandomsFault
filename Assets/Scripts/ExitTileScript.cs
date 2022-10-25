using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Default{
public class ExitTileScript : MonoBehaviour
{
    // public GameObject exitTile;
    private GameObject character;
    private MapGenerator mapGen;
    // Start is called before the first frame update
    void Start()
    {
        character = GameObject.Find("Character");
        mapGen = GameObject.Find("GameManager").GetComponent<MapGenerator>();
    }

    // Update is called once per frame
    void Update()
    {
        if((gameObject.transform.position.x - character.transform.position.x) == 0){
            if((gameObject.transform.position.z - character.transform.position.z) == 0)
        {
            Debug.Log("Exit!");
            Debug.Log(mapGen == null);
            // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            mapGen.CreateNewLevel();
        }
    }
    }
}
}

