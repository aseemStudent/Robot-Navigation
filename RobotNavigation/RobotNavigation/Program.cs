using System;
using System.IO;

namespace RobotNavigation
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            if(args.Length != 2)
            {
                Console.WriteLine("Please make sure command is in format <filename> <method>");
            }

            string textFilePath = args[0];
            GridFile gridList = new GridFile(textFilePath);
            Draw draw = new Draw();
            draw.DrawGrid(gridList);

            Console.WriteLine("Choose a method: \nDFS\nBFS\nGBFS\nAS\nID\nBI");
            string searchMethod = args[1];
            switch (searchMethod.ToUpper())
            {
                case "DFS":
                    Console.WriteLine("Depth First Search");
                    DFS DFS = new DFS(gridList);
                    break;
                case "BFS":
                    Console.WriteLine("Breath First Search");
                    BFS BFS = new BFS(gridList);
                    break;
                case "GBFS":
                    Console.WriteLine("Greedy Best First Search");
                    GBFS GBFS = new GBFS(gridList);
                    break;
                case "AS":
                    Console.WriteLine("A Star");
                    AStar AStar = new AStar(gridList);
                    break;
                case "ID":
                    Console.WriteLine("Iterative Deepening");
                    ID_DFS ID = new ID_DFS(gridList);
                    break;
                case "BI":
                    Console.WriteLine("BI A*");
                    BI_AStar BI = new BI_AStar(gridList);
                    break;
                default:
                    Console.WriteLine("NO method selected");
                    break;

            }
        }
    }
}
