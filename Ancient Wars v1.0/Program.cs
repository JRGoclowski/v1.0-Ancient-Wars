using System;
using System.Collections.Generic;

namespace Ancient_Wars_v1._0
{

    class Program
    {

        static void Main(string[] args)
        {
            Game_Controller main_Controller = new Game_Controller();
            Console.WriteLine(".");
            BoardView main_BoardView = new BoardView();
            Console.WriteLine(".");
            main_BoardView.Board = main_Controller.GetBoard();
            bool gameContinue = true;
            Console.WriteLine(".");
            main_Controller.PlaceUnit(Unit.GetBasicUnit(1), 2, 4);
            main_Controller.PlaceUnit(Unit.GetBasicUnit(0), 3, 5);
            Console.WriteLine(".");




            while (gameContinue)
            {
                main_BoardView.PrintBoardConsole();
                Console.WriteLine("What would you like to do?");

                bool optionSelected = false;
                int userInt = 0;

                while (!optionSelected)
                {
                    Console.WriteLine(main_Controller.GetOptionsList());
                    try
                    {
                        userInt = Convert.ToInt32(Console.ReadLine());
                        if (userInt < 2)
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
                        AddUnit(main_Controller);
                        break;

                    case 1:
                        SelectUnit(main_Controller);
                        break;

                    default:
                        Console.WriteLine("default");
                        break;
                }
            }
        }

        static private void AddUnit(Game_Controller contArg)
        {

            Console.WriteLine("Choose Unit");

            bool unitSelected = false;
            int unitInt = 0;

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

            Console.WriteLine("Choose Space");

            bool spaceSelected = false;
            string userCoords = "";
            SpaceCoordinate lSpaceCoordinate = new SpaceCoordinate();

            while (!spaceSelected)
            {
                Console.Write("Enter Coordinates [Row, Column]: ");
                userCoords = Console.ReadLine();
                lSpaceCoordinate = SpaceCoordinate.ParseCoordinate(userCoords);
                if (lSpaceCoordinate != null)
                {
                    if (contArg.ConfirmValidPlacement(lSpaceCoordinate))
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
            contArg.PlaceUnit(Unit.GetBasicUnit(unitInt), lSpaceCoordinate);
        }

        static private void SelectUnit(Game_Controller contArg)
        {
            Console.WriteLine("Select index of desired unit's space");
            int userIndexInt = -1;
            bool selectedUnit = false;
            while (!selectedUnit)
            {
                Console.WriteLine("Occupied Spaces: ");
                List<SpaceCoordinate> occSpaces = contArg.GetBoard().GetOccupiedCoords();
                int i = 0;

                foreach (SpaceCoordinate iCoord in occSpaces)
                {
                    Console.WriteLine(i.ToString() + " - (" + iCoord.XCoordinate + ", " + iCoord.YCoordinate + ")");
                    i++;
                }
                try
                {
                    userIndexInt = Convert.ToInt32(Console.ReadLine());
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

                Token chosenToken = contArg.GetBoard().GetBoardPieceAt(occSpaces[userIndexInt]).Token;
                Console.WriteLine(chosenToken.Name + " / ID:" + chosenToken.TokenID);
            }
        }
    }
}