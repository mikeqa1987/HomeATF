using System;
using System.Collections.Generic;
using System.Text;

namespace GenericsLearn
{
    internal sealed class TypedNode<T> : Node
    {
        public T mData;

        public TypedNode(T data) : this(data, null)
        { }

        public TypedNode(T data, Node next) : base(next)
        {
            this.mData = data;
        }

        public override string ToString()
        {
            return this.mData.ToString() + ((this.mNext != null) ? this.mNext.ToString() : "No next nodes found");
        }
    }
}
