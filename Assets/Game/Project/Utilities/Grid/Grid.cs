using Game;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities.AI
{
    public class Grid<T, D> where T : GridCell<D>
    {
        private int width;
        private int height;
        private float cellSize;
        private T[,] gridArray;
        private TextMesh[,] debugTextArray;
        private Vector3 originPosition;
        private CONSTANTS.PLANE planeType;

        public float CellSize => cellSize;
        public int Width => width;
        public int Height => height;
        public CONSTANTS.PLANE PlaneType => planeType;
        public T[,] GridArray => gridArray;
        //private List<T> LineOfSightCacheList = new List<T>();
        public Grid(int width, int height, float cellSize, Vector3 originPosition = default, Func<GridCell<D>> ConstructorCell = null, CONSTANTS.PLANE planeType = CONSTANTS.PLANE.XY)
        {
            this.width = width;
            this.height = height;
            this.cellSize = cellSize;
            this.originPosition = originPosition;
            this.planeType = planeType;

            gridArray = new T[width, height];
            debugTextArray = new TextMesh[width, height];

            for (int x = 0; x < gridArray.GetLength(0); x++)
            {
                for (int y = 0; y < gridArray.GetLength(1); y++)
                {
                    gridArray[x, y] = (T)ConstructorCell();
                    gridArray[x, y].SetCellPosition(x, y);
                    gridArray[x, y].Size = cellSize;
                    gridArray[x, y].PlaneType = planeType;
                    switch (planeType)
                    {
                        case CONSTANTS.PLANE.XY:
                            gridArray[x, y].UpdateWorldPosition(originPosition.x, originPosition.y);
                            break;
                        case CONSTANTS.PLANE.XZ:
                            gridArray[x, y].UpdateWorldPosition(originPosition.x, originPosition.z);
                            break;
                        case CONSTANTS.PLANE.YZ:
                            gridArray[x, y].UpdateWorldPosition(originPosition.y, originPosition.z);
                            break;
                    }

                    gridArray[x, y]._OnValueChange += OnGridCellValueChange;
                }
            }


        }
        public Vector3 GetWorldPosition(int x, int y)
        {
            return GetUnitVector3(x, y) * cellSize + originPosition;
        }
        public (int, int) GetGridPosition(Vector3 worldPosition)
        {
            Vector3 realPos = worldPosition - originPosition;
            switch (planeType)
            {
                case CONSTANTS.PLANE.XY:
                    return (Mathf.FloorToInt(realPos.x / cellSize), Mathf.FloorToInt(realPos.y / cellSize));
                case CONSTANTS.PLANE.XZ:
                    return (Mathf.FloorToInt(realPos.x / cellSize), Mathf.FloorToInt(realPos.z / cellSize));
                case CONSTANTS.PLANE.YZ:
                    return (Mathf.FloorToInt(realPos.y / cellSize), Mathf.FloorToInt(realPos.z / cellSize));
            }
            return default;
        }
        public void SetGridCell(int x, int y, T value)
        {
            if (value == null) return;
            if (x >= 0 && y >= 0 && x < width && y < height)
            {
                gridArray[x, y]._OnValueChange -= OnGridCellValueChange;
                gridArray[x, y] = value;
                gridArray[x, y]._OnValueChange += OnGridCellValueChange;
            }
        }
        public void SetGridCell(Vector3 position, T value)
        {
            int x, y;
            (x, y) = GetGridPosition(position);
            SetGridCell(x, y, value);
        }
        public T GetGridCell(int x, int y)
        {
            if (x >= 0 && y >= 0 && x < width && y < height)
            {
                return gridArray[x, y];
            }
            return default;
        }
        public T GetGridCell(Vector3 worldPosition)
        {
            int x, y;
            (x, y) = GetGridPosition(worldPosition);
            if (x >= 0 && y >= 0 && x < width && y < height)
            {
                return gridArray[x, y];
            }
            return default;
        }
        private void OnGridCellValueChange(int x, int y)
        {
            debugTextArray[x, y].text = gridArray[x, y].ToString();
        }
        private Vector3 GetUnitVector3(float val1, float val2)
        {
            switch (planeType)
            {
                case CONSTANTS.PLANE.XY:
                    return new Vector3(val1, val2, 0);
                case CONSTANTS.PLANE.XZ:
                    return new Vector3(val1, 0, val2);
                case CONSTANTS.PLANE.YZ:
                    return new Vector3(0, val1, val2);
            }
            return default;
        }
        public List<T> GetCellsFromLine(int startCellX, int startCellY, int endCellX, int endCellY)
        {
            int detaX = endCellX - startCellX;
            int detaY = endCellY - startCellY;
            float rate = 1;
            List<T> cells = new List<T>();

            if (Mathf.Abs(detaX) > Mathf.Abs(detaY))
            {
                rate = (float)detaY / detaX;
                for (int i = startCellX; i != endCellX; i += Math.Sign(endCellX - startCellX))
                {
                    int posY1 = Mathf.RoundToInt((i - 0.5f - startCellX) * rate + startCellY);
                    int posY2 = Mathf.RoundToInt((i + 0.5f - startCellX) * rate + startCellY);
                    if (posY1 == posY2)
                        cells.Add(gridArray[i, posY1]);
                    else
                    {
                        cells.Add(gridArray[i, posY1]);
                        cells.Add(gridArray[i, posY2]);
                    }
                }
            }
            else if (Mathf.Abs(detaY) > Mathf.Abs(detaX))
            {
                rate = (float)detaX / detaY;
                for (int i = startCellY; i != endCellY; i += Math.Sign(endCellY - startCellY))
                {
                    int posX1 = Mathf.RoundToInt((i - 0.5f - startCellY) * rate + startCellX);
                    int posX2 = Mathf.RoundToInt((i + 0.5f - startCellY) * rate + startCellX);
                    if (posX1 == posX2)
                        cells.Add(gridArray[posX1, i]);
                    else
                    {
                        cells.Add(gridArray[posX1, i]);
                        cells.Add(gridArray[posX2, i]);
                    }
                }
            }
            else
            {
                rate = (float)detaY / detaX;
                for (int i = startCellX; i != endCellX; i += Math.Sign(endCellX - startCellX))
                {
                    //DEV: Index out of range here
                    int posY = Mathf.RoundToInt((i - startCellX) * rate + startCellY);
                    cells.Add(gridArray[i, posY]);
                }
            }
            cells.Add(gridArray[endCellX, endCellY]);
            return cells;
        }


        #region VISIT CLASS
        public abstract class PathfindingAlgorithm
        {
            public bool IsGridUpdate = false;
            public List<NodeCell> CacheList;
            protected Grid<T, D> grid;
            public abstract List<T> FindPath(int startX, int startY, int endX, int endY, Grid<T, D> grid);
            public virtual void AlgorithmUpdate(Grid<T, D> grid = null) { }
            public virtual void GridUpdate(Grid<T, D> grid = null) { }
        }
        public class DebugGrid
        {
            private readonly Vector2 WALKABLE_UV = new Vector2(9f / 334, 0);
            private readonly Vector2 UNWALKABLE_UV = Vector2.zero;

            public virtual void DrawGrid(Grid<T, D> grid, bool isPositionShow = false)
            {
                for (int x = 0; x < grid.gridArray.GetLength(0); x++)
                {
                    for (int y = 0; y < grid.gridArray.GetLength(1); y++)
                    {
                        if (isPositionShow)
                        {
                            grid.debugTextArray[x, y] = GridUtilities.CreateWorldText(grid.gridArray[x, y].ToString(), null
                                , grid.GetWorldPosition(x, y) + new Vector3(grid.cellSize / 2, grid.cellSize / 2), 20, Color.white, TextAnchor.MiddleCenter);
                        }
                        Debug.DrawLine(grid.GetWorldPosition(x, y), grid.GetWorldPosition(x, y + 1), Color.white, 10000f, true);
                        Debug.DrawLine(grid.GetWorldPosition(x, y), grid.GetWorldPosition(x + 1, y), Color.white, 10000f, true);
                    }
                }
                Debug.DrawLine(grid.GetWorldPosition(0, grid.height), grid.GetWorldPosition(grid.width, grid.height), Color.white, 10000f);
                Debug.DrawLine(grid.GetWorldPosition(grid.width, 0), grid.GetWorldPosition(grid.width, grid.height), Color.white, 10000f);
            }
            public virtual void UpdateVisualMap(Grid<NodeCell, int> grid, Mesh mesh)
            {
                GridUtilities.CreateEmptyMeshArray(grid.Width * grid.Height, out Vector3[] vertices, out Vector2[] uv, out int[] triangles);
                for (int x = 0; x < grid.Width; x++)
                {
                    for (int y = 0; y < grid.Height; y++)
                    {
                        int index = x * grid.Height + y;
                        Vector3 quadSize = new Vector3(1, 1) * grid.CellSize;
                        NodeCell cell = grid.GetGridCell(x, y);
                        if (cell.IsWalkable)
                        {
                            GridUtilities.AddToMeshArray(vertices, uv, triangles, index, cell.WorldPos, 0f, quadSize, WALKABLE_UV, WALKABLE_UV);
                        }
                        else
                        {
                            GridUtilities.AddToMeshArray(vertices, uv, triangles, index, cell.WorldPos, 0f, quadSize, UNWALKABLE_UV, UNWALKABLE_UV);
                        }
                    }
                }

                mesh.vertices = vertices;
                mesh.uv = uv;
                mesh.triangles = triangles;
            }
            public virtual void ShowFCost(Grid<NodeCell, int> grid)
            {
                foreach (NodeCell cell in grid.GridArray)
                {
                    for (int x = 0; x < grid.gridArray.GetLength(0); x++)
                    {
                        for (int y = 0; y < grid.gridArray.GetLength(1); y++)
                        {
                            if (grid.debugTextArray[x, y] == null)
                                grid.debugTextArray[x, y] = GridUtilities.CreateWorldText(grid.gridArray[x, y].FCost.ToString(), null
                                , grid.GetWorldPosition(x, y) + new Vector3(grid.cellSize / 2, grid.cellSize / 2), 20, Color.white, TextAnchor.MiddleCenter);

                            if (grid.gridArray[x, y].FCost > 100000)
                            {
                                grid.debugTextArray[x, y].text = "Inf";
                                grid.debugTextArray[x, y].color = Color.black;
                            }
                            else if (grid.gridArray[x, y].FCost < -100000)
                            {
                                grid.debugTextArray[x, y].text = "-Inf";
                                grid.debugTextArray[x, y].color = Color.black;
                            }
                            else
                            {
                                grid.debugTextArray[x, y].text = grid.gridArray[x, y].FCost.ToString();
                                grid.debugTextArray[x, y].color = Color.white;
                            }
                        }
                    }
                }
            }
            public virtual void DrawPath(List<T> path)
            {
                if (path != null)
                {
                    for (int i = 0; i < path.Count - 1; i++)
                    {
                        Debug.DrawLine(new Vector3(path[i].WorldX, path[i].WorldY), new Vector3(path[i + 1].WorldX, path[i + 1].WorldY), Color.cyan, 5f);
                    }
                }
            }
        }
        #endregion
    }

}