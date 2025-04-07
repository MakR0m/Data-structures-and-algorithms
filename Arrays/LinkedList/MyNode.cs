using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Structures.LinkedList
{
    internal class MyNode
    {
        public int Value { get; set; }
        public MyNode Next { get; set; }

        public MyNode(int value)
        {
            Value = value;
            Next = null;
        }
    }
}
