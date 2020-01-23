using System;
using System.Collections.Generic;
using System.Text;

namespace Ancient_Wars_v1._0
{
    //Faciliates Representation of the board in a console UI format
    class BoardView
    {
        private Board mBoardVar;

        public Board Board
        {
            get { return mBoardVar; }
            set { mBoardVar = value; }
        }

        public void PrintBoardConsole()
        {
            Console.WriteLine(GetBoardString());
        }

        public string GetBoardString()
        {
            StringBuilder boardBuilder = new StringBuilder();
            StringBuilder rowBuilder = new StringBuilder();
            List<String> rowList = new List<String>();

            for (int iRow = mBoardVar.NodeGrid.YBounds[1] - 1; iRow >= mBoardVar.NodeGrid.YBounds[0]; iRow--)
            {                
                for (int iCol = mBoardVar.NodeGrid.XBounds[0] ; iCol <= mBoardVar.NodeGrid.XBounds[1] - 1; iCol++)
                {
                    SpaceCoordinate lCoord = new SpaceCoordinate(iCol, iRow);
                    var testVal = mBoardVar.BoardSpaces.Find(x => x.Node.Coordinates.Equals(lCoord));
                    rowBuilder.Append(mBoardVar.BoardSpaces.Find(x => x.Node.Coordinates.Equals(lCoord)).Icon);
                }
                rowList.Add(rowBuilder.ToString());
                rowBuilder.Clear();
            }
            int yAxis = mBoardVar.NodeGrid.YBounds[1] - 1;
            foreach (string iString in rowList)
            {
                boardBuilder.Append(yAxis);
                boardBuilder.Append(iString);
                if (yAxis == 0)
                {
                    boardBuilder.Append("\r\n");
                    boardBuilder.Append(" ");
                    for (int xAxis = mBoardVar.NodeGrid.XBounds[0]; xAxis < mBoardVar.NodeGrid.XBounds[1]; xAxis++)
                    {
                        boardBuilder.Append(xAxis.ToString());
                    }
                }
                boardBuilder.Append("\r\n");
                yAxis--;
            }
            return boardBuilder.ToString();
        }
    }
}

