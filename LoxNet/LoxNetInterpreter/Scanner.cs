using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices.JavaScript;
using System.Text;
using System.Threading.Tasks;

using static LoxNetInterpreter.Token;

namespace LoxNetInterpreter
{
    public class Scanner
    {
        private readonly String source;
        private readonly List<Token> tokens = new List<Token>();
        private readonly Dictionary<String, TokenType> keywords;

        private int start = 0;
        private int current = 0;
        private int line = 1;

        public Scanner(String source)
        {
            this.source = source;
            keywords = new Dictionary<string, TokenType>()
            {
                {"and", TokenType.AND},
                {"class", TokenType.CLASS},
                {"else", TokenType.ELSE},
                {"false", TokenType.FALSE},
                {"for", TokenType.FOR},
                {"fun", TokenType.FUN},
                {"if", TokenType.IF},
                {"nil", TokenType.NIL},
                {"or", TokenType.OR},
                {"print", TokenType.PRINT},
                {"return", TokenType.RETURN},
                {"super", TokenType.SUPER},
                {"this", TokenType.THIS},
                {"true", TokenType.TRUE},
                {"var", TokenType.VAR},
                {"while", TokenType.WHILE},
            };
        }

        public List<Token> ScanTokens()
        {
            while (!IsAtEnd())
            {
                start = current;
                ScanToken();
            }

            tokens.Add(new Token(TokenType.EOF, "", line));
            return tokens;
        }

        private void ScanToken()
        {
            char c = Advance();

            switch (c)
            {
                case '(': AddToken(TokenType.LEFT_PAREN); break;
                case ')': AddToken(TokenType.RIGHT_PAREN); break;
                case '{': AddToken(TokenType.LEFT_BRACE); break;
                case '}': AddToken(TokenType.RIGHT_BRACE); break;
                case ',': AddToken(TokenType.COMMA); break;
                case '.': AddToken(TokenType.DOT); break;
                case '-': AddToken(TokenType.MINUS); break;
                case '+': AddToken(TokenType.PLUS); break;
                case ';': AddToken(TokenType.SEMICOLON); break;
                case '*': AddToken(TokenType.STAR); break;

                case '!':
                    AddToken(Match('=') ? TokenType.BANG_EQUAL : TokenType.BANG);
                    break;
                case '=':
                    AddToken(Match('=') ? TokenType.EQUAL_EQUAL : TokenType.EQUAL);
                    break;
                case '<':
                    AddToken(Match('=') ? TokenType.LESS_EQUAL : TokenType.LESS);
                    break;
                case '>':
                    AddToken(Match('=') ? TokenType.GREATER_EQUAL : TokenType.GREATER);
                    break;

                case '/':
                    if (Match('/'))
                    {
                        // skip all commented text
                        while (Peek() != '\n' && !IsAtEnd()) Advance();
                    } else
                    {
                        AddToken(TokenType.SLASH);
                    }
                    break;

                case '"':
                    ConsumeString();
                    break;

                case ' ':
                case '\r':
                case '\t':
                    break;

                case '\n':
                    line++;
                    break;

                default:
                    if (Char.IsDigit(c))
                    {
                        Number();
                    }
                    else if (IsLoxAlphaChar(c))
                    {
                        Identifier();
                    }
                    else
                    {
                        ErrorReporting.Error(line, "Unexpected character");
                    }
                    break;
            }

            return;
        }
        private Boolean IsAtEnd()
        {
            return current >= source.Length;
        }

        private char Advance()
        {
            return source[current++];
        }

        private void AddToken(TokenType type)
        {
            String text = source.Substring(start, current - start);
            tokens.Add(new Token(type, text, line));
        }

        private void AddToken(TokenType type, Object literal)
        {
            String text = source.Substring(start, current - start);
            tokens.Add(new Token(type, text, literal, line));
        }

        private Boolean Match(char expected)
        {
            if (IsAtEnd())
                return false;

            if (source[current] != expected)
                return false;

            current++;
            return true;
        }


        // check symbol without advance, lookahead
        private char Peek()
        {
            if (IsAtEnd()) return '\0';
            return source[current];
        }

        private void ConsumeString()
        {
            while(Peek() != '"' && !IsAtEnd())
            {
                if(Peek() == '\n')
                {
                    line++;
                }
                Advance();
            }

            if(IsAtEnd())
            {
                ErrorReporting.Error(line, "Unterminated string.");
                return;
            }

            // closing "
            Advance();

            // trim surrounding quotes
            String value = source.Substring(start + 1 , current - start - 1);
            AddToken(TokenType.STRING, value);
        }

        private void Number()
        {
            // advance all digits
            while (Char.IsDigit(Peek()))
            {
                Advance();
            }
            
            // look for fraction part
            if (Peek() == '.' && Char.IsDigit(PeekNext()))
            {
                // consume '.'
                Advance();

                // consume fraction part
                while (Char.IsDigit(Peek())) Advance();
            }

            string doubleString = source.Substring(start, current - start);
            AddToken(TokenType.NUMBER, Double.Parse(doubleString));
        }

        private char PeekNext()
        {
            if (current + 1 >= source.Length) 
                return '\0';

            return source[current + 1];
        }

        private void Identifier()
        {
            while (IsLoxAlphaNumeric(Peek())) Advance();

            String text = source.Substring(start, current - start);
            TokenType type = TokenType.IDENTIFIER;
            
            if (keywords.TryGetValue(text, out var keyword))
            {
                type = keyword;
            }
            
            AddToken(type);
        }

        private Boolean IsLoxAlphaChar(char c)
        {
            return char.IsAsciiLetter(c) || c == '_';
        }

        private Boolean IsLoxAlphaNumeric(char c)
        {
            return IsLoxAlphaChar(c) || char.IsDigit(c);
        }
    }
}
