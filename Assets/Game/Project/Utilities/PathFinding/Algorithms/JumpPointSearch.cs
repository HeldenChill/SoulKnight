using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Utilities.AI
{
    public class JumpPointSearch : AStar
    {
        NodeCell endNode;
        NodeCell startScanCell;
        protected override void ScanningNeighbour(NodeCell endNode, NodeCell currentNode)
        {
            this.endNode = endNode;
            this.startScanCell = currentNode;
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

        protected override List<NodeCell> GetNeighbourNodes(NodeCell currentNode)
        {
            HashSet<NodeCell> neighbourNodes = new HashSet<NodeCell>();
            Vector2Int direction = new Vector2Int();
            NodeCell checkingCell;
            #region CHECK CURRENT CELL
            ScanAroundCell(currentNode, ref neighbourNodes);
            #endregion
            #region SCAN AROUND

            direction.Set(0, -1);
            ScanCell();
            direction.Set(0, 1);
            ScanCell();
            direction.Set(1, 0);
            ScanCell();
            direction.Set(-1, 0);
            ScanCell();
            DiagonallyScan(currentNode, ref neighbourNodes, 1, 1);
            DiagonallyScan(currentNode, ref neighbourNodes, -1, 1);
            DiagonallyScan(currentNode, ref neighbourNodes, 1, -1);
            DiagonallyScan(currentNode, ref neighbourNodes, -1, -1);                   
            #endregion
            void ScanCell()
            {
                checkingCell = DirectionForceNeighourScan(currentNode, direction);
                if (checkingCell != null)
                {
                    neighbourNodes.Add(checkingCell);
                }
            }

            return neighbourNodes.ToList();

        }

        protected void ScanAroundCell(NodeCell currentCell, ref HashSet<NodeCell> neighbourNodes)
        {
            NodeCell cell1 = grid.GetGridCell(currentCell.X + 1, currentCell.Y);
            NodeCell cell2 = grid.GetGridCell(currentCell.X - 1, currentCell.Y);
            NodeCell cell3 = grid.GetGridCell(currentCell.X, currentCell.Y + 1);
            NodeCell cell4 = grid.GetGridCell(currentCell.X, currentCell.Y - 1);
            NodeCell cell;

            if (cell1 != null && !cell1.IsWalkable)
            {
                cell = grid.GetGridCell(currentCell.X + 1, currentCell.Y + 1);
                if (cell != null && cell.IsWalkable)
                    neighbourNodes.Add(cell);

                cell = grid.GetGridCell(currentCell.X + 1, currentCell.Y - 1);
                if (cell != null && cell.IsWalkable)
                    neighbourNodes.Add(cell);
            }

            if (cell2 != null && !cell2.IsWalkable)
            {
                cell = grid.GetGridCell(currentCell.X - 1, currentCell.Y + 1);
                if (cell != null && cell.IsWalkable)
                    neighbourNodes.Add(cell);

                cell = grid.GetGridCell(currentCell.X - 1, currentCell.Y - 1);
                if (cell != null && cell.IsWalkable)
                    neighbourNodes.Add(cell);
            }

            if (cell3 != null && !cell3.IsWalkable)
            {
                cell = grid.GetGridCell(currentCell.X + 1, currentCell.Y + 1);
                if (cell != null && cell.IsWalkable)
                    neighbourNodes.Add(cell);

                cell = grid.GetGridCell(currentCell.X - 1, currentCell.Y + 1);
                if (cell != null && cell.IsWalkable)
                    neighbourNodes.Add(cell);
            }

            if (cell4 != null && !cell4.IsWalkable)
            {
                cell = grid.GetGridCell(currentCell.X + 1, currentCell.Y - 1);
                if (cell != null && cell.IsWalkable)
                    neighbourNodes.Add(cell);

                cell = grid.GetGridCell(currentCell.X - 1, currentCell.Y - 1);
                if (cell != null && cell.IsWalkable)
                    neighbourNodes.Add(cell);
            }
        }
        protected void DiagonallyScan(NodeCell currentNode, ref HashSet<NodeCell> neighbourNodes, int x, int y)
        {
            int index = 1;
            bool diagonallyScan = true;
            NodeCell diagonallyCell;
            NodeCell checkingCell;
            Vector2Int direction = new Vector2Int();

            while (diagonallyScan)
            {
                diagonallyCell = grid.GetGridCell(currentNode.X + index * x, currentNode.Y + index * y);
                if(diagonallyCell != null && diagonallyCell == endNode)
                    neighbourNodes.Add(endNode);
                if (diagonallyCell != null && diagonallyCell.IsWalkable != false)
                {
                    //if (diagonallyCell.IsScanned) return;
                    //diagonallyCell.IsScanned = true;

                    direction.Set(x, 0);
                    checkingCell = DirectionForceNeighourScan(diagonallyCell, direction);
                    if (checkingCell != null)
                    {
                        neighbourNodes.Add(diagonallyCell);
                        break;
                    }

                    direction.Set(0, y);
                    checkingCell = DirectionForceNeighourScan(diagonallyCell, direction);
                    if (checkingCell != null)
                    {
                        neighbourNodes.Add(diagonallyCell);
                        break;
                    }

                    direction.Set(x, y);
                    checkingCell = DirectionForceNeighourScan(diagonallyCell, direction);
                    if (checkingCell != null)
                    {
                        neighbourNodes.Add(checkingCell);
                        break;
                    }
                }
                else
                {
                    diagonallyScan = false;
                }
                index++;
            }
        }
        protected NodeCell DirectionForceNeighourScan(NodeCell currentNode, Vector2Int direction)
        {
            bool scanning = true;
            int index = 1;
            NodeCell forceNeighbour;
            NodeCell firstScanNode;

            if (Mathf.Abs(direction.x) > 0 && Mathf.Abs(direction.y) > 0)
            {
                //NOTE: Cross Scan
                firstScanNode = grid.GetGridCell(currentNode.X + direction.x, currentNode.Y + direction.y);
                //NOTE: Check first node
                if(firstScanNode == null || !firstScanNode.IsWalkable) return null;
                //if (firstScanNode.IsScanned) return null;
                //firstScanNode.IsScanned = true;

                if (firstScanNode != null && firstScanNode == endNode)
                    return endNode;

                NodeCell cell1 = grid.GetGridCell(firstScanNode.X + direction.x, firstScanNode.Y);
                NodeCell cell2 = grid.GetGridCell(firstScanNode.X, firstScanNode.Y + direction.y);

                if (cell1 != null && !cell1.IsWalkable)
                {
                    forceNeighbour = grid.GetGridCell(firstScanNode.X + 2 * direction.x, firstScanNode.Y);
                    if (forceNeighbour != null && forceNeighbour.IsWalkable)
                        return firstScanNode;
                }

                if (cell2 != null && !cell2.IsWalkable)
                {
                    forceNeighbour = grid.GetGridCell(firstScanNode.X, firstScanNode.Y + 2 * direction.y);
                    if (forceNeighbour != null && forceNeighbour.IsWalkable)
                        return firstScanNode;
                }
            }
            else if (Mathf.Abs(direction.x) > 0)
            {
                //NOTE: Horizontal Scan
                firstScanNode = grid.GetGridCell(currentNode.X + direction.x, currentNode.Y);
                while (scanning)
                {
                    NodeCell cell = grid.GetGridCell(currentNode.X + direction.x * index, currentNode.Y);
                    //NOTE: Check first node
                    if (cell == null || !cell.IsWalkable) break;
                    //if (cell.IsScanned) break;
                    //cell.IsScanned = true;
                    if (cell == endNode)
                        return endNode;

                    NodeCell cell1 = grid.GetGridCell(currentNode.X + direction.x * index, currentNode.Y + 1);
                    NodeCell cell2 = grid.GetGridCell(currentNode.X + direction.x * index, currentNode.Y - 1);

                    if (cell1 != null && !cell1.IsWalkable)
                    {
                        forceNeighbour = grid.GetGridCell(currentNode.X + direction.x * (index + 1), currentNode.Y + 1);
                        if (forceNeighbour != null && forceNeighbour.IsWalkable)
                            return cell;
                    }
                    if (cell2 != null && !cell2.IsWalkable)
                    {
                        forceNeighbour = grid.GetGridCell(currentNode.X + direction.x * (index + 1), currentNode.Y - 1);
                        if (forceNeighbour != null && forceNeighbour.IsWalkable)
                            return cell;
                    }
                    index++;

                }
            }
            else if (Mathf.Abs(direction.y) > 0)
            {
                //NOTE: Vertical Scan
                firstScanNode = grid.GetGridCell(currentNode.X, currentNode.Y + direction.y);
                while (scanning)
                {
                    NodeCell cell = grid.GetGridCell(currentNode.X, currentNode.Y + direction.y * index);
                    //NOTE: Check first node
                    if (cell == null || !cell.IsWalkable) break;
                    //if (cell.IsScanned) break;
                    //cell.IsScanned = true;
                    if (cell == endNode)
                        return endNode;

                    NodeCell cell1 = grid.GetGridCell(currentNode.X + 1, currentNode.Y + direction.y * index);
                    NodeCell cell2 = grid.GetGridCell(currentNode.X - 1, currentNode.Y + direction.y * index);

                    if (cell1 != null && !cell1.IsWalkable)
                    {
                        forceNeighbour = grid.GetGridCell(currentNode.X + 1, currentNode.Y + direction.y * (index + 1));
                        if (forceNeighbour != null && forceNeighbour.IsWalkable)
                            return cell;
                    }
                    if (cell2 != null && !cell2.IsWalkable)
                    {
                        forceNeighbour = grid.GetGridCell(currentNode.X - 1, currentNode.Y + direction.y * (index + 1));
                        if (forceNeighbour != null && forceNeighbour.IsWalkable)
                            return cell;
                    }
                    index++;
                }
            }
            return null;
        }
    }
}