using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Char : MonoBehaviour
{  
    
private void Update() 
    {
        Ray ray = new Ray(transform.position, - transform.up);
         
        RaycastHit hit; 

        if (Physics.Raycast(ray, out hit)){
            if (hit.collider.gameObject.GetComponent<Tile>()){
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                    Debug.Log("hit Exit");
            }     
        }
    }
}
