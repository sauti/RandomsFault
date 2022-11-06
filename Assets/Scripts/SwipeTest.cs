using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Default
{
public class SwipeTest : MonoBehaviour
{
    public Swipe swipeControls;
    public Char character;
    public Vector3 characterDirection;
    public MapGenerator mapGen;

    private bool isMoving;
       
    private void Start()
    {       
    }
    
    void Update()
    {
        if (isMoving) {
            return;
        }

        switch (swipeControls.MoveDir) {
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
        // Debug.Log(character.transform.position.x + " " + characterDirection.x);
        // if (swipeControls.SwipeLeft)
        //     characterDirection += Vector3.left;
        // if (swipeControls.SwipeRight)
        //     characterDirection += Vector3.right;
        // if (swipeControls.SwipeUp)
        //     characterDirection += Vector3.forward;
        // if (swipeControls.SwipeDown)
        //     characterDirection += Vector3.back;

        if (Vector3.Distance(character.transform.position, characterDirection) != 0 && !isMoving)
        {
            StartCoroutine(MoveCharacter());
        }
         
        // character.transform.position = new Vector3(
        //     Mathf.Clamp(character.position.x, 0f, 4f),
        //     character.transform.position.y,
        //     Mathf.Clamp(character.position.z, 0f, 4f)
        // );        
    }

    public void PlaceCharacter()
    {
        characterDirection = mapGen.GetRandomEmptyTile();
    }

    private IEnumerator MoveCharacter()
    {
        Debug.Log("Start moveing");
        isMoving = true;
        character.StartMoving();
        character.transform.LookAt(characterDirection); 

        while (Vector3.Distance(character.transform.position, characterDirection) > 0.01f)
        {
            character.transform.position =  Vector3.MoveTowards(character.transform.position, characterDirection, 2.5f * Time.deltaTime);
            yield return null;
        }

        Debug.Log("stop moving");
        character.transform.position = characterDirection;
        character.StopMoving();
        isMoving = false;
    }
}
}