using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CONSTANTS 
{
    public enum PATH_FINDING
    {
        A_STAR = 0,
        A_STAR_POST_SMOOTHING = 1,
        THETA_STAR = 2,
        LAZY_THETA_STAR = 3,
        JUMP_POINT_SEARCH = 4,
        FLOW_FIELD_PATHFINDING = 5,
    }
    public enum PLANE
    {
        XY = 0,
        XZ = 1,
        YZ = 2,
    }
    public const int GROUND_COST = 1;
    public const int WALL_COST = 255;
}

namespace Game
{
    public enum CELL_TYPE
    {
        NONE = 0,
        GROUND = 1,
        WATER = 2,
    }
    public enum CELL_STATE
    {
        NONE = 0,
        PLAYER = 1,
        TREE_OBSTANCE = 3,
        LOW_ROCK_OBSTANCE = 4,
        HIGH_ROCK_OBSTANCE = 5,
    }

    public enum TREE_TYPE
    {
        HORIZONTAL = 0,
        VERTICAL = 1,
    }

    public enum TREE_STATE
    {
        UP = 0,
        DOWN = 1,
    }

    public class GameCellData
    {
        public CELL_TYPE type = CELL_TYPE.WATER;
        public CELL_STATE state = CELL_STATE.NONE;
    }

    public interface IPoolUnit
    {
        public void OnInit();
        public void OnDespawn();
    }
}
