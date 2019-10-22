using System;
using System.Collections.Generic;
using System.Text;

namespace HtmlSyntaxHighlighterDotNet
{
    internal static class Css
    {
        public const string ClassSeperator = " ";

        public const string ClassOperator = ".";

        public const string RootClass = "HtmlSyntaxHighlighterDotNet";

        public const string CommentClass = "co";

        public const string IdentifierClass = "id";

        public const string InterpolatedClass = "int";

        public const string InvocationClass = "inv";

        public const string KeywordClass = "k";

        public const string MethodClass = "m";

        public const string NamespaceClass = "ns";

        public const string NumericClass = "n";

        public const string StatementClass = "sta";

        public const string StringClass = "str";

        public const string TokenClass = "t";

        public const string TypeClass = "ty";

        public const string UsingDirectiveClass = "u";

        public const string VarClass = "v";

        private static readonly Dictionary<string, Dictionary<string, string>> s_styles = new Dictionary<string, Dictionary<string, string>>()
        {
            {
                "",
                new Dictionary<string, string>()
                {
                    { "background-color", "rgb(48, 48, 48)" },
                    { "color", "white" },
                    { "font-family", "consolas, monospace" },
                    { "font-size", "18px" },
                    { "font-weight", "normal" },
                    { "overflow", "auto" }
                }
            },
            {
                CommentClass,
                new Dictionary<string, string>()
                {
                    { "color", "#57A64A" }
                }
            },
            {
                KeywordClass,
                new Dictionary<string, string>()
                {
                    { "color", "#569CD6" }
                }
            },
            {
                KeywordClass + ClassOperator + StatementClass,
                new Dictionary<string, string>()
                {
                    { "color", "#C586C0" }
                }
            },
            {
                IdentifierClass,
                new Dictionary<string, string>()
                {
                    { "color", "#9CDCFE" }
                }
            },
            {
                IdentifierClass + ClassOperator + TypeClass,
                new Dictionary<string, string>()
                {
                    { "color", "#4EC9B0" }
                }
            },
            {
                IdentifierClass + ClassOperator + InvocationClass,
                new Dictionary<string, string>()
                {
                    { "color", "#DCDCAA" }
                }
            },
            {
                IdentifierClass + ClassOperator + MethodClass,
                new Dictionary<string, string>()
                {
                    { "color", "#DCDCAA" }
                }
            },
            {
                IdentifierClass + ClassOperator + NamespaceClass,
                new Dictionary<string, string>()
                {
                    { "color", "#4EC9B0" }
                }
            },
            {
                IdentifierClass + ClassOperator + UsingDirectiveClass,
                new Dictionary<string, string>()
                {
                    { "color", "#4EC9B0" }
                }
            },
            {
                IdentifierClass + ClassOperator + VarClass,
                new Dictionary<string, string>()
                {
                    { "color", "#569CD6" }
                }
            },
            {
                NumericClass,
                new Dictionary<string, string>()
                {
                    { "color", "#B8D7A3" }
                }
            },
            {
                StringClass,
                new Dictionary<string, string>()
                {
                    { "color", "#D69D85" }
                }
            },
        };

        public static string GetStyles()
        {
            var sb = new StringBuilder(1024);

            foreach (var style in s_styles)
            {
                sb.Append(ClassOperator);
                sb.Append(RootClass);
                
                if (style.Key.Length > 0)
                {
                    sb.Append(ClassSeperator);
                    sb.Append(ClassOperator);
                    sb.Append(style.Key);
                }

                sb.Append(" {");
                sb.AppendLine();

                foreach (var prop in style.Value)
                {
                    sb.Append("\t");
                    sb.Append(prop.Key);
                    sb.Append(": ");
                    sb.Append(prop.Value);
                    sb.AppendLine(";");
                }

                sb.Append("}");

                sb.AppendLine();
            }

            return sb.ToString();
        }
    }
}
