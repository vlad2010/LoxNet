namespace LoxNetInterpreter
{
    enum TokenType
    {
        // single char tokens
        LEFT_PAREN, RIGHT_PAREN, LEFT_BRACE, RIGHT_BRACE, COMMA, DOT, MINUS, PLUS,
        SEMICOLON, SLASH, STAR,

        // one or two char tokens
        BANG, BANG_EQUAL, EQUAL, EQUAL_EQUAL,
        GREATER, GREATER_EQUAL, LESS, LESS_EQUAL,

        // literals
        IDENTIFIER, STRING, NUMBER,

        // keywords
        AND, CLASS, ELSE, FALSE, FUN, FOR, IF, NIL, OR, PRINT, RETURN, SUPER,
        THIS, TRUE, VAR, WHILE,

        EOF
    }


    internal class Token
    {
        private readonly TokenType type;
        private readonly String lexeme;
        private readonly Object literal;
        private readonly int line;

        public Token(TokenType type, String lexeme, Object literal, int line)
        {
            this.type = type;
            this.lexeme = lexeme;
            this.literal = literal;
            this.line = line;
        }

        public override string ToString()
        {
            return type + " " + lexeme + " " + literal;
        }

    }
}
