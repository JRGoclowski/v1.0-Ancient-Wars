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

        /*
         * Open List
         * Closed List
         * Add the start node to Open
         * loop
         *  current = node in OPEN wiht the lowest f_cost
         *  remove current form Open
         *  add current to Closed
         */
        public Queue<BoardNode> FindPath(BoardNode startArg, BoardNode endArg)
        {
            mOpenList.Add(startArg);
            BoardNode currentNode = startArg;
            bool pathFound = false;
            while (!pathFound)
            {
                foreach (BoardNode iNode in mOpenList)
                {
                    if (iNode.FCost < currentNode.FCost)
                    {
                        currentNode = iNode;
                    }
                }
                mOpenList.Remove(currentNode);
                mClosedList.Add(currentNode);
            }
            if (currentNode == endArg)
            {
                //Create Queue and return
            }
            foreach (BoardNode iNeighbor in currentNode.Neighbors)
            {
                if (!iNeighbor.IsWalkable || mClosedList.Contains(iNeighbor))
                {
                    continue;
                }

            }
            return null;
        }

        /*  if current is the target node
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
        private List<BoardNode> mOpenList;

        public List<BoardNode> OpenList
        {
            get { return mOpenList; }
            set { mOpenList = value; }
        }

        private List<BoardNode> mClosedList;

        public List<BoardNode> ClosedList
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
