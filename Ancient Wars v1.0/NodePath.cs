﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Ancient_Wars_v1._0
{
    class NodePath
    {
        public NodePath(Pathfinder finderArg)
        {
            mPathQueue = finderArg.
        }
        private Queue<BoardNode> mPathQueue;

        public Queue<BoardNode> Path
        {
            get { return mPathQueue; }
            set { mPathQueue = value; }
        }

        private SpaceCoordinate mEndCoord;

        public SpaceCoordinate EndCoord
        {
            get { return mEndCoord; }
            set { mEndCoord = value; }
        }

        private SpaceCoordinate mStartCoord;

        public SpaceCoordinate StartCoord
        {
            get { return mStartCoord; }
            set { mStartCoord = value; }
        }



    }
}
