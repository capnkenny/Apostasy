using System;
using System.IO;
using System.Text;

namespace Apostasy
{
    public class Lexer
    {
        private string _identifier;
        private double _numberValue;
        private const int Eof = -1;
        private readonly TextReader _reader;
        private readonly StringBuilder _identifierBuilder = new StringBuilder();
        private readonly StringBuilder _numberBuilder = new StringBuilder();
        private int _c = ' ';
        private int _lineNumber = 0;

        public Lexer(string toLex, bool isFile)
        {
            StreamReader textReader;

            try
            {
                if (isFile)
                    textReader = new StreamReader(toLex);
                else
                {
                    var stream = new MemoryStream();
                    var writer = new StreamWriter(stream);
                    writer.Write(toLex);
                    writer.Flush();
                    stream.Position = 0;
                    textReader = new StreamReader(stream);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Exception: {e.Message}");
                return;
            }

            _reader = textReader;
            _lineNumber++;
        }

        public string GetLastIdentifier() => _identifier;

        public double GetLastNumber() => _numberValue;
        
        public Token GetNextTokenImpl()
        {
            if (_c != '(' && _c != ')')
            {
                if (_c == 0x0a)
                {
                    _lineNumber++;
                }
                _c = ' ';
            }

            while (char.IsWhiteSpace((char)_c))
            {
                if (_c == 0x0a)
                {
                    _lineNumber++;
                }
                _c = _reader.Read();
            }

            if (char.IsLetter((char)_c) || _c == '_')
            {
                _identifierBuilder.Append((char)_c);
                while (char.IsLetterOrDigit((char)(_c = _reader.Read())) || _c == '.' || _c == '_')
                {
                    _identifierBuilder.Append((char)_c);
                }

                _identifier = _identifierBuilder.ToString();
                _identifierBuilder.Clear();

                switch (_identifier)
                {
                    
                    case "as":
                        {
                            return new Token(TokenType.AS, "as", _lineNumber);
                        }
                    case "at":
                        {
                            return new Token(TokenType.AT, "at", _lineNumber);
                        }
                    case "behind":
                        {
                            return new Token(TokenType.BEHIND, "behind", _lineNumber);
                        }
                    case "call":
                        {
                            return new Token(TokenType.CALL, "call", _lineNumber);
                        }
                    case "expression":
                        {
                            return new Token(TokenType.EXPRESSION, "expression", _lineNumber);
                        }
                    case "hide":
                        {
                            return new Token(TokenType.HIDE, "hide", _lineNumber);
                        }
                    case "if":
                        {
                            return new Token(TokenType.IF, "if", _lineNumber);
                        }
                    case "in":
                        {
                            return new Token(TokenType.IN, "in", _lineNumber);
                        }
                    case "image":
                        {
                            return new Token(TokenType.IMAGE, "image", _lineNumber);
                        }
                    case "init":
                        {
                            return new Token(TokenType.INIT, "init", _lineNumber);
                        }
                    case "jump":
                        {
                            return new Token(TokenType.JUMP, "jump", _lineNumber);
                        }
                    case "menu":
                        {
                            return new Token(TokenType.MENU, "menu", _lineNumber);
                        }
                    case "onlayer":
                        {
                            return new Token(TokenType.ON_LAYER, "onlayer", _lineNumber);
                        }
                    case "python":
                        {
                            return new Token(TokenType.PYTHON, "python", _lineNumber);
                        }
                    case "return":
                        {
                            return new Token(TokenType.RETURN, "return", _lineNumber);
                        }
                    case "scene":
                        {
                            return new Token(TokenType.SCENE, "scene", _lineNumber);
                        }
                    case "show":
                        {
                            return new Token(TokenType.SHOW, "show", _lineNumber);
                        }
                    case "with":
                        {
                            return new Token(TokenType.WITH, "with", _lineNumber);
                        }
                    case "while":
                        {
                            return new Token(TokenType.WHILE, "while", _lineNumber);
                        }
                    case "zorder":
                        {
                            return new Token(TokenType.ZORDER, "zorder", _lineNumber);
                        }
                    case "transform":
                        {
                            return new Token(TokenType.TRANSFORM, "transform", _lineNumber);
                        }
                    default:
                        {
                            return new Token(TokenType.IDENTIFIER, _identifier, _lineNumber);
                        }
                }
            }

            if (char.IsDigit((char)_c))
            {
                do
                {
                    _numberBuilder.Append((char)_c);
                    _c = _reader.Read();
                }
                while (char.IsDigit((char)_c) || _c == '.');

                _numberValue = double.Parse(_numberBuilder.ToString());
                if (_numberValue % 1 == 0)
                {
                    _numberBuilder.Clear();
                    return new Token(TokenType.INTEGER, _numberValue.ToString(), _lineNumber);
                }

                _numberBuilder.Clear();
                //This is dumb and redundant, change this later.
                return new Token(TokenType.NUMBER, _numberValue.ToString(), _lineNumber);
            }

            if (_c == '#')
            {
                var comment = "";
                do
                {
                    comment += Convert.ToChar(_c);
                    _c = _reader.Read();
                }
                while (_c != Eof && _c != '\n' && _c != '\r');

                if (_c != Eof)
                {
                    return new Token(TokenType.COMMENT, comment, _lineNumber);
                }
            }

            if (char.IsSymbol((char)_c))
            {
                if (_c == '$')
                {
                    return new Token(TokenType.PYTHON, "$", _lineNumber);
                }
                else if (_c == '+')
                {
                    return new Token(TokenType.PLUS, "+", _lineNumber);
                }
                else 
                {
                    return new Token(TokenType.OPERATOR, Convert.ToChar(_c).ToString(), _lineNumber);
                }
                
            }

            if (_c == '*')
            {
                return new Token(TokenType.MULT, Convert.ToChar(_c).ToString(), _lineNumber);
            }
            
            if (_c == '+')
            {
                return new Token(TokenType.PLUS, Convert.ToChar(_c).ToString(), _lineNumber);
            }
            
            if (_c == '-')
            {
                return new Token(TokenType.MINUS, Convert.ToChar(_c).ToString(), _lineNumber);
            }
            
            if (_c == '/')
            {
                return new Token(TokenType.DIV, Convert.ToChar(_c).ToString(), _lineNumber);
            }

            if (_c == '(') //|| c == ')')
            {
                var paren = _c;
                _c = ' ';
                return new Token(TokenType.LPAREN, Convert.ToChar(paren).ToString(), _lineNumber);
            }

            if (_c == ')')
            {
                var paren = _c;
                _c = ' ';
                return new Token(TokenType.RPAREN, Convert.ToChar(paren).ToString(), _lineNumber);
            }

            if (char.IsSeparator((char)_c) || _c == '/' || _c == ',')
            {
                return new Token(TokenType.SEPARATOR, Convert.ToChar(_c).ToString(), _lineNumber);
            }

            if (_c == ':')
            {
                return new Token(TokenType.COLON, Convert.ToChar(_c).ToString(), _lineNumber);
            }

            if (_c == '[')
            {
                return new Token(TokenType.LBRACKET, Convert.ToChar(_c).ToString(), _lineNumber);
            }

            if (_c == ']')
            {
                return new Token(TokenType.RBRACKET, Convert.ToChar(_c).ToString(), _lineNumber);
            }

            if (_c == '{')
            {
                return new Token(TokenType.LBRACE, Convert.ToChar(_c).ToString(), _lineNumber);
            }

            if (_c == '}')
            {
                return new Token(TokenType.RBRACE, Convert.ToChar(_c).ToString(), _lineNumber);
            }

            if (_c == '"')
            {
                _identifierBuilder.Append((char)_c);
                while ((_c = _reader.Read()) != '"')
                {
                    _identifierBuilder.Append((char)_c);
                }

                _identifierBuilder.Append((char)_c);
                _identifier = _identifierBuilder.ToString();
                _identifierBuilder.Clear();


                return new Token(TokenType.DOUBLE_QUOTE, _identifier, _lineNumber);
            }

            if (_c == '\'')
            {
                _identifierBuilder.Append((char)_c);
                while ((_c = _reader.Read()) != '\'')
                {
                    _identifierBuilder.Append((char)_c);
                }

                _identifierBuilder.Append((char)_c);
                _identifier = _identifierBuilder.ToString();
                _identifierBuilder.Clear();


                return new Token(TokenType.SINGLE_QUOTE, _identifier, _lineNumber);
            }


            if (_c == 0x2e)
            {
                return new Token(TokenType.PERIOD, ".", _lineNumber);
            }

            if (_c == '\n' || _c == '\r')
            {
                _lineNumber++;
                return GetNextTokenImpl();
            }

            if (_c == Eof)
            {
                return new Token(TokenType.EOF, "", _lineNumber);
            }
            
            return new Token(TokenType.UNKNOWN, _reader.Read().ToString(), _lineNumber);
        }

        public void CloseLexer()
        {
            _reader.Close();
            _reader.Dispose();
            _identifierBuilder.Clear();
            _numberBuilder.Clear();
        }

        //    private string[] operators = {
        //        "<>", "<<", "<=", "<", ">>", 
        //        ">=", ">", "!=", "==", "|",
        //        "^", "&", "+", "-", "**",
        //        "*", "//", "/", "%", "~"
        //    };

    }
}
