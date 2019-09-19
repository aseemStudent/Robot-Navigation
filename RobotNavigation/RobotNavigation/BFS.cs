using System;
namespace RobotNavigation
{
    public class BFS: SearchMethod
    {
        public BFS(GridFile gridList) : base(gridList)
        {
            bfsVisited.Enqueue(InitializePosition());

            while(!breakloop)
            {
                curretNode = bfsVisited.Dequeue();

                if (PossibleToMoveUp(curretNode))
                {
                    MoveUp(curretNode);
                }
                if (PossibleToMoveLeft(curretNode))
                {
                    Moveleft(curretNode);
                }
                if (PossibleToMoveDown(curretNode))
                {
                    MoveDown(curretNode);
                }
                if (PossibleToMoveRight(curretNode))
                {
                    MoveRight(curretNode);
                }
                //RemovePathToGrid(curretNode);
                draw.DrawGrid(gridList);
                Console.WriteLine("---------------");

                foreach (Node n in bfsVisited)
                {
                    if (n.blockType == Blocks.Green)
                    {
                        breakloop = true;
                    }
                }
            }
            
            Console.WriteLine("BFS {0}", listOfVisited.Count - 1);
            ReturnPath();
        }
    }
}
