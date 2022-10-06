using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapGenerator : MonoBehaviour
{
   // Map Gnerator
   [SerializeField] private GameObject gridPrefab;
   [SerializeField] private float offset;
   [SerializeField] private Transform gridParent;
   [SerializeField] private Transform wallParent;
   public GameObject Wall;
   public GameObject FloorEdge;
   public GameObject ExitTile;
   public GameObject Pillar;
   public GameObject Chest;
   public GameObject Ruins;
   public float floorFormingSpeed = 0.1f;
   Quaternion startAngle = Quaternion.Euler (0,0,0);
   Quaternion finishAngle = Quaternion.Euler (-90,0,0);
   public Quaternion currentAngle;

   private List<Vector3> occupiedPositions = new List<Vector3>();
  
    // Chars Randomizer
    public GameObject Character;
    public Transform CharacterMoves;

    private Vector3 characterDirection;
    public GameObject Enemy;
    private Vector3 enemyDirection;
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

    public Transform mainCamera;
    public Transform cardsCamera;
    public Button exitBtn;

   private void Awake()
   {
        var cellSize = gridPrefab.GetComponent<MeshRenderer>().bounds.size;
        int exitTileX = Random.Range(0, 5);
        int exitTileY = Random.Range(0, 5);

        // floor grid
        for(int x  = 0; x < 5; x++)
        {            
            for(int y = 0; y < 5; y++)
            {
                var position = new Vector3(x, 0, y);
                Vector3 offsetVector = new Vector3(offset, 0, offset);
                GameObject floorCell;
                if (x == exitTileX && y == exitTileY) {
                    floorCell = Instantiate(ExitTile, position + offsetVector, Quaternion.identity, gridParent);
                    occupiedPositions.Add(position);
                } else {
                    floorCell = Instantiate(gridPrefab, position + offsetVector, Quaternion.identity, gridParent);
                }
                floorCell.name = $"X: {x} Y: {y}";
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

        // room walls
        for (int i = 0; i < 3; i++)
        {
            for (int x  = 0; x < 5; x++)
            {
                int randomAngle = Random.Range(0, 2) * 180;
                Vector3 position = new Vector3(x, 0, 4.9f);
                Quaternion rotation = Quaternion.Euler(0, randomAngle, 0);
                if (i > 0) {
                    position = new Vector3(-0.9f, 0, x);
                    rotation = Quaternion.Euler(0, randomAngle + 90, 0);
                }
                if (i == 1) {
                    position = new Vector3(4.9f, 0, x);
                }
                Instantiate(Wall, position, rotation, wallParent);
            }
        }

        // room edges
        Instantiate(FloorEdge, new Vector3(2, 0, 4.65f), Quaternion.identity, wallParent);
        Instantiate(FloorEdge, new Vector3(-0.65f, 0, 2), Quaternion.Euler(0, -90 ,0), wallParent);
        Instantiate(FloorEdge, new Vector3(4.65f, 0, 2), Quaternion.Euler(0, -90 ,0), wallParent);

        // corner pillars
        var pillarOffset = 5.6f;
        for(int x  = 0; x < 2; x++)
        {            
            for(int y = 0; y < 2; y++)
            {
                var position = new Vector3(x * pillarOffset - 0.8f, 0, y * pillarOffset - 0.8f);
                Instantiate(Pillar, position, Quaternion.identity, wallParent);
            }
        }

        // edge pillars
        var pillarOffset1 = 2f;
        for(int x  = 0; x < 2; x++)
        {            
            var position = new Vector3(x * pillarOffset1 + 1f, 0, 4.8f);
            Instantiate(Pillar, position, Quaternion.identity, wallParent);
        }
        for(int x  = 0; x < 2; x++)
        {            
            for(int y = 0; y < 2; y++)
            {
                var position = new Vector3(x * 5.6f - 0.8f, 0, y * pillarOffset1 + 1f);
                Instantiate(Pillar, position, Quaternion.identity, wallParent);
            }
        }

        // chests
        int chestsAmount = Random.Range(1, 5);
        for(int i  = 0; i < chestsAmount; i++)
        {     
            Vector3 position = GetRandomEmptyTile(); 
            var rotation = Quaternion.Euler(0, Random.Range(0, 360) ,0);
            Instantiate(Chest, position, rotation, wallParent);
        }

        int ruinsAmount = Random.Range(1, 5);
        for(int i  = 0; i < chestsAmount; i++)
        {     
            Vector3 position = GetRandomEmptyTile(); 
            var rotation = Quaternion.Euler(0, Random.Range(0, 360) ,0);
            Instantiate(Ruins, position, rotation, wallParent);
        }
        

        // Vector3 randomSpawnHerosPosition = new Vector3(Random.Range(0, 7), 0, Random.Range(0, 7));
        // Instantiate(Character, randomSpawnHerosPosition, Quaternion.identity);

        // Vector3 randomSpawnEnemysPosition = new Vector3(Random.Range(0, 7), 0, Random.Range(0, 7));
        // Instantiate(Enemy, randomSpawnEnemysPosition, Quaternion.identity);

        Character.SetActive(true);
        characterDirection = GetRandomEmptyTile();
        Enemy.SetActive(true);
        enemyDirection = GetRandomEmptyTile();

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

        // if (swipeControls.SwipeLeft)
        //     characterDirection += Vector3.left;
        // if (swipeControls.SwipeRight)
        //     characterDirection += Vector3.right;
        // if (swipeControls.SwipeUp)
        //     characterDirection += Vector3.forward;
        // if (swipeControls.SwipeDown)
        //     characterDirection += Vector3.back;

        //Character.transform.position =  Vector3.MoveTowards(Character.transform.position, characterDirection, 3f * Time.deltaTime);
        Enemy.transform.position =  Vector3.MoveTowards(Enemy.transform.position, enemyDirection, 3f * Time.deltaTime);
       
        if ((Character.transform.position.x - Ruins.transform.position.x) == 0 && (Character.transform.position.z - Ruins.transform.position.z) == 0){
            Debug.Log("Tobi Pizda!");
            cardsCamera.gameObject.SetActive(true);
            mainCamera.gameObject.SetActive(false);
            exitBtn.gameObject.SetActive(true);
        }else{
            
        }

        foreach (Transform Gargoyle in objectsParent){            
        Gargoyle.transform.LookAt(CharacterMoves);
        }
        //Character.transform.LookAt(characterDirection);
        Enemy.transform.LookAt(CharacterMoves); // TODO: check why disable

        planet.transform.RotateAround(orbitCenter.position, Vector3.up, Time.deltaTime * planetSpeed);
   }

   private Vector3 GetRandomEmptyTile() {
        Vector3 position = new Vector3(Random.Range(0, 5), 0, Random.Range(0, 5));
        while (occupiedPositions.Contains(position))
        {
            position = new Vector3(Random.Range(0, 5), 0, Random.Range(0, 5));
        }
        occupiedPositions.Add(position);
        return position;
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

    public void OnExitBtnClick(){
        Debug.Log("Exit");
        Destroy(Ruins); // TODO: find alt
        mainCamera.gameObject.SetActive(true);
        cardsCamera.gameObject.SetActive(false);
    }
}