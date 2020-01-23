using System;
using System.Collections.Generic;
using System.Text;

namespace Ancient_Wars_v1._0
{
    //This class discribes all forms of movement through space on a fundamental level
    //Functions for means of both movement pathfinding, as well as target aiming
    public class SpaceMovement
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
        
        /// <summary>
        /// ==============
        /// Properties
        /// ==============
        /// </summary>
        
        //Amount of movement for a row; -1 for up, 1 for down 
        private int mRowMovementInt;

        public int XMovement
        {
            get { return mRowMovementInt; }
            set { mRowMovementInt = value; }
        }

        //Amount of movement for a col; -1 for left, 1 for right
        private int mColMovementInt;

        public int YMovement
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
            int newRowMove = thisArg.XMovement + moveArg.XMovement;
            int newColMove = thisArg.YMovement + moveArg.YMovement;
            int newCost = thisArg.MovementCost + moveArg.MovementCost;
            SpaceMovement lMove = new SpaceMovement(newRowMove, newColMove, newCost);
            return lMove;
        }

    }
}
