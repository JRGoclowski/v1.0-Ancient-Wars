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

            mRowCoordinateInt = 0;

            mColCoordinateInt = 0;

        }

        public SpaceCoordinate(int rowArg, int colArg)
        {

            mRowCoordinateInt = rowArg;

            mColCoordinateInt = colArg;

        }

        private int mRowCoordinateInt;
        public int RowCoordinate
        {
            get { return mRowCoordinateInt; }
            private set { mRowCoordinateInt = value; }
        }

        private int mColCoordinateInt;
        public int ColCoordinate
        {
            get { return mColCoordinateInt; }
            private set { mColCoordinateInt = value; }
        }

        //Converts a string input into a coordinate combinations; return null on failed parse
        public static SpaceCoordinate ParseCoordinate(string parseArg)
        {
            int lRowCoord, lColCoord;
            //Define regex for digits
            Regex digitRegex = new Regex("[^0-9]");
            //split string at a comma
            string[] inputCoords = parseArg.Split(",");

            //Obtain the integers
            try
            {
                string firstCoord = digitRegex.Replace(inputCoords[0], "");
                lRowCoord = Convert.ToInt32(firstCoord);
            }
            catch
            {
                return null;
            }

            try
            {
                string secondCoord = digitRegex.Replace(inputCoords[1], "");
                lColCoord = Convert.ToInt32(secondCoord);
            }
            catch
            {
                return null;
            }
            try
            {
                //Generate and return the coordinate
                SpaceCoordinate returnVal = new SpaceCoordinate(lRowCoord, lColCoord);
                return returnVal;
            }
            catch
            {
                return null;
            }
        }

        public bool Equals(SpaceCoordinate other)
        {
            return other != null &&
                   RowCoordinate == other.RowCoordinate &&
                   ColCoordinate == other.ColCoordinate;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(RowCoordinate, ColCoordinate);
        }
    }
}
