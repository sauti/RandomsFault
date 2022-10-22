using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public MeshRenderer Mesh;
    public bool isExit;

    public void Init(int x, int y) 
    {
        Debug.Log($"This is tile x {x}, y {y}: {gameObject.name}");        
    }    
}
