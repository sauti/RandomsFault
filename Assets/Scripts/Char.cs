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
        Ray ray = new Ray(transform.position, - transform.up);
         
        RaycastHit hit; 

        if (Physics.Raycast(ray, out hit)){
            if (hit.collider.gameObject.GetComponent<Tile>()){
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                    Debug.Log("hit Exit");
            }     
        }
    }

    public void StartMoving()
    {
        Debug.Log("Start moving");
        animator.SetBool("isMoving", true);
    }

    public void StopMoving()
    {
        animator.SetBool("isMoving", false);
    }
}
