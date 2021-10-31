using System;
using Apostasy.AST;

namespace Apostasy.Interpret
{
    public class IntegerVisitor : IVisitor
    {
        public dynamic Visit(object node)
        {
            Integer numberNode = node as Integer;
            return numberNode.Value;
        }
    }
}