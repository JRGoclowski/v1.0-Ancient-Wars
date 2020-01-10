using System;

namespace Ancient_Wars_v1._0
{

    class Program
    {

        static void Main(string[] args)
        {
            Game_Controller main_Controller = new Game_Controller();
            BoardView main_BoardView = new BoardView();
            main_BoardView.Board = main_Controller.GetBoard();
            bool gameContinue = true;
            main_Controller.PlaceUnit(Unit.GetBasicUnit(1), 3, 4);
            main_Controller.PlaceUnit(Unit.GetBasicUnit(0), 4, 3);




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


            while (!spaceSelected)
            {
                Console.Write("Enter Coordinates [XCoord, YCoord]: ");
                userCoords = Console.ReadLine();
                SpaceCoordinate lCoord = SpaceCoordinate.ParseCoordinate(userCoords);
                if (lCoord != null)
                {
                    if (contArg.GetBoard().withinBounds(lCoord))
                    {
                    spaceSelected = true;
                    }
                }                
                else
                {
                    Console.WriteLine("Entered Coordinates were not valid");
                }
            }

            contArg.PlaceUnit(Unit.GetBasicUnit(unitInt), userCoords);
        }

        static private void SelectUnit(Game_Controller contArg)
        {
            Console.WriteLine("Occupied Spaces: ");
            foreach(SpaceCoordinate iCoord in contArg.GetBoard().GetOccupiedCoords())
            {
                Console.WriteLine("(" + iCoord.XCoordinate + ", " + iCoord.YCoordinate + ")");
            }
        }
    }
}