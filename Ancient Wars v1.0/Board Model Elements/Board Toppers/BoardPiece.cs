using System;
using System.Collections.Generic;
using System.Text;

namespace Ancient_Wars_v1._0
{
    
    //Represents any object placed on the board, allows for manipulation of such objects in the board scope
    class BoardPiece
    {
        //All board pieces are a token of one form or another, a base class for units, objectives, buildings, etc.
        public BoardPiece(Token tokenArg)
        {
            mToken = tokenArg;
            mBoardIcon = mToken.Icon;
            mTargetableBool = true;
        }

        //=========
        //= Props =
        //=========
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

        private bool mTargetableBool;

        public bool Targetable
        {
            get { return mTargetableBool; }
            set { mTargetableBool = value; }
        }
    }

}