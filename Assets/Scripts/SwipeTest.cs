using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeTest : MonoBehaviour
{
    public Swipe swipeControls;
    public Transform character;
    private Vector3 characterDirection;

    private void Awake(){
        characterDirection = new Vector3(Random.Range(0, 5), 0.1f, Random.Range(0, 5));
    }
    
    void Update()
    {
        switch (swipeControls.MoveDir){
            case Direction.Up: 
                if (characterDirection.z < 4)
                    characterDirection += Vector3.forward;
            break;
            
            case Direction.Down: 
                if (characterDirection.z > 0)
                    characterDirection += Vector3.back;
            break;

            case Direction.Left:
                if (characterDirection.x > 0)
                    characterDirection += Vector3.left;
            break;

            case Direction.Right:
                if (characterDirection.x < 4)
                    characterDirection += Vector3.right;             
             break;

        }
        // if (swipeControls.SwipeLeft)
        //     characterDirection += Vector3.left;
        // if (swipeControls.SwipeRight)
        //     characterDirection += Vector3.right;
        // if (swipeControls.SwipeUp)
        //     characterDirection += Vector3.forward;
        // if (swipeControls.SwipeDown)
        //     characterDirection += Vector3.back;

        Vector3 movements = character.transform.position =  Vector3.MoveTowards(character.transform.position, characterDirection, 3f * Time.deltaTime);
        character.transform.LookAt(characterDirection); 
         
        // character.transform.position = new Vector3(
        //     Mathf.Clamp(character.position.x, 0f, 4f),
        //     character.transform.position.y,
        //     Mathf.Clamp(character.position.z, 0f, 4f)
        // );        
    }    
}
