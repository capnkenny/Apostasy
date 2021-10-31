using System;

namespace Apostasy.AST
{
    public class Parser
    {
        private readonly Lexer _lexer;
        private Token _currentToken;
        public Parser(Lexer l)
        {
            _lexer = l;
            _currentToken = l.GetNextTokenImpl();
        }

        private void EatToken(TokenType type)
        {
            if (_currentToken.Type == type)
            {
                _currentToken = _lexer.GetNextTokenImpl();
            }
            else
            {
                throw new Exception("Invalid syntax detected.");
            }
        }

        private dynamic Factor()
        {
            if (_currentToken.Type == TokenType.INTEGER)
            {
                var token = _currentToken;
                EatToken(TokenType.INTEGER);
                return new Number(token);
            }
            else if (_currentToken.Type == TokenType.NUMBER)
            {
                var token = _currentToken;
                EatToken(TokenType.NUMBER);
                return new Number(token);
            }
            else if (_currentToken.Type == TokenType.LPAREN)
            {
                EatToken(TokenType.LPAREN);
                var node = Expression();
                EatToken(TokenType.RPAREN);
                return node;
            }
            else
            {
                throw new Exception("Invalid token node!");
            }
        }

        private dynamic Term()
        {
            var node = Factor();

            while (_currentToken.Type is TokenType.MULT or TokenType.DIV)
            {
                var token = _currentToken;
                if (token.Type is TokenType.MULT)
                {
                    EatToken(TokenType.MULT);
                }
                else
                {
                    EatToken(TokenType.DIV);
                }

                node = new BinaryOperation(node, Factor(), token);
            }

            return node;
        }

        private dynamic Expression()
        {
            var node = Term();
            
            while (_currentToken.Type is TokenType.PLUS or TokenType.MINUS)
            {
                var token = _currentToken;
                if (token.Type is TokenType.PLUS)
                {
                    EatToken(TokenType.PLUS);
                }
                else
                {
                    EatToken(TokenType.MINUS);
                }

                node = new BinaryOperation(node, Term(), token);
            }

            return node;
        }

        public dynamic Parse() => Expression();

    }
}
