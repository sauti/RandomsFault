using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Default
{
public class RuinID : MonoBehaviour
{
    // public Transform ruin;
    // public bool thisRuin;
    public Vector3 objPos;
    // public SwipeTest swipe;
    public void Init(Vector3 pos) 
    {
        Debug.Log($"This tile inited with {pos}: {gameObject.name}");  
        // objPos = pos;      
    }
    
    // public void Init(Vector3 id)
    // {
    //     Debug.Log($"This is tile {id}, {gameObject.name}");
    // }

    // private void Update() {
    //     if (objPos == swipe.characterDirection)
    //     Object.Destroy(this.gameObject);
    // }
}
}