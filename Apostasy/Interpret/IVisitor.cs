using System;
using Apostasy.AST;

namespace Apostasy.Interpret
{
    public interface IVisitor
    {
        dynamic Visit(object node);
    }
}