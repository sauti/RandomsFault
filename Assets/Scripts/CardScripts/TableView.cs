using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Default {
    public class TableView : MonoBehaviour
    {
        // public GameObject cellPrefab;
        // public GameObject cardPrefab;
        // public Vector2 padding = new Vector2(2f, 3f);

        // private void Awake()
        // {
        //     _cells = new List<GameObject>();
        //     _cards = new List<CardView>();
        // }

        // private void GenerateCells(CardData[,] grid)
        // {
        //     foreach (var data in grid)
        //     {
        //         var coord = data.Coord;
        //         var cellObj = Instantiate(cellPrefab, coord, Quaternion.Euler(0, 0, 0), gameObject.transform);
        //         // go.transform.localPosition = coord;
        //         _cells.Add(go);
        //     }
        // }

        // private void GenerateCards(List<Card> cards)
        // {
        //     foreach (var data in cards)
        //     {
        //         if (data.Type == EntityType.Nope)
        //             continue;
            
        //         var coord = cellSize * data.Coord;
        //         if (data.Type == EntityType.Player)
        //         {
        //             playerEntityView.SetPos(coord);
        //             playerEntityView.SetIndex(data.Idx);
        //         }
            
        //         if (!_entitiesConfig.TryGet(data.Type, out var entityPrefab))
        //             continue;
            
        //         var entity = Instantiate(entityPrefab, transform);
        //         entity.SetPos(coord);
        //         entity.SetIndex(data.Idx);
        //         _entities.Add(entity);
        //     }
        // }
    }
}
