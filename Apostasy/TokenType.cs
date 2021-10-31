using System.Dynamic;

namespace Apostasy
{
    public enum TokenType : int
    { 
        EOF = -1,
        NUMBER = -2,
        COMMENT = -3,
        IDENTIFIER = -4,
        AS = -6,
        AT = -7,
        BEHIND = -8,
        CALL = -9,
        EXPRESSION = -10,
        HIDE = -11,
        IF = -12,
        IN = -13,
        IMAGE = -14,
        INIT = -15,
        JUMP = -16,
        MENU = -17,
        ON_LAYER = -18,
        PYTHON = -19,
        RETURN = -20,
        SCENE = -21,
        SHOW = -22,
        WITH = -23,
        WHILE = -24,
        ZORDER = -25,
        TRANSFORM = -26,
        UNKNOWN = -99,
        OPERATOR = -30,
        LPAREN = -31,
        RPAREN = -32,
        SINGLE_QUOTE = -33,
        DOUBLE_QUOTE = -34,
        SEPARATOR = -35,
        PERIOD = -36,
        COLON = -37,
        LBRACKET = -38,
        RBRACKET = -39,
        LBRACE = -40,
        RBRACE = -41,
        PLUS = -42,
        MINUS = -43,
        MULT = -44,
        DIV = -45,
        INTEGER = -46
    }

    //private string[] operators = {
    //        "<>", "<<", "<=", "<", ">>",
    //        ">=", ">", "!=", "==", "|",
    //        "^", "&", "+", "-", "**",
    //        "*", "//", "/", "%", "~"
    //    };
    
}