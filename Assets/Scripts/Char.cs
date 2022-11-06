using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace Default
{
public class Char : OnClickListener
{  
    private Animator animator;
    private GameUI UI;

    void Start()
    {
        UI = GameObject.Find("UI").GetComponent<GameUI>();
        animator = gameObject.GetComponentInChildren<Animator>();
    }

    protected override void OnClick(RaycastHit hit)
    {
        if (hit.transform.name == "Character") {
            UI.ToggleGemsGoal();
        }
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

