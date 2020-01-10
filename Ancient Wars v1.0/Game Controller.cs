using System;
using System.Collections.Generic;
using System.Text;

namespace Ancient_Wars_v1._0
{
    class Game_Controller
    {
        private Board mBoardOfPlay;
        private int mRoundCount;
        private Player mActivePlayer;
        private LinkedList<Player> mPlayerOrder = new LinkedList<Player>();
        private string[] mUnitNames = {"Mage", "Soldier", "Ranger"};


        public Game_Controller()
        {
            mBoardOfPlay = new Board(6,10);
            mRoundCount = 0;
            for (int i = 0; i < 2; i++)
            {
                mPlayerOrder.AddLast(new Player(i.ToString()));
            }
            mActivePlayer = mPlayerOrder.First.Value;
        }

        public Board GetBoard()
        {
            return mBoardOfPlay;
        }
        
        public string GetOptionsList()
        {
            StringBuilder optionsBuilder = new StringBuilder();
            optionsBuilder.Append(" 0 - Add Unit" + "\r\n");
            optionsBuilder.Append(" 1 - Select Unit" + "\r\n");
            optionsBuilder.Append(" 2 - Reset Board" + "\r\n");
            return optionsBuilder.ToString();
        }

        public string GetUnitList()
        {
            StringBuilder optionsBuilder = new StringBuilder();
            optionsBuilder.Append(" 0 - " + mUnitNames[0] + "\r\n");
            optionsBuilder.Append(" 1 - " + mUnitNames[1] + "\r\n");
            optionsBuilder.Append(" 2 - " + mUnitNames[2] + "\r\n");
            return optionsBuilder.ToString();
        }
                
        public string GetUnitNameByIndex(int intArg)
        {
            return mUnitNames[intArg];
        }

        public bool ConfirmValidPlacement(int xArg, int yArg)
        {
            if (mBoardOfPlay.GetOccupiedCoords().Contains({xArg, yArg}))
            {

            }
            return true;
        }
        public void PlaceUnit(Unit unitArg, SpaceCoordinate coordArg)
        {
            BoardPiece placedPiece = new BoardPiece(unitArg);
            mBoardOfPlay.SetBoardPieceAt(placedPiece, coordArg);
        }
    }
}
