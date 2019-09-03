using System;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace RoslynHtmlSyntaxHighlighter
{
    public partial class HtmlSyntaxHighlighter
    {
        public static string TransformCSharp(string source)
        {
            var tree = CSharpSyntaxTree.ParseText(source);

            var syntaxWalker = new CSharpSyntaxWalker();
            syntaxWalker.Highlight((CompilationUnitSyntax)tree.GetRoot());

            return syntaxWalker.ToString();
        }
    }
}
