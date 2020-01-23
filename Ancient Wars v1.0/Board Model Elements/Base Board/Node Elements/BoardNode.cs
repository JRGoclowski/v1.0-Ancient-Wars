using System;
using System.Collections.Generic;
using System.Text;
/*
 * This class serves as a base element for pathfinding and movement. Nodes are required for the
 * A* pathfinding algorithm, and this class contains the relevant member variables
 */
namespace Ancient_Wars_v1._0
{
    //This is a link the a website that explains the A* pathfinding algorithm
    ///https://gigi.nullneuron.net/gigilabs/a-pathfinding-example-in-c/
    ///

    class BoardNode
    {
        public BoardNode(SpaceCoordinate coordArg, bool walkArg, bool aimBool)
        {
            //Coordinates representing position
            mSpaceCoordinate = coordArg;

            //booleans used for pathfinding and targetting enemies
            IsWalkable = walkArg;
            IsAimable = aimBool;

            //FGH costs are the values used to improve pathfinding in A*
            mGCostInt = 0;
            mHCostInt = 0;
            mFCostInt = 0;

            //used to track the last space in a particular path
            mParentNode = null;
        }

        //Nodes need to know what their neighbors are for pathfinding to be efficient
        public static List<BoardNode> IdentifyNeighbors(NodeGrid gridArg, SpaceCoordinate coordArg)
        {

            List<BoardNode> lNeighbors = new List<BoardNode>();
            foreach (SpaceMovement iMove in gridArg.ALL_DIRECTIONS)
            {
                SpaceCoordinate possNeighborCoord = coordArg.CoordAtMove(iMove);
                BoardNode lNode = gridArg.BoardNodes.Find(x => x.Coordinates.Equals(coordArg));
                if (lNode != null)
                {
                    lNeighbors.Add(lNode);
                }
            }
            return lNeighbors;
        }


        private BoardNode mParentNode;

        public BoardNode Parent
        {
            get { return mParentNode; }
            set { mParentNode = value; }
        }


        private List<BoardNode> mNeigherborList;

        public List<BoardNode> Neighbors
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

        public bool IsWalkable
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

        public bool IsAimable
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
