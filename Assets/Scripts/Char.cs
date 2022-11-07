using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace Default
{
public class Char : MonoBehaviour
{  
    private Animator animator;

    void Start()
    {
        animator = gameObject.GetComponentInChildren<Animator>();
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
}

