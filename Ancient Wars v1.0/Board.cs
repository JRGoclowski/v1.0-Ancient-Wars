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
            public class SpaceEdge
            {
                enum edgeName
                {
                    NORTH = 0,
                    EAST = 1,
                    SOUTH = 2,
                    WEST = 3,
                }

                private int edgeWeight;

                public int MyProperty
                {
                    get { return edgeWeight; }
                    set { edgeWeight = value; }
                }


            }

            //Construct a boardspace with x and y coordinates
            public BoardSpace(SpaceCoordinate coordArg)
            {

                mCoordinate = coordArg;

                mNeighborList = new List<BoardSpace>();
            }

            public BoardSpace(SpaceCoordinate coordArg, bool walkBool, bool aimBool)
            {

                mCoordinate = coordArg;

                mNeighborList = new List<BoardSpace>();

                mWalkableBool = walkBool;
                mAimThroughBool = aimBool;
                                
                mPiecePresentBool = false;

            }

            //Construct a boardspace with x and y coordinates, as well as a boardpiece
            public BoardSpace(SpaceCoordinate coordArg, BoardPiece pieceArg, bool walkBool, bool aimBool)
            {

                mCoordinate = coordArg;

                mNeighborList = new List<BoardSpace>();

                mWalkableBool = walkBool;
                mAimThroughBool = aimBool;

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

            //Returns all "neighbors" to a board space
            public static List<BoardSpace> IdentifyNeighbors(Board boardArg, BoardSpace spaceArg)
            {

                List<BoardSpace> lNeighbors = new List<BoardSpace>();

                foreach (SpaceMovement iMove in SpaceMovement.BASIC_DIRECTIONS)
                {
                    BoardSpace possNeighbor = spaceArg.SpaceAtMove(iMove);
                    if (Board.withinBounds(boardArg, possNeighbor.Coordinates))
                    {
                        lNeighbors.Add(possNeighbor);
                    }
                }
                return lNeighbors;
            }



            public void AddNeighbor(BoardSpace spaceArg)
            {
                mNeighborList.Add(spaceArg);
            }

            private BoardSpace SpaceAtMove(SpaceMovement moveArg)
            {
                int newRow = this.Coordinates.RowCoordinate + moveArg.RowMovement;
                int newCol = this.Coordinates.ColCoordinate + moveArg.ColMovement;
                BoardSpace lSpace = new BoardSpace(new SpaceCoordinate(newRow, newCol));
                return lSpace;
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

            private List<BoardSpace> mNeighborList;

            public List<BoardSpace> Neighbors
            {
                get { return mNeighborList; }
                set { mNeighborList = value; }
            }

            private BoardNode mBoardNode;

            public BoardNode Node
            {
                get { return mBoardNode; }
                set { mBoardNode = value; }
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
        public Board(int rowDimension, int colDimension)
        {

            ConstructGrid(rowDimension, colDimension);
            mDimensionArray = new int[2];
            mDimensionArray[0] = rowDimension;
            mDimensionArray[1] = colDimension;

        }

        //mGridArray represents the board as a collection of board spaces
        static private List<BoardSpace> mGridArray;
        private int[] mDimensionArray;

        private void ConstructGrid(int rowArg, int colArg)
        {
            List<BoardSpace> lGrid = new List<BoardSpace>();
            for (int i = 0; i < rowArg; i++)                                
            {
                List<BoardSpace> lRowList = new List<BoardSpace>();
                for (int j = 0; j < colArg; j++)
                {
                    lGrid.Add(new BoardSpace(new SpaceCoordinate(i, j), true, true));
                }                
            }
            mGridArray = lGrid;
        }

        //Populates the neighbor lists of each boardspace
        private void AddValidNeighbors()
        {
            foreach (BoardSpace iSpace in mGridArray)
            {
                foreach(BoardSpace iNeighborSpace in BoardSpace.IdentifyNeighbors(this, iSpace))
                {
                    iSpace.AddNeighbor(iNeighborSpace);
                }
            }
        }

        //Returns the boardpiece at a coordinate
        public BoardPiece GetBoardPieceAt(SpaceCoordinate coordArg)
        {
            return GetBoardSpace(coordArg.RowCoordinate, coordArg.ColCoordinate).PieceAt;
        }

        public void SetBoardPieceAt(BoardPiece pieceArg, SpaceCoordinate coordArg)
        {
            GetBoardSpace(coordArg.RowCoordinate, coordArg.ColCoordinate).SetNewPieceValue(pieceArg);
        }

        public char GetSpaceIcon(int rowArg, int colArg)
        {
            return GetBoardSpace(rowArg, colArg).Icon;
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

        private BoardSpace GetBoardSpace(int rowArg, int colArg)
        {
            foreach (BoardSpace iSpace in mGridArray)
            {
                if (iSpace.GetCoords().RowCoordinate == rowArg)
                {
                    if (iSpace.GetCoords().ColCoordinate == colArg)
                    {
                        return iSpace;
                    }
                }
            }
            return null;
        }

        private List<SpaceCoordinate> GetOccupiedCoordsFromBoard()
        {
            List<SpaceCoordinate> lSpaceList = new List<SpaceCoordinate>();
            foreach (BoardSpace iSpace in mGridArray)
            {                
                if (iSpace.hasPiece)
                {
                    lSpaceList.Add(iSpace.Coordinates);
                }
            }
            return lSpaceList;
        }

        public static bool withinBounds(Board boardArg, SpaceCoordinate coordArg)
        {
            bool xInBounds = false, yInBounds = false;            
            if (coordArg.RowCoordinate > 0 && coordArg.RowCoordinate < boardArg.mDimensionArray[0])
            {
                xInBounds = true;
            }
            if (coordArg.ColCoordinate > 0 && coordArg.ColCoordinate < boardArg.mDimensionArray[1])
            {
                yInBounds = true;
            }
            return (xInBounds && yInBounds);
        }

        public bool SpaceWalkable(SpaceCoordinate coordArg)
        {
            return GetBoardSpace(coordArg.RowCoordinate, coordArg.ColCoordinate).isWalkable;
        }

        public int[] Dimensions
        {
            get { return mDimensionArray; }
            private set { mDimensionArray = value; }
        }
    }
}
