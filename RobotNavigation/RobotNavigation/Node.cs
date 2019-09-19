using System;
namespace RobotNavigation
{
    public class Node
    {

        public Node parentNode = null;
        public int currentRow, currentCol;
        public Blocks blockType;
        public int heuristicCost = 0;
        public int gOfN = 0;
        public Direction getPathFromParent;
        public bool visited = false;
        public bool expanded = false;

        // only used for BI-directional A star
        public int targetHeuristic = 0; // for BI_Astar 
        public int targetGOfN = 0; // for BI_Astar 
        public bool targetExpanded = false;
        public bool targetVisited = false;

        public Node()
        {
        }
    }
}
