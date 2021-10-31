using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apostasy.AST
{
    public class Integer : AstNode
    {
        public Token Token { get; private set; }
        public int Value { get; private set; }

        public Integer(Token t)
        {
            Token = t;
            Value = int.Parse(t.Value);
        }
    }
}
