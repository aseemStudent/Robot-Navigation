using System;
using System.Collections.Generic;
using System.Linq;

namespace RobotNavigation
{
    public class ID_DFS: SearchMethod
    {
        public int levelSearch = 0;

        public ID_DFS(GridFile gridList) : base(gridList)
        {
            while (!breakloop)
            {
                curretNode = InitializePosition();
                dfsVisited.Push(curretNode);

                while (!(dfsVisited.Count == 0) && !breakloop)
                {
                    curretNode = dfsVisited.Peek();
                    if (PossibleToMoveUp(curretNode) && GetPath(curretNode).Length <= levelSearch - 1)
                    {
                        MoveUp(curretNode);
                    }
                    else if (PossibleToMoveLeft(curretNode) && GetPath(curretNode).Length <= levelSearch - 1)
                    {
                        Moveleft(curretNode);
                    }
                    else if (PossibleToMoveDown(curretNode) && GetPath(curretNode).Length <= levelSearch - 1)
                    {
                        MoveDown(curretNode);
                    }
                    else if (PossibleToMoveRight(curretNode) && GetPath(curretNode).Length <= levelSearch - 1)
                    {
                        MoveRight(curretNode);
                    }
                    else
                    {
                        RemovePathToGrid(curretNode);
                        dfsVisited.Pop();
                    }

                    foreach(Node n in dfsVisited)
                    {
                        if (n.blockType == Blocks.Green)
                        {
                            breakloop = true;
                            Console.WriteLine("level {0}", levelSearch);
                            Console.WriteLine("ID_DFS {0}", listOfVisited.Count);
                            ReturnPath();
                            break;
                        }
                    }
                    draw.DrawGrid(gridList);
                    Console.WriteLine("\n-----------------\n");
                }
                listOfVisited.Clear();
                dfsVisited.Clear();
                levelSearch++;
            }

        }
    }
}
