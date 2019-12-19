using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace HtmlSyntaxHighlighterDotNet
{
    internal class CSharpSyntaxWalker : Microsoft.CodeAnalysis.CSharp.CSharpSyntaxWalker
    {
        private static readonly string[] s_operatorKeywords = new string[]
        {
            "nameof"
        };

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
            "using",
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
            _buffer.Append($"<div class=\"{Css.RootClass}\">");
            Visit(node);
            _buffer.Append("</div>");
        }

        public override void VisitArgument(ArgumentSyntax node)
        {
            _stack.Push(SyntaxElement.Argument);
            base.VisitArgument(node);
            _stack.Pop();
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

        public override void VisitExplicitInterfaceSpecifier(ExplicitInterfaceSpecifierSyntax node)
        {
            _stack.Push(SyntaxElement.ExplicitInterfaceSpecifier);
            base.VisitExplicitInterfaceSpecifier(node);
            _stack.Pop();
        }

        public override void VisitFieldDeclaration(FieldDeclarationSyntax node)
        {
            _stack.Push(SyntaxElement.FieldDeclaration);
            base.VisitFieldDeclaration(node);
            _stack.Pop();
        }

        public override void VisitGenericName(GenericNameSyntax node)
        {
            _stack.Push(SyntaxElement.GenericName);
            base.VisitGenericName(node);
            _stack.Pop();
        }

        public override void VisitIdentifierName(IdentifierNameSyntax node)
        {
            _stack.Push(SyntaxElement.IdentifierName);
            base.VisitIdentifierName(node);
            _stack.Pop();
        }

        public override void VisitInterpolation(InterpolationSyntax node)
        {
            _stack.Push(SyntaxElement.Interpolation);
            base.VisitInterpolation(node);
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

        public override void VisitNamespaceDeclaration(NamespaceDeclarationSyntax node)
        {
            _stack.Push(SyntaxElement.NamespaceDeclaration);
            base.VisitNamespaceDeclaration(node);
            _stack.Pop();
        }

        public override void VisitObjectCreationExpression(ObjectCreationExpressionSyntax node)
        {
            _stack.Push(SyntaxElement.ObjectCreationExpression);
            base.VisitObjectCreationExpression(node);
            _stack.Pop();
        }

        public override void VisitParameter(ParameterSyntax node)
        {
            _stack.Push(SyntaxElement.Parameter);
            base.VisitParameter(node);
            _stack.Pop();
        }

        public override void VisitPropertyDeclaration(PropertyDeclarationSyntax node)
        {
            _stack.Push(SyntaxElement.PropertyDeclaration);
            base.VisitPropertyDeclaration(node);
            _stack.Pop();
        }

        public override void VisitUsingDirective(UsingDirectiveSyntax node)
        {
            _stack.Push(SyntaxElement.UsingDirective);
            base.VisitUsingDirective(node);
            _stack.Pop();
        }

        public override void VisitVariableDeclarator(VariableDeclaratorSyntax node)
        {
            _stack.Push(SyntaxElement.VariableDeclarator);
            base.VisitVariableDeclarator(node);
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
                _buffer.Append(Css.KeywordClass);

                if (s_statementKeywords.Contains(token.ValueText))
                {
                    _buffer.Append(Css.ClassSeperator);
                    _buffer.Append(Css.StatementClass);
                }              
            }
            else if (token.IsKind(SyntaxKind.IdentifierToken))
            {
                _buffer.Append(Css.IdentifierClass);

                if (_stack.Peek(SyntaxElement.MethodDeclaration))
                {
                    _buffer.Append(Css.ClassSeperator);
                    _buffer.Append(Css.MethodClass);
                }
                else if (_stack.Peek(SyntaxElement.ClassDeclaration, 1) ||
                         _stack.Peek(SyntaxElement.ExplicitInterfaceSpecifier, 1) ||
                         _stack.Peek(SyntaxElement.ObjectCreationExpression, 1) ||
                         _stack.Peek(SyntaxElement.Parameter, 1))
                {
                    _buffer.Append(Css.ClassSeperator);
                    _buffer.Append(Css.TypeClass);
                }
                else if (_stack.Contains(SyntaxElement.GenericName))
                {
                    _buffer.Append(Css.ClassSeperator);
                    _buffer.Append(Css.TypeClass);
                }
                else if (_stack.Peek(SyntaxElement.Invocation, 1))
                {
                    if (s_operatorKeywords.Contains(token.ValueText))
                    {
                        // Remove the identifier class and add the keyword class.
                        _buffer.Length -= Css.IdentifierClass.Length;
                        _buffer.Append(Css.KeywordClass);
                    }
                    else
                    {
                        _buffer.Append(Css.ClassSeperator);
                        _buffer.Append(Css.InvocationClass);
                    }
                }
                else if (_stack.Peek(SyntaxElement.MemberAccessExpression, 1))
                {
                    if (_stack.Peek(SyntaxElement.Invocation, 2) &&
                        !token.GetNextToken().IsKind(SyntaxKind.DotToken))
                    {
                        _buffer.Append(Css.ClassSeperator);
                        _buffer.Append(Css.InvocationClass);
                    }
                }
                else if (_stack.Peek(SyntaxElement.NamespaceDeclaration, 1))
                {
                    _buffer.Append(Css.ClassSeperator);
                    _buffer.Append(Css.NamespaceClass);
                }
                else if (_stack.Peek(SyntaxElement.UsingDirective, 1))
                {
                    _buffer.Append(Css.ClassSeperator);
                    _buffer.Append(Css.UsingDirectiveClass);
                }
                else if (token.ValueText == "var")
                {
                    _buffer.Append(Css.ClassSeperator);
                    _buffer.Append(Css.VarClass);
                }
            }
            else if (token.IsKind(SyntaxKind.NumericLiteralToken))
            {
                _buffer.Append(Css.NumericClass);
            }
            else if (token.IsKind(SyntaxKind.StringLiteralToken))
            {
                _buffer.Append(Css.StringClass);
            }
            else if (token.IsKind(SyntaxKind.InterpolatedStringStartToken) ||
                     token.IsKind(SyntaxKind.InterpolatedStringEndToken) ||
                     token.IsKind(SyntaxKind.InterpolatedStringTextToken))
            {
                _buffer.Append(Css.StringClass);
                _buffer.Append(Css.ClassSeperator);
                _buffer.Append(Css.InterpolatedClass);
            }
            else
            {
                _buffer.Append(Css.TokenClass);
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
                _buffer.Append("<span class=\"");
                _buffer.Append(Css.CommentClass);
                _buffer.Append("\">");
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
