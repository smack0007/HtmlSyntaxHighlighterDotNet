using System.IO;
using System.Reflection;
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

        public static Stream GetCssStream()
        {
            var ns = typeof(HtmlSyntaxHighlighter).Namespace;
            return Assembly.GetExecutingAssembly().GetManifestResourceStream($"{ns}.{ns}.css");
        }

        public static string GetCssString()
        {
            var stream = GetCssStream();

            using (var sr = new StreamReader(stream))
                return sr.ReadToEnd();
        }
    }
}
