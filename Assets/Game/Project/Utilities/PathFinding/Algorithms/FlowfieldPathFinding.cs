using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using Utilities.AI;

namespace Utilities.AI
{
    public class FlowfieldPathFinding : Grid<NodeCell, int>.PathfindingAlgorithm
    {      
        private NodeCell DestinationCell;
        public override List<NodeCell> FindPath(int startX, int startY, int endX, int endY, Grid<NodeCell, int> grid)
        {
            this.grid = grid;
            NodeCell startNode = grid.GetGridCell(startX, startY);
            NodeCell endNode = grid.GetGridCell(endX, endY);
            if (!endNode.IsWalkable) return null;
            if (endNode != null)
                DestinationCell = endNode;
            if (IsGridUpdate)
            {
                GridUpdate(grid);
                IsGridUpdate = false;
            }
            AlgorithmUpdate(grid);
            NodeCell currentNode = startNode;
            while (currentNode != endNode)
            {
                NodeCell parentNode = grid.GetGridCell(currentNode.X + currentNode.FieldVector.x, currentNode.Y + currentNode.FieldVector.y);
                currentNode.Parent = parentNode;
                currentNode = parentNode;
            }
            endNode.Parent = null;
            return CalculatePath(startNode);
        }
        public override void AlgorithmUpdate(Grid<NodeCell, int> grid = null)
        {
            int oldHCost = DestinationCell.HCost;
            if (grid == null)
            {
                if (this.grid == null) return;
            }
            else
            {
                this.grid = grid;
            }

            if (DestinationCell == null) return;
            Queue<NodeCell> openList = new Queue<NodeCell>();

            foreach (NodeCell cell in this.grid.GridArray)
            {
                cell.GCost = 0;
                cell.ResetFCost();
                cell.FieldVector.Set(0, 0);
            }

            DestinationCell.HCost = 0;
            DestinationCell.GCost = 0;
            DestinationCell.CalculateFCost();
            openList.Enqueue(DestinationCell);   
            
            //NOTE: Calculate heat map
            while(openList.Count > 0)
            {
                NodeCell checkCell = openList.Dequeue();
                foreach(NodeCell cell in GetNeighbourNodes(checkCell))
                {
                    //NOTE: When cell is wall
                    if (cell.HCost == CONSTANTS.WALL_COST)
                    {
                        cell.CalculateFCost();
                        continue;
                    }

                    //NOTE: When cell is checked or is destination
                    if (cell.FCost != 0 || cell.HCost == 0) continue;
                    cell.GCost = checkCell.FCost;
                    cell.CalculateFCost();
                    openList.Enqueue(cell);
                }
            }

            //NOTE: Calculate field vector
            openList.Enqueue(DestinationCell);
            foreach(NodeCell checkCell in this.grid.GridArray)
            {
                if (checkCell.HCost == 0 || checkCell.HCost == CONSTANTS.WALL_COST) continue;
                NodeCell minCell = null;
                foreach(NodeCell cell in GetNeighbourNodes(checkCell))
                {
                    if(minCell == null)
                    {
                        minCell = cell;
                        continue;
                    }
                    else
                    {
                        if(cell.FCost < minCell.FCost)
                        {
                            minCell = cell;
                        }
                    }
                }
                checkCell.FieldVector.Set(minCell.X - checkCell.X, minCell.Y - checkCell.Y);
            }
            DestinationCell.HCost = oldHCost;
        }
        public override void GridUpdate(Grid<NodeCell, int> grid = null)
        {
            if(grid == null)
            {
                if (this.grid == null) return;
            }
            else
            {
                this.grid = grid;
            }
            foreach(NodeCell cell in this.grid.GridArray)
            {
                if (cell.FieldVector == null) cell.FieldVector = new Vector2Int();               
                if (cell.IsWalkable)
                {
                    cell.HCost = CONSTANTS.GROUND_COST;
                }
                else
                {
                    cell.HCost = CONSTANTS.WALL_COST;
                }
            }
        }
        protected virtual List<NodeCell> CalculatePath(NodeCell startNode)
        {
            List<NodeCell> path = new List<NodeCell> { startNode };
            NodeCell currentNode = startNode;
            while (currentNode.Parent != null)
            {
                path.Add(currentNode.Parent);
                currentNode = currentNode.Parent;
            }
            return path;
        }
        protected virtual List<NodeCell> GetNeighbourNodes(NodeCell currentNode)
        {
            List<NodeCell> res = new List<NodeCell>();
            NodeCell cell;
            for(int i = -1; i <= 1; i++)
            {
                for(int j = -1; j <= 1; j++)
                {
                    if (i == 0 && j == 0) continue;
                    cell = grid.GetGridCell(currentNode.X + i,currentNode.Y + j);
                    if(cell != null)
                        res.Add(cell);
                }
            }
            return res;
        }
    }
}