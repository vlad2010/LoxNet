using System;
using System.Collections.Generic;
using System.ComponentModel;
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
            while (!IsAtEnd())
            {
                start = current;
                ScanToken();
            }

            tokens.Add(new Token(TokenType.EOF, "", null, line));
            return tokens;
        }

        private void ScanToken()
        {
            char c = advance();

            switch (c)
            {
                case '(': addToken(TokenType.LEFT_PAREN); break;
                case ')': addToken(TokenType.RIGHT_PAREN); break;
                case '{': addToken(TokenType.LEFT_BRACE); break;
                case '}': addToken(TokenType.RIGHT_BRACE); break;
                case ',': addToken(TokenType.COMMA); break;
                case '.': addToken(TokenType.DOT); break;
                case '-': addToken(TokenType.MINUS); break;
                case '+': addToken(TokenType.PLUS); break;
                case ';': addToken(TokenType.SEMICOLON); break;
                case '*': addToken(TokenType.STAR); break;

                case '!':
                    addToken(match('=') ? TokenType.BANG_EQUAL : TokenType.BANG);
                    break;
                case '=':
                    addToken(match('=') ? TokenType.EQUAL_EQUAL : TokenType.EQUAL);
                    break;
                case '<':
                    addToken(match('=') ? TokenType.LESS_EQUAL : TokenType.LESS);
                    break;
                case '>':
                    addToken(match('=') ? TokenType.GREATER_EQUAL : TokenType.GREATER);
                    break;

                case '/':
                    if (match('/'))
                    {
                        // skip all commented text
                        while (peek() != '\n' && !IsAtEnd()) advance();
                    } else
                    {
                        addToken(TokenType.SLASH);
                    }
                    break;

                case '"':

                case ' ':
                case '\r':
                case '\t':
                    break;

                case '\n':
                    line++;
                    break;

                default:
                    ErrorReporting.Error(line, "Unexpected character");
                    break;
            }

            return;
        }
        private Boolean IsAtEnd()
        {
            return current >= source.Length;
        }

        private char advance()
        {
            return source[current++];
        }

        private void addToken(TokenType type)
        {
            addToken(type, null);
        }

        private void addToken(TokenType type, Object literal)
        {
            String text = source.Substring(start, current - start);
            tokens.Add(new Token(type, text, literal, line));
        }

        private Boolean match(char expected)
        {
            if (IsAtEnd())
                return false;

            if (source[current] != expected)
                return false;

            current++;
            return true;
        }


        // check symbol without advance, lookahead
        private char peek()
        {
            if (IsAtEnd()) return '\0';
            return source[current];
        }

        private void consumeString()
        {
            while(peek() != '"' && !IsAtEnd())
            {
                if(peek() == '\n')
                {
                    line++;
                }
                advance();
            }

            if(IsAtEnd())
            {
                ErrorReporting.Error(line, "Unterminated string.");
                return;
            }

            // closing "
            advance();

            // trim surrounding quotes
            String value = source.Substring(start + 1 , current - start - 1);
            addToken(TokenType.STRING, value);
        }

    }
}
