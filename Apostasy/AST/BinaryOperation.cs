using System;

namespace Apostasy.AST
{
    public class BinaryOperation
    {
        public  Object Left;
        public  Object Right;
        public Token Op { get; private set; }

        public BinaryOperation(Object l, Object r, Token o)
        {
            Left = l;
            Right = r;
            Op = o;
        }

    }
}
