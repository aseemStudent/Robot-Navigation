using System;
using System.IO;
using System.Collections.Generic;

namespace RobotNavigation
{
    public class GridList
    {
        private int _rowNum = 0;
        private int _colNum = 0;
        private Blocks _block;


        public GridList(int RowNum, int ColNum, Blocks Block)
        {
            _rowNum = RowNum;
            _colNum = ColNum;
            _block = Block;
        }

        public int RowNumber
        {
            get
            {
                return _rowNum;
            }
            set
            {
                _rowNum = value;
            }
        }

        public int ColNumber
        {
            get
            {
                return _colNum;
            }
            set
            {
                _colNum = value;
            }
        }

        public Blocks BlockType
        {
            get
            {
                return _block;
            }
            set
            {
                _block = value;
            }
        }

        public GridList gl
        {
            get
            {
                return this;
            }
        }
    }
}
