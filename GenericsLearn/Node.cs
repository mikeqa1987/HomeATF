using System;
using System.Collections.Generic;
using System.Text;

namespace GenericsLearn
{
    internal class Node  
    {
        public Node mNext;

        public Node(Node next)
        {
            this.mNext = next; 
        }
    }
}
