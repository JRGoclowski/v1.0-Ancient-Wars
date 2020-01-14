using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Ancient_Wars_v1._0
{
    class NodeGrid : IEnumerable
    {
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
        }

        public BoardNode GetNodeAt(SpaceCoordinate coordArg)
        {
            return mBoardNodes.Find(x => x.Coordinates.Equals(coordArg));
        }

        public IEnumerator GetEnumerator()
        {
            return ((IEnumerable)BoardNodes).GetEnumerator();
        }

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
