using System;
using System.Collections.Generic;

namespace RobotNavigation
{
    public class Draw
    {
        public Draw()
        {
        }

        // draws the grid on console
        public void DrawGrid(GridFile gridList)
        {
            for (int i = 0; i < gridList.TotalRows; i++)
            {
                for (int j = 0; j < gridList.TotalCol; j++)
                {
                    foreach (GridList g in gridList.GetGridLists)
                    {
                        if (g.RowNumber == i && g.ColNumber == j && g.BlockType == Blocks.White)
                        {
                            Console.Write("| ");
                        }
                        else if (g.RowNumber == i && g.ColNumber == j && g.BlockType == Blocks.Green)
                        {
                            Console.Write("|G");
                        }
                        else if (g.RowNumber == i && g.ColNumber == j && g.BlockType == Blocks.Red)
                        {
                            Console.Write("|R");
                        }
                        else if (g.RowNumber == i && g.ColNumber == j && g.BlockType == Blocks.Grey)
                        {
                            Console.Write("|W");
                        }
                        else if (g.RowNumber == i && g.ColNumber == j && g.BlockType == Blocks.Path)
                        {
                            Console.Write("|-");
                        }
                    }
                }
                Console.Write("|\n");
            }
        }
    }
}
