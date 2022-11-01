using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GargoyleScript : MonoBehaviour
{
    public Transform CharacterMoves;
    [SerializeField] private GameObject gridPrefab;
    public Transform Gargoyle;
    public float gargoylesRotationAmount = 2f;
    public int ticksPerSecond = 60;
    public float gargoylesRotationSpeed = 1f;  
    [SerializeField] private float gargoyleOffset;
    [SerializeField] private Transform objectsParent;
    [SerializeField] private Transform lightEffect;
    // Start is called before the first frame update
    void Start()
    {
        var gargoyleSize = gridPrefab.GetComponent<MeshRenderer>().bounds.size;

        for(int x  = 0; x < 2; x++)
        {            
            for(int y = 0; y < 2; y++)
            {
                var position = new Vector3(x * (gargoyleSize.x + gargoyleOffset), 0, y * (gargoyleSize.z + gargoyleOffset));

                var cell = Instantiate(Gargoyle, position, Quaternion.identity, objectsParent);

                 var lightRotation = Quaternion.Euler(-90, 0, 0);
            // var currLightEffect = GameObject.Instantiate(lightEffect, position, lightRotation, objectsParent);

                //Gargoyle.name = $"X: {x} Y: {y}";
            }
        }
        
        foreach (Transform Gargoyle in objectsParent){
            Gargoyle.position += new Vector3(-0.5f, 0, -0.5f);
        }

        // StartCoroutine(GargoylesRotate());
    }

    // Update is called once per frame
    void Update()
    {
        foreach (Transform Gargoyle in objectsParent){            
        Gargoyle.transform.LookAt(CharacterMoves);
        }            
    }

    //  private IEnumerator GargoylesRotate(){
    //     WaitForSeconds Wait = new WaitForSeconds(1 / ticksPerSecond);

    //     while (true){
            
    //             Gargoyle.transform.Rotate(Vector3.up * gargoylesRotationAmount);
            
    //         yield return Wait;
    //     }
    // }        
}
