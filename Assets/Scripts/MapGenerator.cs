using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

namespace Default
{
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
   public GameObject CornerPillar;
   public GameObject Chest;
   public GameObject Ruins;
   public float floorFormingSpeed = 0.1f;
   Quaternion startAngle = Quaternion.Euler (0,0,0);
   Quaternion finishAngle = Quaternion.Euler (-90,0,0);
   public Quaternion currentAngle;

   private List<Vector3> occupiedPositions;
   public Vector3 position;  

    //public Tile tile;
    //public RuinID ruinPrefab;

    //private Vector2 cellSize;
    //[SerializeField] 
    //private List<EntityView> _entities;

    public GameObject mainCamera;
    public GameObject cardGame;
    public GameObject Swipe;

    //public MainMenuScript mainMS;
    //public var currRuin;    

   private void Start()
   {        
        CreateMap();
   }
  
   private void Update() {
        foreach (Transform gridPrefab in gridParent){
            currentAngle = startAngle;
            ChangeCurrentFloorAngle();
            gridPrefab.transform.rotation = Quaternion.Slerp (gridPrefab.transform.rotation, currentAngle, floorFormingSpeed);
        } 

        if (mainCamera.activeInHierarchy == false){
            OnCardGameStart();        
        }                          
    }

    public void CreateNewLevel()
    {
        DestroyMap();
        CreateMap();
        Swipe.GetComponent<SwipeTest>().PlaceCharacter();
    }

    private void DestroyMap()
    {
        foreach (Transform child in gridParent) {
            Destroy(child.gameObject);
        }
        foreach (Transform child in wallParent) {
            Destroy(child.gameObject);
        }
    }

    private void CreateMap()
    {

        var cellSize = gridPrefab.GetComponent<MeshRenderer>().bounds.size;
        occupiedPositions = new List<Vector3>();
        int exitTileX = Random.Range(0, 5);
        int exitTileY = Random.Range(0, 5);

        // floor grid
        for(int x  = 0; x < 5; x++)
        {            
            for(int y = 0; y < 5; y++)
            {
                var position = new Vector3(x, 0, y) ;
                Vector3 offsetVector = new Vector3(offset, 0, offset);
                GameObject floorCell;
                if (x == exitTileX && y == exitTileY) {
                    floorCell = Instantiate(ExitTile, position + offsetVector, Quaternion.identity, gridParent);
                    occupiedPositions.Add(position);
                    position = GetRandomEmptyTile();
                } else {
                    floorCell = Instantiate(gridPrefab, position + offsetVector, Quaternion.identity, gridParent);
                }
                floorCell.name = $"X: {x} Y: {y}";
            }
        }
                
        foreach (Transform gridPrefab in gridParent){
            gridPrefab.position += new Vector3(0.5f, 0, 0.5f);
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
            var position = new Vector3(x * pillarOffset - 0.8f, 0, pillarOffset - 0.8f);
            Instantiate(CornerPillar, position, Quaternion.identity, wallParent);
        }

        // edge pillars
        for(int x  = 0; x < 2; x++)
        {            
            var position = new Vector3(x * 2f + 1f, 0, 4.65f);
            Instantiate(Pillar, position, Quaternion.identity, wallParent);
        }
        for(int x  = 0; x < 2; x++)
        {            
            for(int y = 0; y < 2; y++)
            {
                var position = new Vector3(x * 5.3f - 0.65f, 0, y * 2f + 1f);
                var rotation = Quaternion.Euler(0, Random.Range(0, 1) * 180 + 90, 0);
                Instantiate(Pillar, position, rotation, wallParent);
            }
        }

        ChestsGen();
        RuinsGen(); 
    }
    
   public void RuinsGen(){
        var ruinsAmount = Random.Range(1, 4);
        for(int i  = 0; i < ruinsAmount; i++)
        {     
            Vector3 position = GetRandomEmptyTile(); 
            var rotation = Quaternion.Euler(0, Random.Range(0, 360) ,0);
            var currRuin = GameObject.Instantiate(Ruins, position, rotation, wallParent);
        }        
   }

   public void ChestsGen(){
    int chestsAmount = Random.Range(1, 4);
        for(int i  = 0; i < chestsAmount; i++)
        {     
            Vector3 position = GetRandomEmptyTile(); 
            var rotation = Quaternion.Euler(0, Random.Range(0, 360) ,0);
            Instantiate(Chest, position, rotation, wallParent);
        }
   }

   public Vector3 GetRandomEmptyTile() {
        Vector3 position = new Vector3(Random.Range(0, 4), 0, Random.Range(0, 4));
        while (occupiedPositions.Contains(position))
        {
            position = new Vector3(Random.Range(0, 4), 0, Random.Range(0, 4));
        }
        occupiedPositions.Add(position);
        return position;
   }

   
   void ChangeCurrentFloorAngle(){
    if(currentAngle.eulerAngles.x == startAngle.eulerAngles.x){
                currentAngle = finishAngle;
            }
   }   

    public void OnCardGameStart(){
        mainCamera.SetActive(false);
        cardGame.gameObject.SetActive(true);
        Swipe.gameObject.SetActive(false);
    }    
}
}