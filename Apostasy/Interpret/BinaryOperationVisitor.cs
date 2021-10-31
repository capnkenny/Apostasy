using System;
using System.Runtime.CompilerServices;
using Apostasy.AST;

namespace Apostasy.Interpret
{
    public class BinaryOperationVisitor : IVisitor
    {
        public dynamic Visit(object node)
        {
            if (node is not BinaryOperation binaryNode) throw new ArgumentNullException("The binary node provided did not cast properly!");
            
            IVisitor leftVisitor = null;
            IVisitor rightVisitor = null;
            AstNode left = DetermineNodeTypeWithVisitor(ref binaryNode.Left, ref leftVisitor);
            AstNode right = DetermineNodeTypeWithVisitor(ref binaryNode.Right, ref rightVisitor);
            
            switch (binaryNode.Op.Type)
            {
                case TokenType.PLUS:
                {
                    return leftVisitor.Visit(left) + rightVisitor.Visit(right);
                }
                case TokenType.MINUS:
                {
                    return leftVisitor.Visit(left) - rightVisitor.Visit(right);
                }
                case TokenType.MULT:
                {
                    return leftVisitor.Visit(left) * rightVisitor.Visit(right);
                }
                case TokenType.DIV:
                {
                    return leftVisitor.Visit(left) / rightVisitor.Visit(right);
                }
                default:
                {
                    throw new Exception("Invalid binary operation visit!");
                }
            }
        }

        private AstNode DetermineNodeTypeWithVisitor(ref object node, ref IVisitor visitor)
        {
            var nodeType = node.GetType();
            if (nodeType == typeof(Integer))
            {
                visitor = new IntegerVisitor();
                return node as Integer;
            }
            else if (nodeType == typeof(Number))
            {
                visitor = new NumberVisitor();
                return node as Number;
            }
            else
            {
                double newNode = Visit(node);
                var tempNumber = new Number(new Token(TokenType.NUMBER, $"{newNode}", -1));
                visitor = new NumberVisitor();
                return tempNumber;
            }

        }
    }
}