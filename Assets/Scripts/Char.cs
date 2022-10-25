using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Char : MonoBehaviour
{  
    private Animator animator;

    void Start()
    {
        animator = gameObject.GetComponentInChildren<Animator>();
    }

    private void Update() 
    {
        // Ray ray = new Ray(transform.position, - transform.up);
         
        // RaycastHit hit; 

        // if (Physics.Raycast(ray, out hit)){
        //     if (hit.collider.gameObject.GetComponent<ExitTileScript>()){
        //             //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        //             mapGen.MapCreation();
        //             Debug.Log("hit Exit");
        //     }     
        // }
    }

    public void StartMoving()
    {
        animator.SetBool("isMoving", true);
    }

    public void StopMoving()
    {
        animator.SetBool("isMoving", false);
    }
}

