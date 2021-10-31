using System;
using Apostasy.AST;

namespace Apostasy.Interpret
{
    public class Interpreter
    {
        private readonly Parser _parser;
        
        public Interpreter(Parser parser)
        {
            _parser = parser;
        }

        public dynamic Interpret()
        {
            var tree = _parser.Parse();
            return VisitNode(tree);
        }

        private dynamic VisitNode(dynamic node)
        {
            if (node is BinaryOperation)
            {
                return new BinaryOperationVisitor().Visit(node);
            }
            else if (node is Number)
            {
                return new NumberVisitor().Visit(node);
            }
            else
            {
                throw new NotImplementedException("Unknown ASTNode - handling not implemented yet!");
            }
        }
    }
}