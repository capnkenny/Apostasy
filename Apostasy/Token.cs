namespace Apostasy
{
    public class Token
    {
        public TokenType Type { get; set; }
        public string Value { get; set; }
        public readonly int LineNumber;

        public Token(TokenType tType, string tValue, int line)
        {
            Type = tType;
            Value = tValue;
            LineNumber = line;
        }

    }
}