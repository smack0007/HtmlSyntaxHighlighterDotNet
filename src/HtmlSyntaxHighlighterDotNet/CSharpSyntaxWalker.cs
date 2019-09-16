using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace RoslynHtmlSyntaxHighlighter
{
    internal class CSharpSyntaxWalker : Microsoft.CodeAnalysis.CSharp.CSharpSyntaxWalker
    {
        private static readonly string[] s_statementKeywords = new string[]
        {
            "do",
            "else",
            "foreach",
            "if",
            "in",
            "return",
            "switch",
            "yield",
            "while",
        };

        public StringBuilder _buffer;
        public SyntaxElementStack _stack;

        public CSharpSyntaxWalker(int initialBufferCapacity = 1024)
            : base(SyntaxWalkerDepth.Token)
        {
            _buffer = new StringBuilder(initialBufferCapacity);
            _stack = new SyntaxElementStack();
        }

        public override string ToString() => _buffer.ToString();

        public void Highlight(SyntaxNode node)
        {
            _buffer.Append("<span class=\"HtmlSyntaxHighlighterDotNet\">");
            Visit(node);
            _buffer.Append("</span>");
        }

        public override void VisitBlock(BlockSyntax node)
        {
            _stack.Push(SyntaxElement.Block);
            base.VisitBlock(node);
            _stack.Pop();
        }

        public override void VisitClassDeclaration(ClassDeclarationSyntax node)
        {
            _stack.Push(SyntaxElement.ClassDeclaration);
            base.VisitClassDeclaration(node);
            _stack.Pop();
        }

        public override void VisitForEachStatement(ForEachStatementSyntax node)
        {
            base.VisitForEachStatement(node);
        }

        public override void VisitGenericName(GenericNameSyntax node)
        {
            _stack.Push(SyntaxElement.GenericName);
            base.VisitGenericName(node);
            _stack.Pop();
        }

        public override void VisitInvocationExpression(InvocationExpressionSyntax node)
        {
            _stack.Push(SyntaxElement.Invocation);
            base.VisitInvocationExpression(node);
            _stack.Pop();
        }

        public override void VisitMethodDeclaration(MethodDeclarationSyntax node)
        {
            _stack.Push(SyntaxElement.MethodDeclaration);
            base.VisitMethodDeclaration(node);
            _stack.Pop();
        }

        public override void VisitMemberAccessExpression(MemberAccessExpressionSyntax node)
        {
            _stack.Push(SyntaxElement.MemberAccessExpression);
            base.VisitMemberAccessExpression(node);
            _stack.Pop();
        }

        public override void VisitUsingDirective(UsingDirectiveSyntax node)
        {
            _stack.Push(SyntaxElement.UsingDirective);
            base.VisitUsingDirective(node);
            _stack.Pop();
        }

        public override void VisitVariableDeclaration(VariableDeclarationSyntax node)
        {
            _stack.Push(SyntaxElement.VariableDeclaration);
            base.VisitVariableDeclaration(node);
            _stack.Pop();
        }

        public override void VisitToken(SyntaxToken token)
        {
            if (token.HasLeadingTrivia)
            {
                foreach (var trivia in token.LeadingTrivia)
                    VisitTrivia(trivia);
            }

            _buffer.Append($"<span class=\"");

            if (token.IsKeyword() || token.IsContextualKeyword())
            {
                _buffer.Append("keyword");

                if (s_statementKeywords.Contains(token.ValueText))
                {
                    _buffer.Append(" keyword-statement");
                }

                _buffer.Append(" keyword-");
                _buffer.Append(token);                
            }
            else if (token.IsKind(SyntaxKind.IdentifierToken))
            {
                _buffer.Append("identifier");

                if (_stack.Peek(SyntaxElement.ClassDeclaration))
                {
                    _buffer.Append(" identifier-class");
                }
                else if (_stack.Peek(SyntaxElement.GenericName))
                {
                    _buffer.Append(" identifier-generic");
                }
                else if (_stack.Peek(SyntaxElement.Invocation))
                {
                    _buffer.Append(" identifier-invocation");
                }
                else if (_stack.Peek(SyntaxElement.MethodDeclaration))
                {
                    _buffer.Append(" identifier-method");
                }
                else if (_stack.Peek(SyntaxElement.MemberAccessExpression))
                {
                    if (_stack.Peek(SyntaxElement.Invocation, 1))
                    {
                        _buffer.Append(" identifier-invocation");
                    }
                    else
                    {
                        _buffer.Append(" identifier-member-access");
                    }
                }
                else if (_stack.Peek(SyntaxElement.UsingDirective))
                {
                    _buffer.Append(" identifier-using-directive");
                }
                else if (_stack.Peek(SyntaxElement.VariableDeclaration))
                {
                    _buffer.Append(" identifier-variable");
                }
                else if (token.ValueText == "var")
                {
                    _buffer.Append(" identifier-var");
                }
            }
            else if (token.IsKind(SyntaxKind.NumericLiteralToken))
            {
                _buffer.Append("numeric-literal");
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
                    VisitTrivia(trivia);
            }
        }

        public override void VisitTrivia(SyntaxTrivia trivia)
        {
            bool closeSpan = false;

            if (trivia.IsKind(SyntaxKind.SingleLineCommentTrivia) ||
                trivia.IsKind(SyntaxKind.MultiLineCommentTrivia))
            {
                _buffer.Append("<span class=\"comment\">");
                closeSpan = true;
            }

            _buffer.Append(trivia);

            if (closeSpan)
            {
                _buffer.Append("</span>");
            }
        }
    }
}
