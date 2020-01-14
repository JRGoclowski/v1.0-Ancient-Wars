﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Ancient_Wars_v1._0
{
    class NodeEnum : IEnumerator
    {
        public BoardNode[] _Nodes;

        // Enumerators are positioned before the first element
        // until the first MoveNext() call.
        int position = -1;

        public NodeEnum(BoardNode[] list)
        {
            _Nodes = list;
        }

        public bool MoveNext()
        {
            position++;
            return (position < _Nodes.Length);
        }

        public void Reset()
        {
            position = -1;
        }

        object IEnumerator.Current
        {
            get
            {
                return Current;
            }
        }

        public BoardNode Current
        {
            get
            {
                try
                {
                    return _Nodes[position];
                }
                catch (IndexOutOfRangeException)
                {
                    throw new InvalidOperationException();
                }
            }
        }
    }
}
