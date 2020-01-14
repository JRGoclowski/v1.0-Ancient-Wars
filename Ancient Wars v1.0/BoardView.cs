using System;
using System.Collections.Generic;
using System.Text;

namespace Ancient_Wars_v1._0
{
    //Faciliates Representation of the board in a UI format
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

            for (int i = mBoardVar.NodeGrid.YBounds[1] - 1; i >= mBoardVar.NodeGrid.YBounds[0]; i--)
            {
                boardBuilder.Append(i.ToString());
                
                
                for (int j = mBoardVar.NodeGrid.XBounds[1] - 1; j >= mBoardVar.NodeGrid.XBounds[0]; j--)
                {
                    SpaceCoordinate lCoord = new SpaceCoordinate(j, i);
                    boardBuilder.Append(mBoardVar.BoardSpaces.Find( x => x.Node.Coordinates.Equals(lCoord)).Icon);
                }
                boardBuilder.Append("\r\n");
                if (i == mBoardVar.NodeGrid.YBounds[0] + 1)
                {
                    boardBuilder.Append(" ");
                    for (int k = mBoardVar.NodeGrid.XBounds[0]; k < mBoardVar.NodeGrid.XBounds[1]; k++) 
                    {
                        boardBuilder.Append(k.ToString());
                    }
                }
            }
            return boardBuilder.ToString();
        }
    }
}

/*
 * for (int i = 0; i < mBoardVar.Dimensions[0] + 1; i++)
            {
                for (int j = 0; j < mBoardVar.Dimensions[1] + 1; j++)
                {
                    if (j == 0)
                    {
                        if (i == 0)
                        {
                            boardBuilder.Append(" ");
                        }
                        else
                        {
                            boardBuilder.Append(i - 1);
                        }                        
                    }
                    else
                    {
                        if (i == 0)
                        {
                            boardBuilder.Append(j - 1);
                        }
                        else
                        {
                            boardBuilder.Append(mBoardVar.GetSpaceIcon((i - 1),(j - 1)));
                        }
                    }                    
                }
                boardBuilder.Append("\r\n");
            }
            return boardBuilder.ToString();
 * 
 * 
 */
