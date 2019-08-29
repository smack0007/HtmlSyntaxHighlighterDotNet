using System;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace RoslynHtmlSyntaxHighlighter
{
    public class HtmlSyntaxHighlighter
    {
        public static string TransformCSharp(string source)
        {
            var tree = CSharpSyntaxTree.ParseText(source);

            var syntaxWalker = new CSharpSyntaxWalker();
            syntaxWalker.Highlight((CompilationUnitSyntax)tree.GetRoot());

            return syntaxWalker.ToString();
        }

        private class CSharpSyntaxWalker : Microsoft.CodeAnalysis.CSharp.CSharpSyntaxWalker
        {
            public StringBuilder _buffer;

            public CSharpSyntaxWalker(int initialBufferCapacity = 1024)
                : base(SyntaxWalkerDepth.Token)
            {
                _buffer = new StringBuilder(initialBufferCapacity);
            }

            public override string ToString() => _buffer.ToString();

            public void Highlight(SyntaxNode node)
            {
                _buffer.Append("<span class=\"RoslynHtmlSyntaxHighlighter\">");
                Visit(node);
                _buffer.Append("</span>");
            }

            public override void VisitToken(SyntaxToken token)
            {
                if (token.HasLeadingTrivia)
                {
                    foreach (var trivia in token.LeadingTrivia)
                        _buffer.Append(trivia);
                }

                _buffer.Append($"<span class=\"");

                if (token.IsKeyword())
                {
                    _buffer.Append("keyword keyword-");
                    _buffer.Append(token);
                }
                else if (token.IsKind(SyntaxKind.IdentifierToken))
                {
                    _buffer.Append("identifier");

                    if (token.Parent is ClassDeclarationSyntax)
                    {
                        _buffer.Append(" identifier-class");
                    }
                    else if (token.Parent is MethodDeclarationSyntax)
                    {
                        _buffer.Append(" identifier-method");
                    }
                }
                else
                {
                    _buffer.Append("token");
                }

                
                _buffer.Append("\">");
                _buffer.Append(token);
                _buffer.Append("</span>");

                if (token.HasTrailingTrivia)
                {
                    foreach (var trivia in token.TrailingTrivia)
                        _buffer.Append(trivia);
                }
            }
        }
    }
}
