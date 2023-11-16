using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Project
{
    using Utilities.AI;
    public class MovingAgent : MonoBehaviour
    {
        [SerializeField]
        CONSTANTS.PATH_FINDING typeAlgorithm;
        [SerializeField]
        Transform mTransform;
        [SerializeField]
        float speed;

        Map map;
        Grid<NodeCell, int>.PathfindingAlgorithm algorithm;
        Queue<Vector2> steps = new Queue<Vector2>();
        Vector2 destination;
        Vector2 direction = new Vector2();
        bool isMoving = false;
        private void Start()
        {
            map = LevelManager.Inst.Map;
            UpdateAlgorithm();
        }

        public void UpdateAlgorithm()
        {
            switch (typeAlgorithm)
            {
                case CONSTANTS.PATH_FINDING.A_STAR:
                    algorithm = AlgorithmLocator.Inst.GetService(0);
                    break;
                case CONSTANTS.PATH_FINDING.A_STAR_POST_SMOOTHING:
                    algorithm = AlgorithmLocator.Inst.GetService(1);
                    break;
                case CONSTANTS.PATH_FINDING.THETA_STAR:
                    algorithm = AlgorithmLocator.Inst.GetService(2);
                    break;
                case CONSTANTS.PATH_FINDING.LAZY_THETA_STAR:
                    algorithm = AlgorithmLocator.Inst.GetService(3);
                    break;
                case CONSTANTS.PATH_FINDING.JUMP_POINT_SEARCH:
                    algorithm = AlgorithmLocator.Inst.GetService(4);
                    break;
                case CONSTANTS.PATH_FINDING.FLOW_FIELD_PATHFINDING:
                    algorithm = AlgorithmLocator.Inst.GetService(5);
                    break;
            }
        }

        public void SetDestination(Vector3 position)
        {
            (int desX, int desY) = map.MapGrid.GetGridPosition(position);
            (int startX, int startY) = map.MapGrid.GetGridPosition(mTransform.position);
            List<NodeCell> path = algorithm.FindPath(startX, startY, desX, desY, map.MapGrid);
            //map.ShowFCost();
            if (path == null) return;
            for (int i = 0; i < path.Count; i++)
            {
                steps.Enqueue(path[i].WorldPos);
            }

            if (steps.Count == 0) return;
            map.DrawPath(path);
            MoveTo(steps.Dequeue());
        }
        private void MoveTo(Vector2 destination)
        {
            this.destination = destination;
            isMoving = true;
        }
        private void ReachDestination()
        {
            isMoving = false;
            if (steps.Count == 0) return;
            MoveTo(steps.Dequeue());
        }
        private void FixedUpdate()
        {
            if (isMoving)
            {

                direction.Set(destination.x - mTransform.position.x, destination.y - mTransform.position.y);
                if (direction.sqrMagnitude <= 1f)
                {
                    ReachDestination();
                }

                direction.Normalize();
                transform.position += (Vector3)direction * speed * Time.fixedDeltaTime;
            }
        }

    }
}