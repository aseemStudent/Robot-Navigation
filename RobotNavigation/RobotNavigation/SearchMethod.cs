using System;
using System.Collections.Generic;
using System.Linq;

namespace RobotNavigation
{
    public abstract class SearchMethod
    {
        public Stack<Node> dfsVisited = new Stack<Node>();// dfs

        public Node curretNode = new Node(); // for uninformed search
        public Node targetNode = new Node(); // for informed search

        public GridFile grid;
        public Draw draw = new Draw();

        public Queue<Node> bfsVisited = new Queue<Node>(); // for bfs
        public Queue<Node> tempNodes = new Queue<Node>();//for informed search

        public List<Node> listOfVisited; // all searches
        public Direction[] pathDirection; // returns path in the end

        public bool breakloop = false; // for breaking while loop

        public SearchMethod(GridFile gridList)
        {
            listOfVisited = new List<Node>();
            grid = gridList;
            curretNode = InitializePosition();
        }



        // initialises the first rod RED
        public Node InitializePosition()
        {
            foreach (GridList g in grid.GetGridLists)
            {
                if (g.BlockType.ToString() == "Red")
                {
                    curretNode.currentCol = g.ColNumber;
                    curretNode.currentRow = g.RowNumber;
                    curretNode.blockType = Blocks.Red;
                }
            }
            listOfVisited.Add(curretNode);
            AddPathToGrid(curretNode);
            return curretNode;
        }



        // should return weather position can move UP
        // eg. inside the boundry, no wall, already visited
        public bool PossibleToMoveUp(Node oldNode)
        {
            if (NotInBoundry(oldNode.currentCol, oldNode.currentRow - 1)
                || IsWalled(oldNode.currentCol, oldNode.currentRow - 1)
                || (IsVisited(oldNode.currentCol, oldNode.currentRow - 1)))
            {
                return false;
            }
            return true;
        }

        // Movement UP
        public void MoveUp(Node n)
        {
            Node newNode = new Node();
            newNode.currentCol = n.currentCol;
            newNode.currentRow = n.currentRow - 1;
            newNode.blockType = BlockValue(newNode.currentCol, newNode.currentRow);
            newNode.parentNode = n;
            newNode.heuristicCost = GetHeuristicCost(newNode.currentCol, newNode.currentRow);
            newNode.gOfN = GetPath(newNode).Length;// use path as g(n) = number of steps
            newNode.getPathFromParent = Direction.Up;
            bfsVisited.Enqueue(newNode);
            AddPathToGrid(newNode);
            listOfVisited.Add(newNode);
            newNode.visited = true;
            dfsVisited.Push(newNode);//dfs
        }


        // should return weather position can move Left
        // eg. inside the boundry, no wall, already visited
        public bool PossibleToMoveLeft(Node oldNode)
        {
            if (NotInBoundry(oldNode.currentCol - 1, oldNode.currentRow)
                || IsWalled(oldNode.currentCol - 1, oldNode.currentRow)
                || (IsVisited(oldNode.currentCol - 1, oldNode.currentRow)))
            {
                return false;
            }
            return true;
        }

        public void Moveleft(Node n)
        {
            Node newNode = new Node();
            newNode.currentCol = n.currentCol - 1;
            newNode.currentRow = n.currentRow;
            newNode.blockType = BlockValue(newNode.currentCol, newNode.currentRow);
            newNode.parentNode = n;
            newNode.heuristicCost = GetHeuristicCost(newNode.currentCol, newNode.currentRow);// gbfs & A*
            newNode.gOfN = GetPath(newNode).Length;// A*
            newNode.getPathFromParent = Direction.Left;
            bfsVisited.Enqueue(newNode);// bfs
            AddPathToGrid(newNode);
            listOfVisited.Add(newNode);
            newNode.visited = true;
            dfsVisited.Push(newNode);//dfs
        }



        // should return weather position can move Down
        // eg. inside the boundry, no wall, already visited
        public bool PossibleToMoveDown(Node oldNode)
        {
            if (NotInBoundry(oldNode.currentCol, oldNode.currentRow + 1)
                || IsWalled(oldNode.currentCol, oldNode.currentRow + 1)
                || (IsVisited(oldNode.currentCol, oldNode.currentRow + 1)))
            {
                return false;
            }
            return true;
        }

        public void MoveDown(Node n)
        {
            Node newNode = new Node();
            newNode.currentCol = n.currentCol;
            newNode.currentRow = n.currentRow + 1;
            newNode.blockType = BlockValue(newNode.currentCol, newNode.currentRow);
            newNode.parentNode = n;
            newNode.heuristicCost = GetHeuristicCost(newNode.currentCol, newNode.currentRow);
            newNode.gOfN = GetPath(newNode).Length;
            newNode.getPathFromParent = Direction.Down;
            bfsVisited.Enqueue(newNode);
            AddPathToGrid(newNode);
            listOfVisited.Add(newNode);
            newNode.visited = true;
            dfsVisited.Push(newNode);//dfs
        }



        // should return weather position can move Right
        // eg. inside the boundry, no wall, already visited
        public bool PossibleToMoveRight(Node oldNode)
        {
            if (NotInBoundry(oldNode.currentCol + 1, oldNode.currentRow)
                || IsWalled(oldNode.currentCol + 1, oldNode.currentRow)
                || (IsVisited(oldNode.currentCol + 1, oldNode.currentRow)))
            {
                return false;
            }
            return true;
        }

        public void MoveRight(Node n)
        {
            Node newNode = new Node();
            newNode.currentCol = n.currentCol + 1;
            newNode.currentRow = n.currentRow;
            newNode.blockType = BlockValue(newNode.currentCol, newNode.currentRow);
            newNode.parentNode = n;
            newNode.heuristicCost = GetHeuristicCost(newNode.currentCol, newNode.currentRow);
            newNode.gOfN = GetPath(newNode).Length;
            newNode.getPathFromParent = Direction.Right;
            bfsVisited.Enqueue(newNode);
            AddPathToGrid(newNode);
            listOfVisited.Add(newNode);
            newNode.visited = true;
            dfsVisited.Push(newNode);//dfs
        }


        // Already visited
        public bool IsVisited(int colN, int rowN)
        {
            foreach (Node n in listOfVisited)
            {
                if (n.currentCol == colN && n.currentRow == rowN)
                {
                    return true;
                }
            }
            return false;
        }



        // checks weather new position is in boundry
        public bool NotInBoundry(int colN, int rowN)
        {
            if (colN > grid.TotalCol - 1 || colN < 0 || rowN < 0 || rowN > grid.TotalRows - 1)
            {
                return true;
            }
            return false;
        }



        //checks for wall
        public bool IsWalled(int colN, int rowN)
        {
            foreach (GridList g in grid.GetGridLists)
            {
                if (colN == g.ColNumber && rowN == g.RowNumber && g.BlockType == Blocks.Grey)
                {
                    return true;
                }
            }
            return false;
        }



        // returs the block type of a position
        public Blocks BlockValue(int colN, int rowN)
        {
            foreach (GridList g in grid.GetGridLists)
            {
                if (colN == g.ColNumber && rowN == g.RowNumber)
                {
                    return g.BlockType;

                }
            }
            Console.WriteLine("Error");
            return Blocks.White;
        }



        // Add Path to Grid
        public void AddPathToGrid(Node n)
        {
            foreach (GridList g in grid.GetGridLists)
            {
                if (g.ColNumber == n.currentCol && g.RowNumber == n.currentRow && !(g.BlockType == Blocks.Green)
                    && !(g.BlockType == Blocks.Red))
                {
                    g.BlockType = Blocks.Path;
                }
            }
        }


        // Remove Path to Grid
        public void RemovePathToGrid(Node n)
        {
            foreach (GridList g in grid.GetGridLists)
            {
                if (g.ColNumber == n.currentCol && g.RowNumber == n.currentRow && !(g.BlockType == Blocks.Green)
                    && !(g.BlockType == Blocks.Red))
                {
                    g.BlockType = Blocks.White;
                }
            }
        }


        // uses Direction[] to print
        public void ReturnPath ()
        {
            // trace back the directions
            foreach (Node n in listOfVisited)
            {
                if (n.blockType == Blocks.Green)
                {

                    Console.WriteLine("\nPath to Green ");
                    pathDirection = GetPath(n);

                    for (int i = 0; i < pathDirection.Length; i++)
                    {
                        Console.Write("{0}; ", pathDirection[i]);
                    }
                    Console.WriteLine("\n");
                }
            }
        }

        // gives direction as in left, right, up or down
        public Direction[] GetPath(Node n)
        {

            Direction[] result;
            if (n.parentNode == null)
            {
                result = new Direction[0];
                return result;
            }
            else
            {
                Direction[] temp = GetPath(n.parentNode);
                result = new Direction[temp.Length + 1];
                for (int i = 0; i < temp.Length; i++)
                {
                    result[i] = temp[i];
                }
                result[temp.Length] = n.getPathFromParent;
                return result;
            }

        }



        /* the methods below are only used for 
         * informed search AStar and GBFS       
         */

        // finds target (the closest green to red)
        public Node FindGreenNode()
        {
            List<Node> allPossibleTargets = new List<Node>();

            // stores all green nods in a list
            foreach (GridList g in grid.GetGridLists)
            {
                Node temp = new Node();

                // you temporarily assign green node heuristic value to reach red
                if (g.BlockType == Blocks.Green)
                {
                    temp.blockType = g.BlockType;
                    temp.currentCol = g.ColNumber;
                    temp.currentRow = g.RowNumber;
                    temp.heuristicCost = Math.Abs(curretNode.currentCol + curretNode.currentRow - (temp.currentCol + temp.currentRow));
                    allPossibleTargets.Add(temp);
                }
            }


            // saves the node with lowest heuristic cost (closest) as targets
            // changes heuristic cost to 0
            foreach (Node n in allPossibleTargets)
            {
                int lowestHeuristic = allPossibleTargets.Min(Node => Node.heuristicCost);
                if (n.heuristicCost == lowestHeuristic)
                {
                    targetNode = n;
                    targetNode.heuristicCost = 0;
                }
            }
            return targetNode;
        }


        // gets manhattan distance of nods
        public int GetHeuristicCost(int colNum, int rowNum)
        {
            int result = (Math.Abs(colNum - targetNode.currentCol) + Math.Abs(rowNum - targetNode.currentRow));
            return result;
        }
    }
}
