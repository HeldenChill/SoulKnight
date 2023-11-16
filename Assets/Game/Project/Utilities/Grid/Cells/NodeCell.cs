using UnityEngine;
namespace Utilities.AI
{
    public class NodeCell : GridCell<int>
    {
        public int GCost;
        public int HCost;
        public int FCost => value;
        public bool IsWalkable = true;

        public NodeCell Parent;
        public Vector2Int FieldVector;

        public bool IsScanned = false;
        public override string ToString()
        {
            return $"{x},{y}";
        }

        public void CalculateFCost()
        {
            value = GCost + HCost;
        }

        public void ResetFCost()
        {
            value = 0;
        }
    }
}