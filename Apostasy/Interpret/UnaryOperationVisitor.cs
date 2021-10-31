using System;
using System.IO.MemoryMappedFiles;
using System.Runtime.CompilerServices;
using Apostasy.AST;

namespace Apostasy.Interpret
{
    public class UnaryOperationVisitor : IVisitor
    {
        public dynamic Visit(object incomingNode)
        {
            IVisitor visitor = null;

            if (incomingNode is UnaryOperation unaryNode)
            {
                if (unaryNode.Op.Type == TokenType.PLUS)
                {
                    if (unaryNode.Right.GetType() == typeof(UnaryOperation))
                        return +(Visit(unaryNode.Right));
                    else
                    {
                        var newNode = DetermineNodeTypeWithVisitor(ref unaryNode.Right, ref visitor);
                        return visitor.Visit(newNode);
                    }
                }
                else if (unaryNode.Op.Type == TokenType.MINUS)
                {
                    if (unaryNode.Right.GetType() == typeof(UnaryOperation))
                        return -(Visit(unaryNode.Right));
                    else
                    {
                        var newNode = DetermineNodeTypeWithVisitor(ref unaryNode.Right, ref visitor);
                        return -visitor.Visit(newNode);
                    }
                }
                
                var node = DetermineNodeTypeWithVisitor(ref unaryNode.Right, ref visitor);
                return visitor.Visit(node);
            }

            // var nodeType = incomingNode.GetType();
            // if (nodeType == typeof(Integer))
            // {
            //     unaryNode = incomingNode as Integer;
            //     visitor = new IntegerVisitor();
            // }
            // else if (nodeType == typeof(Number))
            // {
            //     unaryNode = incomingNode as Number;
            //     visitor = new NumberVisitor();
            // }
            // else
            // {
            //     unaryNode = incomingNode as UnaryOperation;
            //     visitor = new UnaryOperationVisitor();
            // }
            
            
            else
            {
                throw new NotImplementedException("Unknown unary node supplied!");
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
            else
            {
                visitor = new NumberVisitor();
                return node as Number;
            }
        }

    }
}