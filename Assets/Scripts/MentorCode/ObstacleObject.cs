using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleObject : MonoBehaviour
{
    public void Init (int id){
        Debug.Log (message: $"Game Object inited with id {id}: {gameObject.name}");
    } 
}