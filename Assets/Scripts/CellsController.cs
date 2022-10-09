
using UnityEngine;

namespace CellsController
{
    public enum CellType
    {
        Nope,
        Obstacle,
        Obstacle1,
        Obstacle2,
        Exit
    }

    public abstract class Entity
    {
        
    }

    public class Cell
    {
        public CellType Type;
        public Entity Entity;
    }

    public class Grid 
    {
        public Cell[,] Cell;

        private bool isBound(Vector2Int coord)
        {
            return coord.x >= 0 && coord.x < Cell.GetLength(dimension: 0)
                                && coord.y >= 0 && coord.y < Cell.GetLength(dimension: 1);
        }

        public bool TryMove(Vector2Int coord)
        {
            if (!isBound(coord))
            return false;

            var cell = Cell[coord.x, coord.y];
            switch (cell.Type)
            {
                case CellType.Obstacle or CellType.Obstacle1 or CellType.Obstacle2:
                    return false;
                case CellType.Exit:
                    return true;
                case CellType.Nope:
                    return cell.Entity != null;
                default:
                    return false;
            }
        }
    }
}
