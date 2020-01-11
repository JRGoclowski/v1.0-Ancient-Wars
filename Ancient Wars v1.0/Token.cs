using System;
using System.Collections.Generic;
using System.Text;

namespace Ancient_Wars_v1._0
{
    class Token
    {
        public Token()
        {
            mNameString = "DefName";
            mIconChar = 'N';
            mTokenID = new TokenId();
        }

        public Token(string nameArg, char iconArg)
        {
            mNameString = nameArg;
            mIconChar = iconArg;
            mTokenID = new TokenId();
        }

        private string mNameString;

        public string Name
        {
            get { return mNameString; }
            private set { mNameString = value; }
        }


        private char mIconChar;
        public char Icon
        {
            get { return mIconChar; }
             set { mIconChar = value; }
        }

        private TokenId mTokenID;

        public TokenId TokenID
        {
            get { return mTokenID; }
            set { mTokenID = value; }
        }

        public class TokenId
        {
            private static int counterID = 0;
            
            public TokenId()
            {
                mIDNumberInt = counterID;
                counterID++;
                IDChar = 'N';
                IDstring = IDChar + mIDNumberInt.ToString();
            }

            public TokenId(char charArg)
            {
                mIDNumberInt = counterID;
                counterID++;
                IDChar = charArg;
                IDstring = IDChar + mIDNumberInt.ToString();
            }

            public string IDstring
            {
                get
                {
                    return IDstring;
                }
                set
                {
                    IDstring = value;
                }
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
