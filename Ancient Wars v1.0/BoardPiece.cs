using System;
using System.Collections.Generic;
using System.Text;

namespace Ancient_Wars_v1._0
{
    
    //The actual pieces that sit on a board. Used primarily for manipulation in the board context
    class BoardPiece
    {
        public BoardPiece(Token tokenArg)
        {
            mToken = tokenArg;
            mBoardIcon = mToken.Icon;
        }

        private Token mToken;
        private char mBoardIcon;


        public char Icon
        {
            get { return mBoardIcon; }
            set { mBoardIcon = value; }
        }

        public Token Token
        {
            get { return mToken; }
            set { mToken = value; }
        }

        private bool mCreatureBool;

        public bool hasCreature
        {
            get { return mCreatureBool; }
            set { mCreatureBool = value; }
        }

        private bool mObjectBool;

        public bool hasObject
        {
            get { return mObjectBool; }
            set { mObjectBool = value; }
        }

    }

}