﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

/// <summary>
/// This class is the collection of nodes that will serves as the foundation that the board is built on
/// </summary>
namespace Ancient_Wars_v1._0
{
    class NodeGrid : IEnumerable
    {
        /**
         * Constructor - Instantiate a grid given the bounds to be used, allows for negative bounds
         * Though this isn't really used so may be unnecessary. 
         * Note - As this version is not prepared for delivery, it only insantiates a rectangular board in which 
         * all nodes are walkable and targetable
         */
        public NodeGrid(int minXCoordArg, int maxXCoordArg, int minYCoordArg, int maxYCoordArg)
        {
            mBoardNodes = new List<BoardNode>();
            for (int i = minXCoordArg; i < maxXCoordArg; i++)
            {
                for (int j = minYCoordArg; j < maxYCoordArg; j++)
                {
                    SpaceCoordinate lCoord = new SpaceCoordinate(i, j);
                    BoardNode lBoardNode = new BoardNode(lCoord, true, true);
                    mBoardNodes.Add(lBoardNode);
                }
            }
            mXBounds = new int[2];
            mXBounds[0] = minXCoordArg;
            mXBounds[1] = maxXCoordArg;

            mYBounds = new int[2];
            mYBounds[0] = minYCoordArg;
            mYBounds[1] = maxYCoordArg;

            //Instantiate all directions on the node grid, there may be a better place to put these
            //but this allowed me to begin testing other features
            SpaceMovement DIR_UP = new SpaceMovement(-1, 0, 10);
            SpaceMovement DIR_DOWN = new SpaceMovement(1, 0, 10);
            SpaceMovement DIR_RIGHT = new SpaceMovement(0, 1, 10);
            SpaceMovement DIR_LEFT = new SpaceMovement(0, -1, 10);
            SpaceMovement DIR_UP_RIGHT = new SpaceMovement(1, 1, 14);
            SpaceMovement DIR_UP_LEFT = new SpaceMovement(1, -1, 14);
            SpaceMovement DIR_DOWN_RIGHT = new SpaceMovement(-1, 1, 14);
            SpaceMovement DIR_DOWN_LEFT = new SpaceMovement(-1, -1, 14);

            //Two lists for containing all directions
            mBaseDir = new List<SpaceMovement>();
            mAllDir = new List<SpaceMovement>();

            //Add the cardinal directions to the base direction list, in clockwise order
            mBaseDir.Add(DIR_UP);
            mBaseDir.Add(DIR_RIGHT);
            mBaseDir.Add(DIR_DOWN);
            mBaseDir.Add(DIR_LEFT);

            //Add all 8 directions to the all direction list, in clockwise order
            mAllDir.Add(DIR_UP);
            mAllDir.Add(DIR_UP_RIGHT);
            mAllDir.Add(DIR_RIGHT);
            mAllDir.Add(DIR_DOWN_RIGHT);
            mAllDir.Add(DIR_DOWN);
            mAllDir.Add(DIR_DOWN_LEFT);
            mAllDir.Add(DIR_LEFT);
            mAllDir.Add(DIR_UP_LEFT);

            //Instatiate each node's neighbor list
            foreach (BoardNode iNode in this.BoardNodes)
            {
                iNode.Neighbors = BoardNode.IdentifyNeighbors(this, iNode.Coordinates);
            }
        }

        private List<SpaceMovement> mBaseDir;

        public List<SpaceMovement> BASIC_DIRECTIONS
        {
            get { return mBaseDir; }
            set { mBaseDir = value; }
        }

        private List<SpaceMovement> mAllDir;

        public List<SpaceMovement> ALL_DIRECTIONS
        {
            get { return mAllDir; }
            set { mAllDir = value; }
        }


        public BoardNode GetNodeAt(SpaceCoordinate coordArg)
        {
            return mBoardNodes.Find(x => x.Coordinates.Equals(coordArg));
        }

        public IEnumerator GetEnumerator()
        {
            return ((IEnumerable)BoardNodes).GetEnumerator();
        }
        

        //Definitions of each direction

        private int[] mXBounds;

        public int[] XBounds
        {
            get { return mXBounds; }
            set { mXBounds = value; }
        }

        private int[] mYBounds;

        public int[] YBounds
        {
            get { return mYBounds; }
            set { mYBounds = value; }
        }


        private List<BoardNode> mBoardNodes;

        public List<BoardNode> BoardNodes
        {
            get { return mBoardNodes; }
            set { mBoardNodes = value; }
        }
        
    }
}
