using System.Collections.Generic;
using UnityEngine;

namespace Default
{
    public class GridController : MonoBehaviour
    {
        private CellData[,] _cells;
        private List<Entity> _entities;

        [SerializeField] 
        private Vector2Int _gridSize;
        
        [SerializeField] 
        private GridView _view;

        private bool _hasField;
        private Entity _player;
        
        public void GenerateField()
        {
            _cells = new CellData[_gridSize.y, _gridSize.x];
            
            for (var i = 0; i != _cells.GetLength(0); i++)
            {
                for (var j = 0; j != _cells.GetLength(1); j++)
                {
                    _cells[i, j] = new CellData()
                    {
                        Coord = new Vector2Int(j, i),
                        Type = GetRndCell()
                    };
                }
            }

            var exitCoord = FindEmptyCell();
            _cells[exitCoord.x, exitCoord.y].Type = CellType.Exit;
            
            _player = new Entity()
            {
                Idx = 0,
                Type = EntityType.Player,
                Coord = FindEmptyCell()
            };

            _view.GenerateField(_cells, new List<Entity>(){_player});
            _hasField = true;
        }

        public void MovePlayerDelta(Vector2Int dir)
        {
            if (!_hasField)
                return;
            
            var coord = _player.Coord + dir;
            if (!InBound(ref coord))
                return;

            switch (_cells[coord.y, coord.x].Type)
            {
                case CellType.Obstacle:
                    return;
                case CellType.Exit:
                    GenerateField();
                    return;
            }
            
            _player.Coord = coord;
            _view.MoveEntityTo(_player.Idx, coord);
        }
        
        private Vector2Int FindEmptyCell()
        {
            while (true)
            {
                var coord = new Vector2Int(
                    Random.Range(0, _gridSize.x),
                    Random.Range(0, _gridSize.y));
                if (IsEmpty(ref coord))
                    return coord;
            }
        }

        private bool IsCellType(ref Vector2Int coord, CellType type)
        {
            if (coord.x < 0 || coord.x > _gridSize.x - 1 ||
                coord.y < 0 || coord.y > _gridSize.y - 1)
                return false;
            
            return _cells[coord.y, coord.x].Type != CellType.Obstacle;
        }

        private bool InBound(ref Vector2Int coord)
        {
            return (coord.x >= 0 && coord.x < _gridSize.x &&
                    coord.y >= 0 && coord.y < _gridSize.y);
        }
        
        private bool IsEmpty(ref Vector2Int coord)
        {
            return IsCellType(ref coord, CellType.Empty);
        }
        
        private CellType GetRndCell()
        {
            return Random.Range(0, 1f) > 0.2 ? CellType.Empty : CellType.Obstacle;
        }
    }
}