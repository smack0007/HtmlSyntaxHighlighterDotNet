using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace HtmlSyntaxHighlighterDotNet
{
    public static class HtmlSyntaxHighlighter
    {
        public static string TransformCSharp(string source)
        {
            var tree = CSharpSyntaxTree.ParseText(source);

            var syntaxWalker = new CSharpSyntaxWalker();
            syntaxWalker.Highlight((CompilationUnitSyntax)tree.GetRoot());

            return syntaxWalker.ToString();
        }

        public static string GetCssStyles() => Css.GetStyles();
    }
}
