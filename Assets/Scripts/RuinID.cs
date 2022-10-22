using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuinID : MonoBehaviour
{
    public Transform ruin;
    public bool thisRuin;
    public void Init(int id) 
    {
        Debug.Log($"This tile inited with {id}: {gameObject.name}");        
    }
}
