using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMesh : MonoBehaviour
{

    public Mesh[] meshes;
    //public Transform character;
    
    private void Update() {
        // if(meshes.GetValue(me) == character.transform.position){
        //     Debug.Log ("ok!");
        // }
    }
    void OnEnable()
    {
        GetComponent<MeshFilter>().mesh = meshes[Random.Range(0, meshes.Length)];
    }
}
