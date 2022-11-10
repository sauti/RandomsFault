using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemsBagController : MonoBehaviour
{
    // public GameObject MyPrefab;
    public GameObject parent;
    public float minRange = 10f;
    private List<GameObject> items = new List<GameObject>();
 
    public void AddGem(GameObject gemPrefab)
    {
        var searchCount = 10;
        while(searchCount-- > 0)
        {
            var position = GetRandomPositionOnMesh();
            if (IsPositionEmpty(position))
            {
                var go = Instantiate(gemPrefab, parent.transform);
                go.transform.localPosition = position;
                go.transform.localRotation = Quaternion.Euler(UnityEngine.Random.Range(0, 360), 90, 90);
                go.layer = LayerMask.NameToLayer("Web");
                items.Add(go);
                break;
            }
        }
    }
 
    bool IsPositionEmpty(Vector3 position)
    {
        foreach(var item in items)
        {
            if (Vector3.Distance(position, item.transform.localPosition) < minRange)
                return false;
        }

        return true;
    }

    Vector3 GetRandomPositionOnMesh()
    {
        return Random.insideUnitCircle * 1.5f;
    }

    // // for position debug
    // private void CreateItems(int itemQuantity)
    // {
    //     for(var n = 0; n < itemQuantity; ++n)
    //     {
    //         AddGem(MyPrefab)l
    //     }
    // }
}
