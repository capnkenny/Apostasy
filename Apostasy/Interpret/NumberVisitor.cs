using System;
using Apostasy.AST;

namespace Apostasy.Interpret
{
    public class NumberVisitor : IVisitor
    {
        public dynamic Visit(object node)
        {
            Number numberNode = node as Number;
            return numberNode.Value;
        }
    }
}