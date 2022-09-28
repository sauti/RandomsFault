using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;

public class RandomCardStats : MonoBehaviour
{
    public Texture2D[] textures;
    private int damage;
    private int health;

    // Start is called before the first frame update
    void Start()
    {
        health = Random.Range(0, textures.Length);
        damage = Random.Range(0, textures.Length);

        Renderer renderer = GetComponent<Renderer>();
        Material[] materials = GetComponent<Renderer>().materials;

        for (int i = 0; i < materials.Length; i++)
        {
            string matName = materials[i].name;
            Debug.Log(matName);
            if (Regex.Match(matName, "Health").Success)
            {
                materials[i].SetTexture("_MainTex", textures[health]);
            }
            
            if (Regex.Match(matName, "Damage").Success)
            {
                materials[i].SetTexture("_MainTex", textures[damage]);
            }
        }  
    }
}
