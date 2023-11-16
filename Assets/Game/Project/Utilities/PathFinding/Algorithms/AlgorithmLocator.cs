using System.Collections;
using System.Collections.Generic;
namespace Utilities.AI {
    public class AlgorithmLocator : ServiceLocator<Grid<NodeCell, int>.PathfindingAlgorithm>
    {
        protected override void Awake()
        {
            base.Awake();
            InitService();
        }
        public override void InitService()
        {
            service = new List<Grid<NodeCell, int>.PathfindingAlgorithm>()
            { new AStar(), new AStarPostSmoothing(), new ThetaStar(), new LazyThetaStar(), new JumpPointSearch(), new FlowfieldPathFinding()};
            MapUpdate();
            Dispatcher.Inst.RegisterListenerEvent(EVENT_ID.MAP_UPDATE, MapUpdate);
        }
        public override Grid<NodeCell, int>.PathfindingAlgorithm GetService(int id)
        {
            return service[id];
        }

        protected void MapUpdate()
        {
            foreach(var algorithm in service)
            {
                algorithm.IsGridUpdate = true;
            }
        }
        
    }
}