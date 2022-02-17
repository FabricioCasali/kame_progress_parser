using System;
using System.IO;

using Antlr4.Runtime;

using KameProgressParser;

using Xunit;

namespace KameTests
{

    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var cc = File.ReadAllText(@"C:\Users\fabri\Downloads\shdmed_1.df");
            var expression = cc;
            cc = null;
            var inputStream = new AntlrInputStream(expression);
            var lexer = new ABLProgressLexer(inputStream);
            var tokenStream = new CommonTokenStream(lexer);
            var parser = new ABLProgressParser(tokenStream);
            //parser.ErrorHandler = new ErrorHandling();
            var visitor = new ABLProgressCommandVisitor(tokenStream);
            parser.BuildParseTree = true;
            var s = parser.script();
            var script = visitor.Visit(s);
            Console.WriteLine(script);
        }
    }
}