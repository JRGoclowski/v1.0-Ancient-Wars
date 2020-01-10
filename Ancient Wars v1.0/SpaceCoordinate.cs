using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Ancient_Wars_v1._0
{
    public class SpaceCoordinate
    {

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

        public static SpaceCoordinate ParseCoordinate(string parseArg)
        {
            int lXCoord, lYCoord;
            Regex digitRegex = new Regex("[^0-9]");
            string[] inputCoords = parseArg.Split(",");

            string firstCoord = digitRegex.Replace(inputCoords[0], "");
            lXCoord = Convert.ToInt32(firstCoord);

            string secondCoord = digitRegex.Replace(inputCoords[1], "");
            lYCoord = Convert.ToInt32(secondCoord);
            try
            {
                SpaceCoordinate returnVal = new SpaceCoordinate(lXCoord, lYCoord);
                return returnVal;
            }
            catch
            {
                return null;
            }
        }

        public override bool Equals(object coordArg)
        {
            SpaceCoordinate spaceCoord = (SpaceCoordinate)coordArg;
            if (spaceCoord.XCoordinate == this.XCoordinate)
            {
                if (spaceCoord.YCoordinate == this.YCoordinate)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
