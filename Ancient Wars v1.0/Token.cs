using System;
using System.Collections.Generic;
using System.Text;

namespace Ancient_Wars_v1._0
{
    //Base class for any object that is placed onto the board that is not a part of the terrain
    //Used primarily for identification and data
    class Token : IEquatable<Token>
    {

        //Default token constructor
        public Token()
        {
            mNameString = "DefName";
            mIconChar = 'N';
            mTokenID = new TokenId();
        }

        //Constructor when given a name and icon
        public Token(string nameArg, char iconArg)
        {
            mNameString = nameArg;
            mIconChar = iconArg;
            mTokenID = new TokenId();
        }

        public Token(string nameArg, char iconArg, char teamArg)
        {
            mNameString = nameArg;
            mIconChar = iconArg;
            mTokenID = new TokenId(teamArg);
        }
               

        //Returns a token's ID
        public string GetID()
        {
            return mTokenID.IDString;
        }

        public override bool Equals(object Arg)
        {
            try
            {
                Token lToken = (Token)Arg;
                if (this.TokenID.IDNumber == lToken.TokenID.IDNumber)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        public bool Equals(Token other)
        {
            return other != null &&
                   EqualityComparer<TokenId>.Default.Equals(TokenID, other.TokenID);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(TokenID);
        }

        /// <summary>
        /// ==============
        /// Properties
        /// ==============
        /// </summary>

        //Name of the token
        private string mNameString;

        public string Name
        {
            get { return mNameString; }
            private set { mNameString = value; }
        }

        //symbol to represent the token on the board
        private char mIconChar;
        public char Icon
        {
            get { return mIconChar; }
             set { mIconChar = value; }
        }

        //Token's ID
        private TokenId mTokenID;
        public TokenId TokenID
        {
            get { return mTokenID; }
            set { mTokenID = value; }
        }

        //Class defines the method for definining a token's ID
        public class TokenId
        {
            //Counter to serialize the ID Number of all tokens. 
            private static int counterID = 0;
            
            public TokenId()
            {
                mIDNumberInt = counterID;
                counterID++;
                IDChar = 'N';
                mIDStringVar = IDChar + mIDNumberInt.ToString();
            }

            public TokenId(char charArg)
            {
                mIDNumberInt = counterID;
                counterID++;
                IDChar = charArg;
                mIDStringVar = IDChar + mIDNumberInt.ToString();
            }


            private string mIDStringVar;

            public string IDString
            {
                get { return mIDStringVar; }
                set { mIDStringVar = value; }
            }

            private char mIDChar;

            public char IDChar
            {
                get { return mIDChar; }
                set { mIDChar = value; }
            }


            private int mIDNumberInt;

            public int IDNumber
            {
                get { return mIDNumberInt; }
                set { mIDNumberInt = value; }
            }
        }
    }
}
