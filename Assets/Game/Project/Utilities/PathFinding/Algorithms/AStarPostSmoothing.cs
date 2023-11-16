using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Utilities.AI
{
    public class AStarPostSmoothing : AStar
    {
        protected override List<NodeCell> CalculatePath(NodeCell endNode)
        {
            List<NodeCell> path = base.CalculatePath(endNode);
            int k = 0;
            List<NodeCell> smoothPath = new List<NodeCell> { path[0] };
            for (int i = 1; i < path.Count - 1; i++)
            {
                List<NodeCell> lineOfSightCells = grid.GetCellsFromLine(smoothPath[k].X, smoothPath[k].Y, path[i + 1].X, path[i + 1].Y);
                if (lineOfSightCells.Any(x => x.IsWalkable == false))
                {
                    k++;
                    smoothPath.Add(path[i]);
                }
            }
            smoothPath.Add(path[path.Count - 1]);
            return smoothPath;
        }
    }
}