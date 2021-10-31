using System;

namespace Apostasy.AST
{
    public class UnaryOperation : AstNode
    {
        public  Object Right;
        public Token Op { get; private set; }

        public UnaryOperation(Object r, Token o)
        {
            Right = r;
            Op = o;
        }

    }
}
