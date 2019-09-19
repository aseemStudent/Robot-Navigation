using System;
using System.Collections.Generic;

namespace RobotNavigation
{
    public class DFS: SearchMethod
    {

        public DFS(GridFile gridList):base(gridList)
        {
            dfsVisited.Push(curretNode);

            while (!breakloop)
            {
                curretNode = dfsVisited.Peek();

                if (PossibleToMoveUp(curretNode))
                {
                    MoveUp(curretNode);
                }
                else if (PossibleToMoveLeft(curretNode))
                {
                    Moveleft(curretNode);
                }
                else if (PossibleToMoveDown(curretNode))
                {
                    MoveDown(curretNode);
                }
                else if (PossibleToMoveRight(curretNode))
                {
                    MoveRight(curretNode);
                }
                else
                {
                    RemovePathToGrid(curretNode);
                    dfsVisited.Pop();
                }

                if (curretNode.blockType == Blocks.Green)
                {
                    breakloop = true;
                }
                else
                {
                    draw.DrawGrid(gridList);
                    Console.WriteLine("\n-----------------\n");
                }
            }
            Console.WriteLine("DFS {0}", listOfVisited.Count - 1); //bug
            ReturnPath();
        }
    }
}
