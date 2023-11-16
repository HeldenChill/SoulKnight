using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Utilities.AI
{
    public class LazyThetaStar : AStar
    {
        protected override void UpdateFCost(NodeCell endNode, NodeCell currentNode, NodeCell neighbourNode)
        {
            int tentativeCost = 0;
            if(currentNode.Parent != null)
                tentativeCost = currentNode.Parent.GCost + CalculateDistanceCost(currentNode.Parent, neighbourNode);
            else
                tentativeCost = currentNode.GCost + CalculateDistanceCost(currentNode, neighbourNode);

            if (tentativeCost < neighbourNode.GCost)
            {
                //NOTE: Parent of neighbour node is still current node,but assumes update its fcost base on current node parent
                neighbourNode.Parent = currentNode;
                neighbourNode.GCost = tentativeCost;
                neighbourNode.HCost = CalculateDistanceCost(neighbourNode, endNode);
                neighbourNode.CalculateFCost();
            }
        }

        protected override NodeCell GetLowestFCostCell(List<NodeCell> nodeCellList)
        {
            NodeCell neighbourNode = base.GetLowestFCostCell (nodeCellList);
            NodeCell currentNode = neighbourNode.Parent;
            //NOTE: Checking real line of sight here
            List<NodeCell> lineOfSightCells = null;

            if (currentNode == null || currentNode.Parent == null) return neighbourNode;
            lineOfSightCells = grid.GetCellsFromLine(currentNode.Parent.X, currentNode.Parent.Y, neighbourNode.X, neighbourNode.Y);

            //NOTE: If it really has line of sight between neighbour and current node parent
            if (lineOfSightCells != null && !lineOfSightCells.Any(c => c.IsWalkable == false))
            {
                //NOTE: Update parent for neighbour node
                neighbourNode.Parent = currentNode.Parent;
            }
            else
            {
                //NOTE: Re-update gcost because it don't have line of sight bettween neighbour and current node parent
                neighbourNode.GCost = currentNode.GCost + CalculateDistanceCost(currentNode, neighbourNode);
                neighbourNode.CalculateFCost();
                //NOTE: Continue to find node that have lowest gcost
                
            }
            return neighbourNode;
        }
    }
}