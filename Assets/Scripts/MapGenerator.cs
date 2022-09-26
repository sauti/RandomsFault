using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
   // Map Gnerator
   [SerializeField] private GameObject gridPrefab;
   [SerializeField] private float offset;
   [SerializeField] private Transform gridParent;
   public float floorFormingSpeed = 0.1f;
   Quaternion startAngle = Quaternion.Euler (0,0,0);
   Quaternion finishAngle = Quaternion.Euler (90,0,0);
   public Quaternion currentAngle;
  
    // Chars Randomizer
    public GameObject Character;
    public Transform CharacterMoves;

    private Vector3 characterDirection;
    public GameObject Enemy;
    private Vector3 enemyDirection;
    public GameObject Wall;
    public Transform Gargoyle;

    // Swipe Controls
    public Swipe swipeControls;

    // Evade physics
    // Vector2 directionRay;

    // Aditional effects
    [SerializeField] private Transform objectsParent;

    public float gargoylesRotationAmount = 2f;
    public int ticksPerSecond = 60;
    public bool pause = false;
    public float gargoylesRotationSpeed = 1f;  
    [SerializeField] private float gargoyleOffset;
    

    public Transform orbitCenter;
    public GameObject orbitCenterObj;
    public Transform planet;
    public float planetSpeed;
    public float planetAngle = 20;


    
   private void Awake()
   {

        var cellSize = gridPrefab.GetComponent<MeshRenderer>().bounds.size;


        for(int x  = 0; x < 5; x++)
        {            
            for(int y = 0; y < 5; y++)
            {
                var position = new Vector3(x * (cellSize.x + offset), 0, y * (cellSize.z + offset));

                var cellGargoyle = Instantiate(gridPrefab, position, Quaternion.identity, gridParent);

                cellGargoyle.name = $"X: {x} Y: {y}";
            }
        }
                
        foreach (Transform gridPrefab in gridParent){
            gridPrefab.position += new Vector3(0.5f, 0, 0.5f);

            //gridPrefab.transform.eulerAngles = new Vector3(90, 0, 0);
        }

        var gargoyleSize = gridPrefab.GetComponent<MeshRenderer>().bounds.size;

        for(int x  = 0; x < 2; x++)
        {            
            for(int y = 0; y < 2; y++)
            {
                var position = new Vector3(x * (gargoyleSize.x + gargoyleOffset), 0, y * (gargoyleSize.z + gargoyleOffset));

                var cell = Instantiate(Gargoyle, position, Quaternion.identity, objectsParent);

                //Gargoyle.name = $"X: {x} Y: {y}";
            }
        }

        foreach (Transform Gargoyle in objectsParent){
            Gargoyle.position += new Vector3(-0.5f, 0, -0.5f);
            //Gargoyle.transform.eulerAngles = new Vector3(90, 0, 0);
        }

        // Vector3 randomSpawnWallPosition = new Vector3(Random.Range(0, 7), 0, Random.Range(0, 7));
        // Instantiate(Wall, randomSpawnWallPosition, Quaternion.identity);

        // Vector3 randomSpawnHerosPosition = new Vector3(Random.Range(0, 7), 0, Random.Range(0, 7));
        // Instantiate(Character, randomSpawnHerosPosition, Quaternion.identity);

        // Vector3 randomSpawnEnemysPosition = new Vector3(Random.Range(0, 7), 0, Random.Range(0, 7));
        // Instantiate(Enemy, randomSpawnEnemysPosition, Quaternion.identity);

        Character.SetActive(true);
        characterDirection = new Vector3(Random.Range(0, 5), 0, Random.Range(0, 5));
        Enemy.SetActive(true);
        enemyDirection = new Vector3(Random.Range(0, 5), 0, Random.Range(0, 5));

        orbitCenterObj.SetActive(true);

        StartCoroutine(GargoylesRotate());
   }

   private void Start() {
        
   }
   private void Update() {

        foreach (Transform gridPrefab in gridParent){

            currentAngle = startAngle;
            ChangeCurrentFloorAngle();
            gridPrefab.transform.rotation = Quaternion.Slerp (gridPrefab.transform.rotation, currentAngle, floorFormingSpeed);
        }

        if (swipeControls.SwipeLeft)
            characterDirection += Vector3.left;
        if (swipeControls.SwipeRight)
            characterDirection += Vector3.right;
        if (swipeControls.SwipeUp)
            characterDirection += Vector3.forward;
        if (swipeControls.SwipeDown)
            characterDirection += Vector3.back;

        Character.transform.position =  Vector3.MoveTowards(Character.transform.position, characterDirection, 3f * Time.deltaTime);
        Enemy.transform.position =  Vector3.MoveTowards(Enemy.transform.position, enemyDirection, 3f * Time.deltaTime);
       
        if ((Character.transform.position.x - Enemy.transform.position.x) == 0 && (Character.transform.position.z - Enemy.transform.position.z) == 0){
            Debug.Log("Tobi Pizda!");
        }else{
            Debug.Log("OK!");
        }

        foreach (Transform Gargoyle in objectsParent){            
        Gargoyle.transform.LookAt(CharacterMoves);
        }

        planet.transform.RotateAround(orbitCenter.position, Vector3.up, Time.deltaTime * planetSpeed);
   }

   

   void ChangeCurrentFloorAngle(){
    if(currentAngle.eulerAngles.x == startAngle.eulerAngles.x){
                currentAngle = finishAngle;
            }
   }

   //---------------------------------------------------------------Gargoyles--------------------------------------------------------------------------------------

    private IEnumerator GargoylesRotate(){
        WaitForSeconds Wait = new WaitForSeconds(1 / ticksPerSecond);

        while (true){
            if(!pause){
                Gargoyle.transform.Rotate(Vector3.up * gargoylesRotationAmount);
            }
            yield return Wait;
        }
    }    
}