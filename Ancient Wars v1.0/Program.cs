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
                        main_Controller.AddUnit();
                        break;

                    case 1:
                        //User wants to do an action of a particular unit
                        main_Controller.SelectToken();
                        break;

                    default:
                        Console.WriteLine("default");
                        break;
                }
            }
        }

        
    }
}