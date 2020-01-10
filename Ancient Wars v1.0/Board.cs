using System;
using System.Collections.Generic;
using System.Text;


namespace Ancient_Wars_v1._0
{
    class Board
    {
        //Private contained class boardspace represents each square of the grid
        private class BoardSpace
        {
            

            //Construct a boardspace with x and y coordinates
            public BoardSpace(SpaceCoordinate coordArg)
            {

                mCoordinate = coordArg;

            }

            //Construct a boardspace with x and y coordinates, as well as a boardpiece
            public BoardSpace(SpaceCoordinate coordArg, BoardPiece pieceArg)
            {

                mCoordinate = coordArg;

                PieceAt = pieceArg;
                mPiecePresentBool = true;

            }

            //Returns the coordinates of the boardspaces in x,y order
            public SpaceCoordinate GetCoords()
            { 
                return this.Coordinates;
            }

            //Updates an empty board space to a new unit token
            public void SetNewPieceValue(BoardPiece mPieceArg)
            {
                mPiece = mPieceArg;
                mPiecePresentBool = true;
            }

            //Empties the board space of any board pieces
            public void EmptyPiece()
            {
                mPiece = null;
                mPiecePresentBool = false;
            }

            //=========
            //= Props =
            //=========
            private SpaceCoordinate mCoordinate;

            public SpaceCoordinate Coordinates
            {
                get { return mCoordinate; }
                set { mCoordinate = value; }
            }


            private BoardPiece mPiece;

            public BoardPiece PieceAt
            {
                get
                {
                    return mPiece;
                }
                set
                {
                    mPiece = value;
                    if (value != null)
                    {
                        mPiecePresentBool = true;
                    }
                    else
                    {
                        mPiecePresentBool = false;
                    }
                }

            }

            public char Icon
            {
                get
                {
                    if (hasPiece)
                    {
                        return PieceAt.Icon;
                    }
                    else
                    {
                        return '.';
                    }
                }
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

            private bool mPiecePresentBool;

            public bool hasPiece
            {
                get { return mPiecePresentBool; }
                private set { mPiecePresentBool = value; }
            }



        }
        public Board(int xDimension, int yDimension)
        {

            ConstructGrid(xDimension, yDimension);
            mDimensionArray = new int[2];
            mDimensionArray[0] = xDimension;
            mDimensionArray[1] = yDimension;

        }

        //mGridArray represents the board as a collection of board spaces
        static private List<List<BoardSpace>> mGridArray;
        private int[] mDimensionArray;

        private void ConstructGrid(int xArg, int yArg)
        {
            List<List<BoardSpace>> lGrid = new List<List<BoardSpace>>();
            for (int i = 0; i < xArg; i++)
            {
                List<BoardSpace> lRowList = new List<BoardSpace>();
                for (int j = 0; j < yArg; j++)
                {
                    lRowList.Add(new BoardSpace(new SpaceCoordinate (xArg, yArg)));
                }
                lGrid.Add(lRowList);
            }
            mGridArray = lGrid;
        }

        public BoardPiece GetBoardPieceAt(SpaceCoordinate coordArg)
        {
            return GetBoardSpace(coordArg.XCoordinate, coordArg.YCoordinate).PieceAt;
        }

        public void SetBoardPieceAt(BoardPiece pieceArg, SpaceCoordinate coordArg)
        {
            GetBoardSpace(coordArg.XCoordinate, coordArg.YCoordinate).SetNewPieceValue(pieceArg);
        }

        public char GetSpaceIcon(int xArg, int yArg)
        {
            return GetBoardSpace(xArg, yArg).Icon;
        }

        public List<SpaceCoordinate> GetOccupiedCoords()
        {
            List<SpaceCoordinate> occSpaceCoords = new List<SpaceCoordinate>();
            List<SpaceCoordinate> lSpaceList = GetOccupiedCoordsFromBoard();
            foreach (SpaceCoordinate iBSpace in lSpaceList)
            {
                occSpaceCoords.Add(iBSpace);
            }
            return occSpaceCoords;
        }        

        private BoardSpace GetBoardSpace(int xArg, int yArg) => mGridArray[xArg][yArg];

        private List<SpaceCoordinate> GetOccupiedCoordsFromBoard()
        {
            List<SpaceCoordinate> lSpaceList = new List<SpaceCoordinate>();
            foreach (List<BoardSpace> lList in mGridArray)
            {
                foreach (BoardSpace lBoardSpace in lList)
                {
                    if (lBoardSpace.hasPiece)
                    {
                        lSpaceList.Add(lBoardSpace.Coordinates);
                    }
                }
            }
            return lSpaceList;
        }

        public bool withinBounds(SpaceCoordinate coordArg)
        { 
            if (coordArg.XCoordinate > 0 && coordArg.XCoordinate < this.mDimensionArray[0])
            {
                if (coordArg.YCoordinate > 0 && coordArg.YCoordinate < this.mDimensionArray[1])
                {
                    return true;
                }
            }
            return false;
        }
        public int[] Dimensions
        {
            get { return mDimensionArray; }
            private set { mDimensionArray = value; }
        }
    }
}
