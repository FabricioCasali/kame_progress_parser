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
            var gen = new OracleGenerator();
            var r = gen.Generate(script);
            File.WriteAllText("c:/temp/scriptConverter.sql", r);
        }

        [Fact]
        public void Test2()
        {
            var gen = new OracleGenerator();
            var r = gen.ToOracleValidName("dz-dmed-registro");
            var x = gen.ToOracleValidName("dz-dmed-registro-nome-longo");
            var y = gen.ToOracleValidName("dz-dmed-registro-nome-muito-muito-muito-longo");
        }
    }
}

