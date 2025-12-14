using System.Collections;
using LoxNetInterpreter;
using NUnit.Framework;
namespace LoxNetTests;

public class ScannerTests
{
    private static IEnumerable<TestCaseData> ScannerTestData
    {
        get
        {
            var tokens = new List<Token> { new (TokenType.EOF, string.Empty, 1)};
            yield return new TestCaseData("", tokens);

            yield return new TestCaseData("if", new List<Token> {
                new(TokenType.IF, "if", 1),
                new(TokenType.EOF, string.Empty, 1)
            });

            yield return new TestCaseData("print return\n for", new List<Token> {
                new(TokenType.PRINT, "print", 1),
                new(TokenType.RETURN, "return", 1),
                new(TokenType.FOR, "for", 2),
                new(TokenType.EOF, string.Empty, 2)
            });

            yield return new TestCaseData("class var\n for\nwhile", new List<Token> {
                new(TokenType.CLASS, "class", 1),
                new(TokenType.VAR, "var", 1),
                new(TokenType.FOR, "for", 2),
                new(TokenType.WHILE, "while", 3),
                new(TokenType.EOF, string.Empty, 3)
            });

            // test case for single line comments
            yield return new TestCaseData("class var\n// for\nwhile", new List<Token> {
                new(TokenType.CLASS, "class", 1),
                new(TokenType.VAR, "var", 1),
                new(TokenType.WHILE, "while", 3),
                new(TokenType.EOF, string.Empty, 3)
            });


        }
    }

    [Test]
    public void TestConstructor()
    {
        Scanner sc = new Scanner("");
        var tokens = sc.ScanTokens();
        Assert.AreEqual(1, tokens.Count);
    }

    [Test, TestCaseSource(nameof(ScannerTestData))]
    public void TestConstructorCases(string src, List<Token> tokens)
    {
        Scanner sc = new Scanner(src);
        var actualTokens = sc.ScanTokens();

        Assert.AreEqual(tokens, actualTokens);
    }
}