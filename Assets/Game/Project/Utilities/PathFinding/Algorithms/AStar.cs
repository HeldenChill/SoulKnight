using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities.AI
{
    public partial class AStar : Grid<NodeCell, int>.PathfindingAlgorithm
    {
        protected const int MOVE_STRAIGHT_COST = 10;
        protected const int MOVE_DIAGONAL_COST = 14;

        protected List<NodeCell> openList;
        protected List<NodeCell> closeList;

        public AStar()
        {

            openList = new List<NodeCell>();
            closeList = new List<NodeCell>();
        }

        public override List<NodeCell> FindPath(int startX, int startY, int endX, int endY, Grid<NodeCell, int> grid)
        {
            this.grid = grid;
            openList.Clear();
            closeList.Clear();
            NodeCell startNode = grid.GetGridCell(startX, startY);
            NodeCell endNode = grid.GetGridCell(endX, endY);
            if (!endNode.IsWalkable || !startNode.IsWalkable) return null;
            openList.Add(startNode);

            for (int x = 0; x < grid.Width; x++)
            {
                for (int y = 0; y < grid.Height; y++)
                {
                    NodeCell cell = grid.GetGridCell(x, y);
                    cell.GCost = int.MaxValue;
                    cell.CalculateFCost();
                    cell.Parent = null;
                    cell.IsScanned = false;
                }
            }

            startNode.GCost = 0;
            startNode.HCost = CalculateDistanceCost(startNode, endNode);
            startNode.CalculateFCost();

            while (openList.Count > 0)
            {
                NodeCell currentNode = GetLowestFCostCell(openList);
                if (currentNode == endNode)
                {
                    //NOTE: Reach
                    startNode.Parent = null;
                    return CalculatePath(endNode);
                }

                openList.Remove(currentNode);
                closeList.Add(currentNode);

                //NOTE: Calculate FCost for all neighbour cells
                //NOTE: Add Node to Open List and Close List
                ScanningNeighbour(endNode, currentNode);
            }

            return null;
        }
        //NOTE: Scanning neighbours cells
        protected virtual void ScanningNeighbour(NodeCell endNode, NodeCell currentNode)
        {
            foreach (NodeCell neighbourNode in GetNeighbourNodes(currentNode))
            {
                if (closeList.Contains(neighbourNode))
                    continue;
                if (!neighbourNode.IsWalkable)
                {
                    closeList.Add(neighbourNode);
                    continue;
                }

                UpdateFCost(endNode, currentNode, neighbourNode);

                if (!openList.Contains(neighbourNode))
                {
                    openList.Add(neighbourNode);
                }
            }
        }

        protected virtual void UpdateFCost(NodeCell endNode, NodeCell currentNode, NodeCell neighbourNode)
        {
            int tentativeGCost = currentNode.GCost + CalculateDistanceCost(currentNode, neighbourNode);
            if (tentativeGCost < neighbourNode.GCost)
            {
                neighbourNode.Parent = currentNode;
                neighbourNode.GCost = tentativeGCost;
                neighbourNode.HCost = CalculateDistanceCost(neighbourNode, endNode);
                neighbourNode.CalculateFCost();
            }
        }

        protected virtual List<NodeCell> GetNeighbourNodes(NodeCell currentNode)
        {
            List<NodeCell> neighbourNodes = new List<NodeCell>();

            if (currentNode.X - 1 >= 0)
            {
                neighbourNodes.Add(grid.GetGridCell(currentNode.X - 1, currentNode.Y));

                if (currentNode.Y - 1 >= 0) neighbourNodes.Add(grid.GetGridCell(currentNode.X - 1, currentNode.Y - 1));
                if (currentNode.Y + 1 < grid.Height) neighbourNodes.Add(grid.GetGridCell(currentNode.X - 1, currentNode.Y + 1));
            }

            if (currentNode.X + 1 < grid.Width)
            {
                neighbourNodes.Add(grid.GetGridCell(currentNode.X + 1, currentNode.Y));

                if (currentNode.Y - 1 >= 0) neighbourNodes.Add(grid.GetGridCell(currentNode.X + 1, currentNode.Y - 1));
                if (currentNode.Y + 1 < grid.Height) neighbourNodes.Add(grid.GetGridCell(currentNode.X + 1, currentNode.Y + 1));
            }

            if (currentNode.Y - 1 >= 0) neighbourNodes.Add(grid.GetGridCell(currentNode.X, currentNode.Y - 1));
            if (currentNode.Y + 1 < grid.Height) neighbourNodes.Add(grid.GetGridCell(currentNode.X, currentNode.Y + 1));

            return neighbourNodes;
        }
        protected virtual List<NodeCell> CalculatePath(NodeCell endNode)
        {
            CacheList.Clear();
            CacheList.Add(endNode);
            NodeCell currentNode = endNode;
            while (currentNode.Parent != null)
            {
                CacheList.Add(currentNode.Parent);
                currentNode = currentNode.Parent;
            }
            CacheList.Reverse();
            return CacheList;
        }
        protected int CalculateDistanceCost(NodeCell a, NodeCell b)
        {
            int xDistance = Mathf.Abs(a.X - b.X);
            int yDistance = Mathf.Abs(a.Y - b.Y);
            int remaining = Mathf.Abs(xDistance - yDistance);
            return MOVE_DIAGONAL_COST * Mathf.Min(xDistance, yDistance) + MOVE_STRAIGHT_COST * remaining;
        }
        //DEV: Can change List -> Heap for optimize
        protected virtual NodeCell GetLowestFCostCell(List<NodeCell> nodeCellList)
        {
            NodeCell lowestFCostNode = nodeCellList[0];
            for (int i = 1; i < nodeCellList.Count; i++)
            {
                if (nodeCellList[i].FCost < lowestFCostNode.FCost)
                {
                    lowestFCostNode = nodeCellList[i];
                }
            }
            return lowestFCostNode;
        }
    }
}