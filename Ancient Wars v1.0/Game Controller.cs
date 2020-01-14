using System;
using System.Collections.Generic;
using System.Text;

namespace Ancient_Wars_v1._0
{
    //Controller Class to perform all actions on the model of the game
    class Game_Controller
    {
        //The board being played on
        private Board mBoardOfPlay;

        //Tracks round
        private int mRoundCount;

        //Indicates whichever player is acting
        private Player mActivePlayer;
        
        //All players in game in the order they act
        private LinkedList<Player> mPlayerOrder = new LinkedList<Player>();


        private string[] mUnitNames = {"Mage", "Soldier", "Ranger"};

        //Sets up test game 
        public Game_Controller()
        {
            mBoardOfPlay = new Board(0,10, 0, 6);
            mRoundCount = 0;
            for (int i = 0; i < 2; i++)
            {
                mPlayerOrder.AddLast(new Player(i.ToString()));
            }
            mActivePlayer = mPlayerOrder.First.Value;
        }

        //Returns the board
        public Board GetBoard()
        {
            return mBoardOfPlay;
        }
        
        //Action options to act on the game
        public string GetOptionsList()
        {
            StringBuilder optionsBuilder = new StringBuilder();
            optionsBuilder.Append(" 0 - Add Unit" + "\r\n");
            optionsBuilder.Append(" 1 - Select Token" + "\r\n");
            optionsBuilder.Append(" 2 - Reset Board" + "\r\n");
            return optionsBuilder.ToString();
        }
       
        //Return possible units to create
        public string GetUnitList()
        {
            StringBuilder optionsBuilder = new StringBuilder();
            int i = 0;
            foreach (Unit iUnit in Unit.All_BASIC_UNITS)
            {
                optionsBuilder.Append(i + " - " + mUnitNames[i] + "\r\n");
                i++;
            }            
            return optionsBuilder.ToString();
        }
        
        //Confirms that the given coordinate is open for placement of a boardpiece
        public bool ConfirmValidPlacement(Board boardArg, SpaceCoordinate coordArg)
        {
            if (mBoardOfPlay.GetOccupiedCoords().Contains(coordArg))
            {
                return false;
            }
            if (!mBoardOfPlay.SpaceWalkable(coordArg))
            {
                return false;
            }
            if (!Board.withinBounds(boardArg, coordArg))
            {
                return false;
            }
            return true;
        }

        //Places a unit at a coordinate
        public void PlaceUnit(Unit unitArg, SpaceCoordinate coordArg)
        {
            BoardPiece placedPiece = new BoardPiece(unitArg);
            mBoardOfPlay.SetBoardPieceAt(placedPiece, coordArg);
        }

        
    }
}
