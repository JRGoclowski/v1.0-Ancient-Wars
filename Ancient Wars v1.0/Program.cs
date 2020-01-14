using System;
using System.Collections.Generic;

namespace Ancient_Wars_v1._0
{

    //Runs all code to run a game
    class Program
    {

        static void Main(string[] args)
        {
            //Instatiate Controller, and Boardview to fascilitate gameplay
            Game_Controller main_Controller = new Game_Controller();
            BoardView main_BoardView = new BoardView();
            main_BoardView.Board = main_Controller.GetBoard();

            
            Console.WriteLine(".");
            //Begin the actual game
            
            //set loop bolean true
            bool gameContinue = true;
            //Place two starting units
            SpaceCoordinate coordOne = new SpaceCoordinate(4, 2);
            SpaceCoordinate coordTwo = new SpaceCoordinate(5, 3);
            main_Controller.PlaceUnit(Unit.GetBasicUnit(1), coordOne);
            main_Controller.PlaceUnit(Unit.GetBasicUnit(0), coordTwo);
            
            //Actual gameplay loop
            while (gameContinue)
            {
                //Display the console after each cycle
                main_BoardView.PrintBoardConsole();
                //Query user
                Console.WriteLine("What would you like to do?");

                //Loop Control and user input  declaration
                bool optionSelected = false;
                int userInt = 0;

                //Action selection loop
                while (!optionSelected)
                {
                    //Game controller offers selection options
                    Console.WriteLine(main_Controller.GetOptionsList());
                    try
                    {
                        userInt = Convert.ToInt32(Console.ReadLine());
                        if (userInt < main_Controller.GetOptionsList().Length)
                        {
                            optionSelected = true;
                        }

                    }
                    catch (FormatException e)
                    {
                        Console.WriteLine("Not an integer, Please enter an integer.");
                    }
                }

                switch (userInt)
                {
                    case 0:
                        //User wants to put a new unit onto the board
                        AddUnit(main_Controller);
                        break;

                    case 1:
                        //User wants to do an action of a particular unit
                        SelectToken(main_Controller);
                        break;

                    default:
                        Console.WriteLine("default");
                        break;
                }
            }
        }

        //Control game to add the unit to the board
        static private void AddUnit(Game_Controller contArg)
        {
            //Prompt user to select the unit to be added to the board
            Console.WriteLine("Choose Unit");

            bool unitSelected = false;
            int unitInt = 0;

            //loop to select unit to be added
            while (!unitSelected)
            {
                Console.WriteLine(contArg.GetUnitList());
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
                    if (contArg.ConfirmValidPlacement(contArg.GetBoard(),lSpaceCoordinate))
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
            contArg.PlaceUnit(Unit.GetBasicUnit(unitInt), lSpaceCoordinate);
        }

        //Functions to respond to when a user wishes to select a unit
        static private void SelectToken(Game_Controller contArg)
        {
            //Select the unit desired to be selected an indexed list
            Console.WriteLine("Select index of desired Token's space");
            int userIndexInt = -1;
            bool selectedUnit = false;
            List<SpaceCoordinate> occSpaces = contArg.GetBoard().GetOccupiedCoords();

            //Loop to select unit
            while (!selectedUnit)
            {
                //Display list of units
                Console.WriteLine("Occupied Spaces: ");
                int i = 0;
                foreach (SpaceCoordinate iCoord in occSpaces)
                {
                    Token lToken = contArg.GetBoard().GetBoardPieceAt(iCoord).Token;
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
            Token chosenToken = contArg.GetBoard().GetBoardPieceAt(occSpaces[userIndexInt]).Token;

            //Trigger whatever action cylce the token takes
            TokenSelected(chosenToken);
        }

        //Respond to token selection
        static private void TokenSelected(Token tokenArg)
        {
            foreach (string iString in tokenArg.ActionList())
            {
                Console.WriteLine(iString);
            }
        }
    }
}