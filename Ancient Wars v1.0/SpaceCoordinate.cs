using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Ancient_Wars_v1._0
{
    //Represents any area in space
    public class SpaceCoordinate : IEquatable<SpaceCoordinate>
    {
        //Constructors, Default and with coordinates
        public SpaceCoordinate()
        {

            mXCoordinateInt = 0;

            mYCoordinateInt = 0;

        }

        public SpaceCoordinate(int xArg, int yArg)
        {

            mXCoordinateInt = xArg;

            mYCoordinateInt = yArg;

        }

        private int mXCoordinateInt;
        public int XCoordinate
        {
            get { return mXCoordinateInt; }
            private set { mXCoordinateInt = value; }
        }

        private int mYCoordinateInt;
        public int YCoordinate
        {
            get { return mYCoordinateInt; }
            private set { mYCoordinateInt = value; }
        }

        //Converts a string input into a coordinate combinations; return null on failed parse
        public static SpaceCoordinate ParseCoordinate(string parseArg)
        {
            int lXCoord, lYCoord;
            //Define regex for digits
            Regex digitRegex = new Regex("[^0-9]");
            //split string at a comma
            string[] inputCoords = parseArg.Split(",");

            //Obtain the integers
            try
            {
                string firstCoord = digitRegex.Replace(inputCoords[0], "");
                lXCoord = Convert.ToInt32(firstCoord);
            }
            catch
            {
                return null;
            }

            try
            {
                string secondCoord = digitRegex.Replace(inputCoords[1], "");
                lYCoord = Convert.ToInt32(secondCoord);
            }
            catch
            {
                return null;
            }
            try
            {
                //Generate and return the coordinate
                SpaceCoordinate returnVal = new SpaceCoordinate(lXCoord, lYCoord);
                return returnVal;
            }
            catch
            {
                return null;
            }
        }

        public SpaceCoordinate CoordAtMove(SpaceMovement moveArg)
        {
            int newX = this.mXCoordinateInt + moveArg.XMovement;
            int newY = this.mYCoordinateInt + moveArg.YMovement;
            SpaceCoordinate lCoord = new SpaceCoordinate(newX, newY);
            return lCoord;
        }

        public bool Equals(SpaceCoordinate other)
        {
            return other != null &&
                   XCoordinate == other.XCoordinate &&
                   YCoordinate == other.YCoordinate;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(XCoordinate, YCoordinate);
        }

        public override string ToString()
        {
            return "(" + XCoordinate + "," + YCoordinate + ")";
        }
    }
}
