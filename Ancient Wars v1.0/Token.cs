using System;
using System.Collections.Generic;
using System.Text;

namespace Ancient_Wars_v1._0
{
    class Token
    {
        private char mIconChar;
        public char Icon
        {
            get { return mIconChar; }
             set { mIconChar = value; }
        }

        private static int counterID = 0;
        private class TokenId
        {
            public TokenId()
            {
                IDnumber = counterID;
                counterID++;
                IDChar = 'N';
                IDstring = IDChar + IDnumber.ToString();
            }

            public TokenId(char charArg)
            {
                IDnumber = counterID;
                counterID++;
                IDChar = charArg;
                IDstring = IDChar + IDnumber.ToString();
            }

            private string IDstring
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

            private char IDChar
            {
                get
                {
                    return IDChar;
                }
                set
                {
                    IDChar = value;
                }
            }

            private static int IDnumber
            {
                get
                {
                    return IDnumber;
                }
                set
                {
                    IDnumber = value;
                }

            }
        }
    }
}
