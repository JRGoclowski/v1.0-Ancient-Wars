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

        public List<BoardPiece> ReturnPossibleTargets(Unit attackingUnitArg)
        {
            List<BoardPiece> targetPieces = new List<BoardPiece>();
            Board.BoardSpace startLocation = mBoardOfPlay.GetBoardSpaceOfToken(attackingUnitArg);
            Board testB = mBoardOfPlay;
            NodeGrid testG = testB.NodeGrid;
            List<SpaceMovement> allDir = testG.ALL_DIRECTIONS;
            foreach(SpaceMovement iDir in allDir)
            {
                Board.BoardSpace spaceWalker = startLocation;
                for (int i = 0; i < attackingUnitArg.AttackRange + 1; i++)
                {
                    spaceWalker = mBoardOfPlay.SpaceAtMove(spaceWalker.GetCoords(), iDir);
                    BoardPiece lPiece = spaceWalker.PieceAt;
                    bool test1 = lPiece != null;
                    if (test1)
                    {
                        bool test2 = lPiece.Token.TokenID.IDChar != attackingUnitArg.TokenID.IDChar;
                    }
                    

                    if (lPiece != null && lPiece.Token.TokenID.IDChar != attackingUnitArg.TokenID.IDChar)
                    {
                        if (lPiece.Targetable)
                        {
                            targetPieces.Add(spaceWalker.PieceAt);
                        }
                        
                    }
                }
            }
            return targetPieces;
        }

        //Control game to add the unit to the board
        public void AddUnit()
        {
            //Prompt user to select the unit to be added to the board
            Console.WriteLine("Choose Unit");

            bool unitSelected = false;
            int unitInt = 0;

            //loop to select unit to be added
            while (!unitSelected)
            {
                Console.WriteLine(GetUnitList());
                try
                {
                    unitInt = Convert.ToInt32(Console.ReadLine());
                    if (unitInt < 3 && unitInt >= 0)
                    {
                        unitSelected = true;
                    }

                }
                catch (FormatException e)
                {
                    Console.WriteLine("Not an integer, Please enter an integer.");
                }
            }

            //Prompt user to choose location
            Console.WriteLine("Choose Space");

            bool spaceSelected = false;
            string userCoords = "";
            SpaceCoordinate lSpaceCoordinate = new SpaceCoordinate();

            //loop to select board space
            while (!spaceSelected)
            {
                Console.Write("Enter Coordinates [X, Y]: ");
                userCoords = Console.ReadLine();
                lSpaceCoordinate = SpaceCoordinate.ParseCoordinate(userCoords);
                if (lSpaceCoordinate != null)
                {
                    if (ConfirmValidPlacement(mBoardOfPlay, lSpaceCoordinate))
                    {
                        spaceSelected = true;
                    }
                    else
                    {
                        Console.WriteLine("That coordinate is not valid for placement");
                    }
                }
                else
                {
                    Console.WriteLine("Input not a valid Coordinate");
                }
            }

            //use game controller to add a unit to the game board
            //using a basic unit and a parsed coordinate
            PlaceUnit(Unit.GetBasicUnit(unitInt), lSpaceCoordinate);
        }

        //Functions to respond to when a user wishes to select a unit
        public void SelectToken()
        {
            //Select the unit desired to be selected an indexed list
            Console.WriteLine("Select index of desired Token's space");
            int userIndexInt = -1;
            bool selectedUnit = false;
            List<SpaceCoordinate> occSpaces = mBoardOfPlay.GetOccupiedCoords();

            //Loop to select unit
            while (!selectedUnit)
            {
                //Display list of units
                Console.WriteLine("Occupied Spaces: ");
                int i = 0;
                foreach (SpaceCoordinate iCoord in occSpaces)
                {
                    Token lToken = mBoardOfPlay.GetBoardPieceAt(iCoord).Token;
                    Console.WriteLine(
                        i.ToString() + " - (" + iCoord.XCoordinate + ", " + iCoord.YCoordinate + ") : "
                        + lToken.Name + " ID: " + lToken.TokenID.IDString
                        );
                    i++;
                }

                //Get user selection
                try
                {
                    userIndexInt = Convert.ToInt32(Console.ReadLine());
                    bool test1 = userIndexInt > occSpaces.Count;
                    bool test2 = userIndexInt < 0;
                    if (userIndexInt > occSpaces.Count || userIndexInt < 0)
                    {
                        Console.WriteLine("Not within bounds");
                    }
                    else
                    {
                        selectedUnit = true;
                    }
                }
                catch (FormatException e)
                {
                    Console.WriteLine("Not an integer, Please enter an integer.");
                }
            }

            //Obtain token selected from game controller based on user selection
            Token chosenToken = mBoardOfPlay.GetBoardPieceAt(occSpaces[userIndexInt]).Token;

            //Trigger whatever action cylce the token takes
            TokenSelected(chosenToken);
        }

        //Respond to token selection
        public void TokenSelected(Token tokenArg)
        {
            Console.WriteLine("Select unit action: ");
            int userIndexInt = -1;
            bool actionSelected = false;
            String[] lActionList = ActionList();
            while (!actionSelected)
            {
                foreach (string iString in lActionList)
                {
                    Console.WriteLine(iString);
                }
                try
                {
                    userIndexInt = Convert.ToInt32(Console.ReadLine());
                    if (userIndexInt < 0 || userIndexInt > lActionList.Length)
                    {
                        Console.WriteLine("Not within bounds");
                    }
                    else
                    {
                        actionSelected = true;
                    }
                }
                catch (FormatException e)
                {
                    Console.WriteLine("Not an integer, Please enter an integer.");
                }
            }
            TakeAction(userIndexInt, tokenArg);
        }

        public string[] ActionList()
        {
            string[] lList =
            {
                "0 - List Unit Data;",
                "1 - Show Possible Unit Moves; N/I",
                "2 - Show Possible Unit Targets;",
                "3 - Move Unit; N/I",
                "4 - Attack Another Unit; N/I"
            };
            return lList;

        }
        public void TakeAction(int actionSelected, Token tokenArg)
        {
            switch (actionSelected)
            {
                case 0:
                    if (tokenArg != null)
                    {
                        Console.WriteLine(((Unit)tokenArg).GetUnitInfo());
                    }
                    break;
                case 1:
                    break;
                case 2:
                    if (tokenArg != null)
                    {
                        List<BoardPiece> lPieces = ReturnPossibleTargets((Unit)tokenArg);
                        foreach (BoardPiece iPiece in lPieces)
                        {
                            Console.WriteLine(iPiece.Token.Name + " - ID:" + iPiece.Token.TokenID.IDString + " @ " + mBoardOfPlay.GetBoardSpaceOfToken(iPiece.Token).Node.Coordinates.ToString());                        }
                    }
                    break;
                case 3:
                    break;
                case 4:
                    if (tokenArg != null)
                    {
                        LaunchAttack((Unit)tokenArg);
                    }
                    break;
                default:
                        break;
            }
        }

        public void LaunchAttack(Unit unitArg)
        {
            List<BoardPiece> lPieces = ReturnPossibleTargets(unitArg);
            
            
            Console.WriteLine("Select Unit to attack");
            int userIndexInt = -1;
            int i = 0;
            bool selectedUnit = false;

            //Loop to select unit
            while (!selectedUnit)
            {
                foreach (BoardPiece iPiece in lPieces)
                {
                    Console.WriteLine(i.ToString() + " - " + iPiece.Token.Name + " - ID:" + iPiece.Token.TokenID.IDString + " @ " + mBoardOfPlay.GetBoardSpaceOfToken(iPiece.Token).Node.Coordinates.ToString());
                }
                //Get user selection
                try
                {
                    userIndexInt = Convert.ToInt32(Console.ReadLine());
                    if (userIndexInt < 0 || userIndexInt > lPieces.Count)
                    {
                        Console.WriteLine("Not within bounds");
                    }
                    else
                    {
                        selectedUnit = true;
                    }
                }
                catch (FormatException e)
                {
                    Console.WriteLine("Not an integer, Please enter an integer.");
                }
            }
            Unit targetUnit = (Unit)lPieces[userIndexInt].Token;
            unitArg.AttackUnit(targetUnit);
            if (!targetUnit.Alive)
            {
                mBoardOfPlay.GetBoardSpaceOfToken(targetUnit).EmptyPiece();
            }
        }
    }

}
