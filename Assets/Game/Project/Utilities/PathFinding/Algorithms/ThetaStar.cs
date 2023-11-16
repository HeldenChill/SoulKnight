using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Utilities.AI
{
    public class ThetaStar : AStar
    {
        protected override void UpdateFCost(NodeCell endNode, NodeCell currentNode, NodeCell neighbourNode)
        {
            int tentativeCost = 0;
            List<NodeCell> lineOfSightCells = null;
            NodeCell parent = null;

            if (currentNode.Parent != null) 
                lineOfSightCells = grid.GetCellsFromLine(currentNode.Parent.X, currentNode.Parent.Y, neighbourNode.X, neighbourNode.Y);

            if(lineOfSightCells != null && !lineOfSightCells.Any(c => c.IsWalkable == false))
            {
                parent = currentNode.Parent;
            }
            else
            {
                parent = currentNode;
            }

            tentativeCost = parent.GCost + CalculateDistanceCost(parent, neighbourNode);
            if (tentativeCost < neighbourNode.GCost)
            {
                neighbourNode.Parent = parent;
                neighbourNode.GCost = tentativeCost;
                neighbourNode.HCost = CalculateDistanceCost(neighbourNode, endNode);
                neighbourNode.CalculateFCost();
            }
        }
    }
}