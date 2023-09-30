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
        Scanner sc = new Scanner("");
        var actualTokens = sc.ScanTokens();
        
        Assert.AreEqual(tokens, actualTokens);
    }
}