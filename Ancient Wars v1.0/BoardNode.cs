using System;
using System.Collections.Generic;
using System.Text;

namespace Ancient_Wars_v1._0
{
    ///https://gigi.nullneuron.net/gigilabs/a-pathfinding-example-in-c/
    class BoardNode
    {
        public BoardNode(SpaceCoordinate coordArg, bool walkArg, bool aimBool)
        {
            mSpaceCoordinate = coordArg;
            isWalkable = walkArg;
            isAimable = aimBool;
            mGCostInt = 0;
            mHCostInt = 0;
            mFCostInt = 0;
        }

        public static List<BoardNode> IdentifyNeighbors(NodeGrid gridArg, SpaceCoordinate coordArg)
        {

            List<BoardNode> lNeighbors = new List<BoardNode>();
            foreach (SpaceMovement iMove in SpaceMovement.BASIC_DIRECTIONS)
            {
                SpaceCoordinate possNeighborCoord = coordArg.CoordAtMove(iMove);
                foreach (BoardNode iNode in gridArg)
                {
                    if (iNode.Equals(possNeighborCoord))
                    {
                        lNeighbors.Add(iNode);
                    }
                }
            }
            return lNeighbors;
        }

        private BoardNode[] mNeigherborList;

        public BoardNode[] Neighbors
        {
            get { return mNeigherborList; }
            set { mNeigherborList = value; }
        }

        private SpaceCoordinate mSpaceCoordinate;

        public SpaceCoordinate Coordinates
        {
            get { return mSpaceCoordinate; }
            private set { mSpaceCoordinate = value; }
        }


        private int mGCostInt;

        public int GCost
        {
            get { return mGCostInt; }
            set { mGCostInt = value; }
        }

        private int mHCostInt;

        public int HCost
        {
            get { return mHCostInt; }
            set { mHCostInt = value; }
        }

        private int mFCostInt;

        public int FCost
        {
            get { return mFCostInt; }
            set { mFCostInt = value; }
        }

        public bool mWalkableBool;

        public bool isWalkable
        {
            get
            {
                return mWalkableBool;
            }
            set
            {
                mWalkableBool = value;
            }

        }

        public bool mAimThroughBool;

        public bool isAimable
        {
            get
            {
                return mAimThroughBool;
            }
            set
            {
                mAimThroughBool = value;
            }

        }

        public bool Equals(SpaceCoordinate other)
        {
            return other != null &&
                this.Coordinates.XCoordinate == other.XCoordinate &&
                this.Coordinates.YCoordinate == other.YCoordinate;
        }

        /*
         * Open List
         * Closed List
         * Add the start node to Open
         * loop
         *  current = node in OPEN wiht the lowest f_cost
         *  remove current form Open
         *  add current to Closed
         *  if current is the target node
         *      return
         *  for each neighbour of the currentnode
         *      if neighbor is not traversable or neighbour in closed
         *          skip to next neighbor
         *      if new path to neighbour is shorter or neighbor is not in OPEN
         *          set fcost of neighbor
         *          set parent of neigbour to curren
         *          if neigbour is not in open
         *              add neighbor to Open
         *       
         */
    }
}
