using System;
using System.Collections.Generic;
using System.Text;

/// <summary>
/// The highest level object to model the board 
/// </summary>
namespace Ancient_Wars_v1._0
{
    class Board
    {
        //contained class boardspace represents each square of the board and the relevant info
        //Of such a space
        public class BoardSpace
        {
            
            //Unimplemeneted feature of A* algorithm design patterns, allows for more complex
            //pathfinding calculations
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

            //Constructs a board space on top of a provided node argument, obtained from the nodegrid
            public BoardSpace(BoardNode nodeArg)
            {

                mBoardNode = nodeArg;

                mPiecePresentBool = false;

            }


            //Constructs a board space on top of a provided node argument, obtained from the nodegrid 
            //as well as a boardpiece, if placing boardpieces is required at instantiation
            public BoardSpace(BoardNode nodeArg, BoardPiece pieceArg)
            {

                mBoardNode = nodeArg;
                
                PieceAt = pieceArg;
                mPiecePresentBool = true;

            }

            //Returns the coordinates of the board space, may not be necesarry with minor refactoring 
            public SpaceCoordinate GetCoords()
            { 
                return this.Node.Coordinates;
            }


            //Updates an empty board space to a new unit token
            public void SetNewPieceValue(BoardPiece mPieceArg)
            {
                if (!this.hasPiece)
                {
                    mPiece = mPieceArg;
                    mPiecePresentBool = true;
                }                
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


        //===========
        //== Board ==
        //===========
        public Board(int minXArg, int maxXArg, int minYArg, int maxYArg)
        {
            mNodeGrid = new NodeGrid(minXArg, maxXArg, minYArg, maxYArg);
            mBoardSpaces = new List<BoardSpace>();
            ConstructSpaces();
        }

        //Creates all the boardspaces using each node in the nodegrid
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

        //Returns the boardspace of a given token
        public BoardSpace GetBoardSpaceOfToken(Token tokenArg)
        {
            List<SpaceCoordinate> occCoords = GetOccupiedCoordsFromBoard();
            foreach (SpaceCoordinate iCoord in occCoords)
            {
                if (tokenArg.TokenID == GetBoardPieceAt(iCoord).Token.TokenID)
                {
                    return GetBoardSpace(iCoord);
                }
            }
            return null;
        }

        //Place a board piece at a coordinate, straightforward
        public void SetBoardPieceAt(BoardPiece pieceArg, SpaceCoordinate coordArg)
        {
            GetBoardSpace(coordArg).SetNewPieceValue(pieceArg);
        }

        //Returns the character that represents whatever Icon a boardspace has
        public char GetSpaceIcon(SpaceCoordinate coordArg)
        {
            return GetBoardSpace(coordArg).Icon;
        }


        //The following two sections likely could be optimized/one removed, one was written when boardspaces had been a private
        //class, which has since changed. Either way, they return the coordinates of every occupied boardspace
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

        //Searches the board for a board space when given a coordinate
        //I welcome optimization of this, if you can think of a better way. 
        private BoardSpace GetBoardSpace(SpaceCoordinate coordArg)
        {
            var testVal = this.BoardSpaces.Find(x => x.Node.Coordinates.Equals(coordArg));
            return testVal;
        }

        //returns the board space that is at a particular movement from an initial coordinate
        public BoardSpace SpaceAtMove(SpaceCoordinate coordArg, SpaceMovement moveArg)
        {
            SpaceCoordinate lCoord = coordArg.CoordAtMove(moveArg);
            return GetBoardSpace(lCoord);
        }

        //checks if a space coordinate is within the bounds of the board, often used to check move validity
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

        //returns if a board space is walkable, but this may not be necessary
        //Atrifacted by addition of nodes, likely could be replaced
        public bool SpaceWalkable(SpaceCoordinate coordArg)
        {
            return GetBoardSpace(coordArg).Node.IsWalkable;
        }


        //=========
        //= Props =
        //=========
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


    }
}
