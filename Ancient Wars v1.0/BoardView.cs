using System;
using System.Collections.Generic;
using System.Text;

namespace Ancient_Wars_v1._0
{
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

            for (int i = 0; i < mBoardVar.Dimensions[0] + 1; i++)
            {
                for (int j = 0; j < mBoardVar.Dimensions[1] + 1; j++)
                {
                    if (i == 0)
                    {
                        if (j == 0)
                        {
                            boardBuilder.Append(" ");
                        }
                        else
                        {
                            boardBuilder.Append(j-1);
                        }                        
                    }
                    else
                    {
                        if (j == 0)
                        {
                            boardBuilder.Append(i-1);
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
        }
    }
}
