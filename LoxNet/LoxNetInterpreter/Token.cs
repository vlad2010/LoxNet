namespace LoxNetInterpreter
{
    public enum TokenType
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

    public class Token
    {
        private readonly TokenType m_type;

        // m_lexeme is text representation of token
        private readonly String m_lexeme;
        private readonly Object? m_literal;
        private readonly int m_line;

        public Token(TokenType mType, String mLexeme, Object mLiteral, int mLine)
        {
            this.m_type = mType;
            this.m_lexeme = mLexeme;
            this.m_literal = mLiteral;
            this.m_line = mLine;
        }

        public Token(TokenType mType, String mLexeme, int mLine)
        {
            this.m_type = mType;
            this.m_lexeme = mLexeme;
            this.m_line = mLine;
        }

        public override string ToString()
        {
            return m_type + " " + m_lexeme + " " + m_literal;
        }

        public override bool Equals(Object obj)
        {
            var t = obj as Token;
            if (t == null)
            {
                return false;
            }

            return t.m_line == m_line && t.m_type == m_type && t.m_lexeme == m_lexeme && t.m_literal == m_literal;
        }

        public override int GetHashCode()
        {
            return new { line = m_line, type = m_type, lexeme = m_lexeme, literal = m_literal }.GetHashCode();
        }

    }
}
