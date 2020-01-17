using System;
using System.Collections.Generic;
using System.Text;


namespace Ancient_Wars_v1._0
{
    class Board
    {
        //Private contained class boardspace represents each square of the grid
        public class BoardSpace
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

                public int EdgeWeight
                {
                    get { return edgeWeight; }
                    set { edgeWeight = value; }
                }


            }

            //Construct a boardspace with x and y coordinates
            

            //TODO REDO
            public BoardSpace(BoardNode nodeArg)
            {

                mBoardNode = nodeArg;

                mNeighborList = new List<BoardSpace>();

                mPiecePresentBool = false;

            }


            //Construct a boardspace with x and y coordinates, as well as a boardpiece

            //TODO REDO
            public BoardSpace(BoardNode nodeArg, BoardPiece pieceArg)
            {

                mBoardNode = nodeArg;

                mNeighborList = new List<BoardSpace>();

                PieceAt = pieceArg;
                mPiecePresentBool = true;

            }

            //Returns the coordinates of the board space
            public SpaceCoordinate GetCoords()
            { 
                return this.Node.Coordinates;
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

           public void AddNeighbor(BoardSpace spaceArg)
            {
                mNeighborList.Add(spaceArg);
            }

            //=========
            //= Props =
            //=========

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
            
            private bool mPiecePresentBool;

            public bool hasPiece
            {
                get { return mPiecePresentBool; }
                private set { mPiecePresentBool = value; }
            }

            

        }
        public Board(int minXArg, int maxXArg, int minYArg, int maxYArg)
        {
            mNodeGrid = new NodeGrid(minXArg, maxXArg, minYArg, maxYArg);
            mBoardSpaces = new List<BoardSpace>();
            ConstructSpaces();
        }

        private List<BoardSpace> mBoardSpaces;

        public List<BoardSpace> BoardSpaces
        {
            get { return mBoardSpaces; }
            set { mBoardSpaces = value; }
        }


        //mGridArray represents the board as a collection of board spaces
        private NodeGrid mNodeGrid;

        public NodeGrid NodeGrid
        {
            get { return mNodeGrid; }
            private set { mNodeGrid = value; }
        }


        private void ConstructSpaces()
        {
            foreach (BoardNode iNode in mNodeGrid)
            {
                mBoardSpaces.Add(new BoardSpace(iNode));
            }
        }

       
        //Returns the boardpiece at a coordinate
        public BoardPiece GetBoardPieceAt(SpaceCoordinate coordArg)
        {
            return GetBoardSpace(coordArg).PieceAt;
        }

        public BoardSpace GetBoardSpaceOfToken(Token tokenArg)
        {
            List<SpaceCoordinate> occCoords = GetOccupiedCoordsFromBoard();
            foreach(SpaceCoordinate iCoord in occCoords)
            {
                if (tokenArg.TokenID == GetBoardPieceAt(iCoord).Token.TokenID)
                {
                    return GetBoardSpace(iCoord);
                }
            }
            return null;
        }

        public void SetBoardPieceAt(BoardPiece pieceArg, SpaceCoordinate coordArg)
        {
            GetBoardSpace(coordArg).SetNewPieceValue(pieceArg);
        }

        public char GetSpaceIcon(SpaceCoordinate coordArg)
        {
            return GetBoardSpace(coordArg).Icon;
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

        private BoardSpace GetBoardSpace(SpaceCoordinate coordArg)
        {
            var testVal = this.BoardSpaces.Find(x => x.Node.Coordinates.Equals(coordArg));
            return testVal;
        }

        public BoardSpace SpaceAtMove(SpaceCoordinate coordArg, SpaceMovement moveArg)
        {
            SpaceCoordinate lCoord = coordArg.CoordAtMove(moveArg);
            return GetBoardSpace(lCoord);
        }

        private List<SpaceCoordinate> GetOccupiedCoordsFromBoard()
        {
            List<SpaceCoordinate> lSpaceList = new List<SpaceCoordinate>();
            foreach (BoardSpace iSpace in mBoardSpaces)
            {                
                if (iSpace.hasPiece)
                {
                    lSpaceList.Add(iSpace.Node.Coordinates);
                }
            }
            return lSpaceList;
        }

        public static bool withinBounds(Board boardArg, SpaceCoordinate coordArg)
        {
            bool xInBounds = false, yInBounds = false;
            if (coordArg.XCoordinate >= boardArg.NodeGrid.XBounds[0] && coordArg.XCoordinate < boardArg.NodeGrid.XBounds[1])
            {
                xInBounds = true;
            }
            if (coordArg.YCoordinate >= boardArg.NodeGrid.YBounds[0] && coordArg.YCoordinate < boardArg.NodeGrid.YBounds[1])
            {
                yInBounds = true;
            }
            return (xInBounds && yInBounds);
        }

        public bool SpaceWalkable(SpaceCoordinate coordArg)
        {
            return GetBoardSpace(coordArg).Node.IsWalkable;
        }
    }
}
