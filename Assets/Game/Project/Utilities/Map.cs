using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project
{
    using Utilities;
    using Utilities.AI;
    public class Map : MonoBehaviour
    {
        
        Grid<NodeCell, int> grid;
        Grid<NodeCell, int>.DebugGrid debug;

        private Mesh mesh;
        private Obstance[] obstances;
        public Grid<NodeCell, int> MapGrid => grid;
        void Awake()
        {
            grid = new Grid<NodeCell, int>(56, 44, 2, new Vector2(-56, -45) ,() => new NodeCell());
            debug = new Grid<NodeCell, int>.DebugGrid();
            obstances = FindObjectsOfType<Obstance>();
            UpdateObstance();

            mesh = new Mesh();
            GetComponent<MeshFilter>().mesh = mesh;
            debug.DrawGrid(grid);
            debug.UpdateVisualMap(grid, mesh);
        }
        // Update is called once per frame
        private void Update()
        {
            if (Input.GetMouseButtonDown(1))
            {
                Vector3 mousePos = GridUtilities.GetMouseWorldPosition();
                (int x, int y) = grid.GetGridPosition(mousePos);

                NodeCell cell = grid.GetGridCell(x, y);
                cell.IsWalkable = !cell.IsWalkable;
                debug.UpdateVisualMap(grid, mesh);
                Dispatcher.Inst.PostEvent(EVENT_ID.MAP_UPDATE);
            }          
        }

        public void DrawPath(List<NodeCell> path)
        {
            debug.DrawPath(path);
        }
        public void ShowFCost()
        {
            debug.ShowFCost(grid);
        }
        private void UpdateObstance()
        {
            foreach(Obstance obstance in obstances)
            {
                foreach(Vector2 point in obstance.PointExists)
                {
                    grid.GetGridCell(point).IsWalkable = false;
                }
            }
        }
    }
}