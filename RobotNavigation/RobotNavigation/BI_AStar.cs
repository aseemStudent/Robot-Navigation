using System;
using System.Collections.Generic;
using System.Linq;

namespace RobotNavigation
{
    public class BI_AStar: SearchMethod
    {
        public List<Node> listOfVisitedTarget; // all searches
        public Queue<Node> targetTempNodes = new Queue<Node>();//for informed search
        public Node reverseNode = new Node();
        public Node forwardNode = new Node();

        public BI_AStar(GridFile gridList) : base(gridList)
        {
            listOfVisitedTarget = new List<Node>();
            targetNode = FindGreenNode();
            targetNode.targetHeuristic = GetHeuristicCostForTarget(targetNode.currentCol, targetNode.currentRow);
            listOfVisitedTarget.Add(targetNode);


            curretNode.heuristicCost = GetHeuristicCost(curretNode.currentCol, curretNode.currentRow);

            while (!breakloop)
            {


                // no cost can be greater than 55 (11*5) in this case
                // or more then column * rows in any case
                int fOfN = grid.TotalCol * grid.TotalRows;
                tempNodes.Clear();

                foreach (Node n in listOfVisited)
                {
                    if (n.expanded == false)
                    {
                        if ((n.heuristicCost + n.gOfN) < fOfN)
                        {
                            fOfN = (n.heuristicCost + n.gOfN);
                        }
                    }
                }



                // found a green node. break loop
                //tempNodes selects nodes with smallest F(n) that has not been expanded
                foreach (Node n in listOfVisited)
                {
                    // if paths dont meet this will still find it
                    if (n.heuristicCost == 0)
                    {
                        curretNode = n;
                        ReturnPath();
                        breakloop = true;
                        break;
                    }
                    if (((n.heuristicCost + n.gOfN) == fOfN) && (n.expanded == false))
                    {
                        tempNodes.Enqueue(n);
                    }
                }



                foreach (Node n in tempNodes)
                {
                    //Console.Write("col {0}, row {1} ", n.currentCol, n.currentRow);
                    //Console.WriteLine("h(n) {0}, g(n) {1}, f(n) {2}", n.heuristicCost, n.gOfN, n.heuristicCost + n.gOfN);
                    //Console.ReadLine();
                    if (PossibleToMoveUp(n))
                    {
                        MoveUp(n);
                    }
                    if (PossibleToMoveLeft(n))
                    {
                        Moveleft(n);
                    }
                    if (PossibleToMoveDown(n))
                    {
                        MoveDown(n);
                    }
                    if (PossibleToMoveRight(n))
                    {
                        MoveRight(n);
                    }
                    n.expanded = true;
                }





                /*movement of target
                 */

                targetTempNodes.Clear();
                fOfN = grid.TotalCol * grid.TotalRows;

                foreach (Node n in listOfVisitedTarget)
                {

                    if (n.expanded == false)
                    {
                        if ((n.targetHeuristic + n.targetGOfN) < fOfN)
                        {
                            fOfN = (n.targetHeuristic + n.targetGOfN);

                        }
                    }
                }

  
                foreach (Node n in listOfVisitedTarget)
                {
                    if (((n.targetHeuristic + n.targetGOfN) == fOfN) && (n.targetExpanded == false))
                    {
                        targetTempNodes.Enqueue(n);
                    }
                }

                foreach (Node n in targetTempNodes)
                {

                    if (TargetPossibleToMoveUp(n))
                    {
                        Node newNode = new Node();
                        newNode.currentCol = n.currentCol;
                        newNode.currentRow = n.currentRow - 1;
                        newNode.blockType = BlockValue(newNode.currentCol, newNode.currentRow);
                        newNode.parentNode = n;
                        newNode.targetHeuristic = GetHeuristicCostForTarget(newNode.currentCol, newNode.currentRow);
                        newNode.targetGOfN = GetPath(newNode).Length;// use path as g(n) = number of steps
                        newNode.getPathFromParent = Direction.Up;
                        AddPathToGrid(newNode);
                        listOfVisitedTarget.Add(newNode);
                        newNode.targetVisited = true;
                    }
                    if (TargetPossibleToMoveLeft(n))
                    {
                        Node newNode = new Node();
                        newNode.currentCol = n.currentCol - 1;
                        newNode.currentRow = n.currentRow;
                        newNode.blockType = BlockValue(newNode.currentCol, newNode.currentRow);
                        newNode.parentNode = n;
                        newNode.targetHeuristic = GetHeuristicCostForTarget(newNode.currentCol, newNode.currentRow);// gbfs & A*
                        newNode.targetGOfN = GetPath(newNode).Length;// A*
                        newNode.getPathFromParent = Direction.Left;
                        AddPathToGrid(newNode);
                        listOfVisitedTarget.Add(newNode);
                        newNode.targetVisited = true;
                    }
                    if (TargetPossibleToMoveDown(n))
                    {
                        Node newNode = new Node();
                        newNode.currentCol = n.currentCol;
                        newNode.currentRow = n.currentRow + 1;
                        newNode.blockType = BlockValue(newNode.currentCol, newNode.currentRow);
                        newNode.parentNode = n;
                        newNode.targetHeuristic = GetHeuristicCostForTarget(newNode.currentCol, newNode.currentRow);
                        newNode.targetGOfN = GetPath(newNode).Length;
                        newNode.getPathFromParent = Direction.Down;
                        AddPathToGrid(newNode);
                        listOfVisitedTarget.Add(newNode);
                        newNode.targetVisited = true;
                    }
                    if (TargetPossibleToMoveRight(n))
                    {
                        Node newNode = new Node();
                        newNode.currentCol = n.currentCol + 1;
                        newNode.currentRow = n.currentRow;
                        newNode.blockType = BlockValue(newNode.currentCol, newNode.currentRow);
                        newNode.parentNode = n;
                        newNode.targetHeuristic = GetHeuristicCostForTarget(newNode.currentCol, newNode.currentRow);
                        newNode.targetGOfN = GetPath(newNode).Length;
                        newNode.getPathFromParent = Direction.Right;
                        AddPathToGrid(newNode);
                        listOfVisitedTarget.Add(newNode);
                        newNode.targetVisited = true;
                    }
                    n.targetExpanded = true;
                }

                // finds the first Node visiter from both side
                foreach(Node reverse in listOfVisitedTarget)
                {
                    foreach(Node forward in listOfVisited)
                    {
                        if (reverse.currentCol == forward.currentCol && reverse.currentRow == forward.currentRow)
                        {
                            reverseNode = reverse;
                            forwardNode = forward;
                            breakloop = true;
                            break;
                        }
                    }
                }
                draw.DrawGrid(gridList);
                Console.WriteLine("---------------");
            }

            PathToMeetingPoint(forwardNode, reverseNode, gridList);
        }


        public void PathToMeetingPoint(Node forward, Node reverse, GridFile gridList)
        {
            Console.WriteLine("\nBI {0}\n", listOfVisited.Count);
            Console.WriteLine("Meeting Point is Column {0}, Row {1}", forward.currentCol, forward.currentRow);
            Console.WriteLine("\nPath Forward is: ");
            pathDirection = GetPath(forward);

            for (int i = 0; i < pathDirection.Length; i++)
            {
                Console.Write("{0}; ", pathDirection[i]);
            }

            Console.WriteLine("\n\nPath Backward is: ");
            pathDirection = GetPath(reverse);

            for (int i = 0; i < pathDirection.Length; i++)
            {
                Console.Write("{0}; ", pathDirection[i]);
            }
        }

        // gets manhattan distance of nods
        public int GetHeuristicCostForTarget(int colNum, int rowNum)
        {
            int result = (Math.Abs(colNum - curretNode.currentCol) + Math.Abs(rowNum - curretNode.currentRow));
            return result;
        }


        public bool TargetPossibleToMoveUp(Node oldNode)
        {
            if (NotInBoundry(oldNode.currentCol, oldNode.currentRow - 1)
                || IsWalled(oldNode.currentCol, oldNode.currentRow - 1)
                || (TargetIsVisited(oldNode.currentCol, oldNode.currentRow - 1)))
            {
                return false;
            }
            return true;
        }

        public bool TargetPossibleToMoveLeft(Node oldNode)
        {
            if (NotInBoundry(oldNode.currentCol - 1, oldNode.currentRow)
                || IsWalled(oldNode.currentCol - 1, oldNode.currentRow)
                || (TargetIsVisited(oldNode.currentCol - 1, oldNode.currentRow)))
            {
                return false;
            }
            return true;
        }

        public bool TargetPossibleToMoveDown(Node oldNode)
        {
            if (NotInBoundry(oldNode.currentCol, oldNode.currentRow + 1)
                || IsWalled(oldNode.currentCol, oldNode.currentRow + 1)
                || (TargetIsVisited(oldNode.currentCol, oldNode.currentRow + 1)))
            {
                return false;
            }
            return true;
        }

        public bool TargetPossibleToMoveRight(Node oldNode)
        {
            if (NotInBoundry(oldNode.currentCol + 1, oldNode.currentRow)
                || IsWalled(oldNode.currentCol + 1, oldNode.currentRow)
                || (TargetIsVisited(oldNode.currentCol + 1, oldNode.currentRow)))
            {
                return false;
            }
            return true;
        }

        // Already visited by target
        public bool TargetIsVisited(int colN, int rowN)
        {
            foreach (Node n in listOfVisitedTarget)
            {
                if (n.currentCol == colN && n.currentRow == rowN)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
