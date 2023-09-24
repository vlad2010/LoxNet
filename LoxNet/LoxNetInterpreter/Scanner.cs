using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static LoxNetInterpreter.Token;

namespace LoxNetInterpreter
{
    internal class Scanner
    {
        private readonly String source;
        private readonly List<Token> tokens = new List<Token>();

        private int start = 0;
        private int current = 0;
        private int line = 1;

        public Scanner(String source)
        {
            this.source = source;
        }

        List<Token> ScanTokens()
        {
            while(!IsAtEnd())
            {
                start = current;
                ScanTokens();
            }

            tokens.Add(new Token(TokenType.EOF, "", null, line) );
            return tokens;
        }

        private Boolean IsAtEnd()
        {
            return current >= source.Length;
        }
    }
}
