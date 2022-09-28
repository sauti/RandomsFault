using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMesh : MonoBehaviour
{

    public Mesh[] meshes;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<MeshFilter>().mesh = meshes[Random.Range(0, meshes.Length)];
    }
}
