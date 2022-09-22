using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterEnemyScript : MonoBehaviour
{
    public Transform Enemy;
    private Vector3 direction;

    private void Awake(){
        direction = new Vector3(Random.Range(0, 7), 0.5f, Random.Range(0, 7));
    }

    private void Update() {
        Enemy.transform.position =  Vector3.MoveTowards(Enemy.transform.position, direction, 3f * Time.deltaTime);
    }
}
