using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeTest : MonoBehaviour
{
    public Swipe swipeControls;
    public Transform character;
    private Vector3 characterDirection;

    private void Awake(){
        characterDirection = new Vector3(Random.Range(0, 7), 0.1f, Random.Range(0, 7));
    }
    
    void Update()
    {
        if (swipeControls.SwipeLeft)
            characterDirection += Vector3.left;
        if (swipeControls.SwipeRight)
            characterDirection += Vector3.right;
        if (swipeControls.SwipeUp)
            characterDirection += Vector3.forward;
        if (swipeControls.SwipeDown)
            characterDirection += Vector3.back;

        character.transform.position =  Vector3.MoveTowards(character.transform.position, characterDirection, 3f * Time.deltaTime);
                
       }
}
