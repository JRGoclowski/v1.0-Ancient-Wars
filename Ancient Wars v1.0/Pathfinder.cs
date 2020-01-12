using System;
using System.Collections.Generic;
using System.Text;

namespace Ancient_Wars_v1._0
{
    class Pathfinder
    {


        /*
         * public int ComputeH(SpaceCoordinate startArg, SpaceCoordinate targetArg)
        {
            return (Math.Abs(targetArg.RowCoordinate - startArg.RowCoordinate) + Math.Abs(targetArg.ColCoordinate - startArg.ColCoordinate));
        }
         */


        private BoardNode[] mOpenList;

        public BoardNode[] OpenList
        {
            get { return mOpenList; }
            set { mOpenList = value; }
        }

        private BoardNode[] mClosedList;

        public BoardNode[] ClosedList
        {
            get { return mClosedList; }
            set { mClosedList = value; }
        }



        private SpaceCoordinate mStartCoord;

        public SpaceCoordinate Start
        {
            get { return mStartCoord; }
            set { mStartCoord = value; }
        }

        private SpaceCoordinate mEndCoord;

        public SpaceCoordinate End
        {
            get { return mEndCoord; }
            set { mEndCoord = value; }
        }


    }
}
