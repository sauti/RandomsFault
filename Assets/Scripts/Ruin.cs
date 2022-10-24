using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Default
{
public class ObstacleCell : MonoBehaviour
{    
    private Vector3 objPos;
    public SwipeTest swipe;
   
    public void Init(int x) 
    {
        Debug.Log($"This is tile x {x}: {gameObject.name}");        
    }   
    // public void Init(Vector3 position)
    // {
    //     Debug.Log($"This is tile x {position}, {gameObject.name}");
    // } 

    private void Update() {
        if (objPos == swipe.characterDirection)
        Destroy(this.gameObject);
    }
}
}
