using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitTileScript : MonoBehaviour
{
    public Transform exitTile;
    public Transform Character;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if((exitTile.transform.position.x - GameObject.Find("Character").transform.position.x) == 0){
            if((exitTile.transform.position.z - GameObject.Find("Character").transform.position.z) == 0)
        {
            Debug.Log("Exit!");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
    }
   
}
