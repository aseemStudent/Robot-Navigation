using System;
using System.IO;
using System.Collections.Generic;

namespace RobotNavigation
{
    public class GridFile
    {
        private int _totalRow = 0;
        private int _totalCol = 0;
        List<GridList> ListOfGrid = new List<GridList>();

        public GridFile(string textFileName)
        {
            StreamReader textFile = new StreamReader(textFileName);
            string text = textFile.ReadLine();
            string text2;
            string[] value, value2;

            while (text != null)
            {
                // reads the first lines and make row*colums grid
                if (text.StartsWith("[", StringComparison.Ordinal) && text.EndsWith("]", StringComparison.Ordinal))
                {

                    text = CleanEdges(text);  // removes the brakets 
                    value = text.Split(','); // divide values by ',' and puts them in an array

                    _totalRow = Int32.Parse(value[0]);
                    _totalCol = Int32.Parse(value[1]);

                    // initialise all empty blocks white 
                    for (int i = 0; i < _totalRow; i++)
                    {
                        for (int j = 0; j < _totalCol; j++)
                        {
                            ListOfGrid.Add(new GridList(i, j, Blocks.White));
                        }
                    }

                }
                // includes red (robot) block (2) 
                else if (text.StartsWith("(", StringComparison.Ordinal) && !text.Contains("|"))
                {
                    text = CleanEdges(text);// removes the brakets 
                    value = text.Split(',');// divide values by ',' and puts them in an array

                    if (value.Length == 2)
                    {
                        foreach (GridList g in ListOfGrid)
                        {
                            if (g.RowNumber == Int32.Parse(value[1]) && g.ColNumber == Int32.Parse(value[0]))
                            {
                                g.BlockType = Blocks.Red;
                            }
                        }
                    }
                    // includes grey blocks (4)
                    else if (value.Length == 4)
                    {
                        foreach (GridList g in ListOfGrid)
                        {
                            for (int col = Int32.Parse(value[0]); col < (Int32.Parse(value[0]) + Int32.Parse(value[2])); col++)
                            {
                                for (int row = Int32.Parse(value[1]); row < (Int32.Parse(value[1]) + Int32.Parse(value[3])); row++)
                                {
                                    if (g.RowNumber == row && g.ColNumber == col)
                                    {
                                        g.BlockType = Blocks.Grey;
                                    }

                                }
                            }
                        }
                    }

                }
                // green blocks
                else if (text.StartsWith("(", StringComparison.Ordinal) && text.Contains("|"))
                {
                    value = text.Split('|');

                    foreach (string s in value)
                    {
                        text2 = CleanEdges(s);
                        value2 = text2.Split(',');
                        foreach (GridList g in ListOfGrid)
                        {
                            if (g.RowNumber == Int32.Parse(value2[1]) && g.ColNumber == Int32.Parse(value2[0]))
                            {
                                g.BlockType = Blocks.Green;
                            }
                        }

                    }
                }

                text = textFile.ReadLine();
            }
            textFile.Close(); // close file at the end
        }

        // removes bracket
        public string CleanEdges(string text)
        {
            text = text.Remove(text.Length - 1, 1);
            text = text.Remove(0, 1);
            return text;
        }

        //access to ListOfGrid
        public List<GridList> GetGridLists
        {
            get
            {
                return ListOfGrid;
            }
            set
            {
                ListOfGrid = value;
            }
        }

        // access to total number of rows
        public int TotalRows
        {
            get
            {
                return _totalRow;
            }
        }

        // access to total number of colums
        public int TotalCol
        {
            get
            {
                return _totalCol;
            }
        }
    }
}
