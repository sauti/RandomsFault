using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Default
{
    public class GridView : MonoBehaviour
    {
        [SerializeField] 
        private Vector2 cellSize;
        
        [SerializeField] 
        private CellsConfig _cellsConfig;
        
        [SerializeField] 
        private EntitiesConfig _entitiesConfig;

        [SerializeField] 
        private EntityView playerEntityView;

        private List<GameObject> _cells;
        private List<EntityView> _entities;

        private void Awake()
        {
            _cells = new List<GameObject>();
            _entities = new List<EntityView>();
        }

        private void ClearField()
        {
            foreach (var cell in _cells)
            {
                Destroy(cell);
            }
            
            foreach (var entity in _entities)
            {
                Destroy(entity.gameObject);
            }
        }
        
        public void GenerateField(CellData[,] grid, List<Entity> entities)
        {
            ClearField();
            GenerateCells(grid);
            GenerateEntities(entities);
        }

        public void MoveEntityTo(int idx, Vector2Int coord)
        {
            var pos = cellSize * coord;
            EntityView entityView = null;
            if (playerEntityView.Idx == idx)
                entityView = playerEntityView;
            else
                entityView = _entities.FirstOrDefault(x => x.Idx == idx);
            
            if (entityView)
                entityView.SetPos(pos);
        }
        
        private void GenerateCells(CellData[,] grid)
        {
            foreach (var data in grid)
            {
                var coord = cellSize * data.Coord;
                if (!_cellsConfig.TryGet(data.Type, out var prefab))
                    continue;

                var go = Instantiate(prefab, transform);
                go.transform.localPosition = coord;
                _cells.Add(go);
            }
        }

        private void GenerateEntities(List<Entity> entities)
        {
            foreach (var data in entities)
            {
                if (data.Type == EntityType.Nope)
                    continue;
            
                var coord = cellSize * data.Coord;
                if (data.Type == EntityType.Player)
                {
                    playerEntityView.SetPos(coord);
                    playerEntityView.SetIndex(data.Idx);
                }
            
                if (!_entitiesConfig.TryGet(data.Type, out var entityPrefab))
                    continue;
            
                var entity = Instantiate(entityPrefab, transform);
                entity.SetPos(coord);
                entity.SetIndex(data.Idx);
                _entities.Add(entity);
            }
        }
    }
}