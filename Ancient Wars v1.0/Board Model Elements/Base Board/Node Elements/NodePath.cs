using System;
using System.Collections.Generic;
using System.Text;

//This object serves as a collection of nodes to represent a found path
//Not really that implemented yet
namespace Ancient_Wars_v1._0
{
    class NodePath
    {
        public NodePath()
        { 
        
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
