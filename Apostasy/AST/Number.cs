using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apostasy.AST
{
    public class Number : AstNode
    {
        public Token Token { get; private set; }
        public double Value { get; private set; }

        public Number(Token t)
        {
            Token = t;
            Value = double.Parse(t.Value);
        }
    }
}
