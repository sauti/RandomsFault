using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Default
{
public class SwipeTest : MonoBehaviour
{
    private List<Vector3> occupiedPositions = new List<Vector3>();
    public Swipe swipeControls;
    public Transform character;
    public Vector3 characterDirection;
    public MapGenerator mapGen;
       
    private void Start(){       
        characterDirection = mapGen.GetRandomEmptyTile();
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
}