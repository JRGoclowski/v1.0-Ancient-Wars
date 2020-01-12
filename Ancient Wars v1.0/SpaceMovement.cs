using System;
using System.Collections.Generic;
using System.Text;

namespace Ancient_Wars_v1._0
{
    //This class discribes all forms of movement through space on a fundamental level
    //Functions for means of both movement pathfinding, as well as target aiming
    class SpaceMovement
    {
        //Instantiate a movement to 0 vals for default constructor
        public SpaceMovement()
        {
            mRowMovementInt = 0;
            mColMovementInt = 0;
            mMovementCostInt = 0;
        }

        //Take in row change and column change values, as well as a "cost" which will serve as
        //foundation for calculating movement abilities
        public SpaceMovement(int rowArg, int colArg, int costArg)
        {
            mRowMovementInt = rowArg;
            mColMovementInt = colArg;
            mMovementCostInt = costArg;
        }
        //Cardinal Directions; Set in clockwise rotation
        public static readonly SpaceMovement[] BASIC_DIRECTIONS = 
            {
            DIR_UP,
            DIR_RIGHT,
            DIR_DOWN,
            DIR_LEFT
            };
        //All directions adjacent from a single space; Set in clockwise Rotation
        public static readonly SpaceMovement[] ALL_DIRECTIONS = 
            {
            DIR_UP, 
            DIR_UP_RIGHT,
            DIR_RIGHT,
            DIR_DOWN_RIGHT,
            DIR_DOWN,
            DIR_DOWN_LEFT,
            DIR_LEFT,
            DIR_UP_LEFT
            };

        //Definitions of each direction
        private static readonly SpaceMovement DIR_UP = new SpaceMovement(-1, 0, 1) ;
        private static readonly SpaceMovement DIR_DOWN = new SpaceMovement(1, 0, 1);
        private static readonly SpaceMovement DIR_RIGHT = new SpaceMovement(0, 1 , 1);
        private static readonly SpaceMovement DIR_LEFT = new SpaceMovement(0, -1 ,1);
        private static readonly SpaceMovement DIR_UP_RIGHT = DIR_UP + DIR_RIGHT;
        private static readonly SpaceMovement DIR_UP_LEFT = DIR_UP + DIR_LEFT;
        private static readonly SpaceMovement DIR_DOWN_RIGHT = DIR_DOWN + DIR_RIGHT;
        private static readonly SpaceMovement DIR_DOWN_LEFT = DIR_DOWN + DIR_LEFT;
        

        /// <summary>
        /// ==============
        /// Properties
        /// ==============
        /// </summary>
        
        //Amount of movement for a row; -1 for up, 1 for down 
        private int mRowMovementInt;

        public int RowMovement
        {
            get { return mRowMovementInt; }
            set { mRowMovementInt = value; }
        }

        //Amount of movement for a col; -1 for left, 1 for right
        private int mColMovementInt;

        public int ColMovement
        {
            get { return mColMovementInt; }
            set { mColMovementInt = value; }
        }

        //Amount of cost for a diretion, used to calculate diagonal movement
        private int mMovementCostInt;

        public int MovementCost
        {
            get { return mMovementCostInt; }
            set { mMovementCostInt = value; }
        }

        //Allows for the addition of two directions to calculate movement
        public static SpaceMovement operator +(SpaceMovement thisArg, SpaceMovement moveArg)
        {
            int newRowMove = thisArg.RowMovement + moveArg.RowMovement;
            int newColMove = thisArg.ColMovement + moveArg.ColMovement;
            int newCost = thisArg.MovementCost + moveArg.MovementCost;
            SpaceMovement lMove = new SpaceMovement(newRowMove, newColMove, newCost);
            return lMove;
        }

    }
}
