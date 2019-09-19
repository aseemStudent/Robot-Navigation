using System;
using System.Collections.Generic;
using System.Linq;

namespace RobotNavigation
{
    public class AStar: SearchMethod
    {
        public AStar(GridFile gridList) : base(gridList)
        {
            targetNode = FindGreenNode();
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

                    if (n.heuristicCost == 0)
                    {
                        curretNode = n;
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

                draw.DrawGrid(gridList);
                Console.WriteLine("---------------");
            }
            Console.WriteLine("A Star {0}", listOfVisited.Count);
            ReturnPath();
        }
    }
}
