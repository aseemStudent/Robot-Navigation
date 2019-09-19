using System;
namespace RobotNavigation
{
    public class GBFS: SearchMethod
    {
        public GBFS(GridFile gridList) : base(gridList)
        {
            targetNode = FindGreenNode();
            curretNode.heuristicCost = GetHeuristicCost(curretNode.currentCol, curretNode.currentRow);

            while(!breakloop)
            {
                // no cost can be greater than 55 (11*5) in this case
                // or more then column * rows in any case
                int minHeuristicCost = grid.TotalCol * grid.TotalRows;
                tempNodes.Clear();

                foreach (Node n in listOfVisited)
                {
                    if (n.expanded == false)
                    {
                        if (n.heuristicCost < minHeuristicCost)
                        {
                            minHeuristicCost = n.heuristicCost;
                        }
                    }
                }

                // found a green node. break loop
                //tempNodes selects nodes with smallest Heuristc cost that has not been expanded
                foreach (Node n in listOfVisited)
                {
                    if (n.heuristicCost == 0)
                    {
                        curretNode = n;
                        breakloop = true;
                        break;
                    }
                    if ((n.heuristicCost == minHeuristicCost) && (n.expanded == false))
                    {
                        tempNodes.Enqueue(n);
                    }
                }


                // 
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
            Console.WriteLine("GBFS {0}", listOfVisited.Count);
            ReturnPath();
        }
    }
}
