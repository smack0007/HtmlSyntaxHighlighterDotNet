using System;
using System.Collections.Generic;
using System.Text;

namespace HtmlSyntaxHighlighterDotNet
{
    internal static class Css
    {
        public const string ClassSeperator = " ";

        public const string ClassOperator = ".";

        public const string ClassExtender = "-";

        public const string RootClass = "HtmlSyntaxHighlighterDotNet";

        public const string ClassClass = "cl";

        public const string CommentClass = "co";

        public const string GenericClass = "g";

        public const string IdentifierClass = "id";

        public const string InterpolatedClass = "int";

        public const string InvocationClass = "inv";

        public const string KeywordClass = "k";

        public const string MemberAccessClass = "ma";

        public const string MethodClass = "m";

        public const string NumericClass = "n";

        public const string StatementClass = "sta";

        public const string StringClass = "str";

        public const string TokenClass = "t";

        public const string UsingDirectiveClass = "u";

        public const string VarClass = "v";

        private static readonly Dictionary<string, Dictionary<string, string>> s_styles = new Dictionary<string, Dictionary<string, string>>()
        {
            {
                "",
                new Dictionary<string, string>()
                {
                    { "font-family", "Consolas" },
                    { "font-size", "16px" }
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
                    { "color", "#D8A0DF" }
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
                IdentifierClass + ClassOperator + ClassClass,
                new Dictionary<string, string>()
                {
                    { "color", "#84D5A1" }
                }
            },
            {
                IdentifierClass + ClassOperator + GenericClass,
                new Dictionary<string, string>()
                {
                    { "color", "#B8D7A3" }
                }
            },
            {
                IdentifierClass + ClassOperator + MemberAccessClass,
                new Dictionary<string, string>()
                {
                    { "color", "#84D5A1" }
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
                IdentifierClass + ClassOperator + UsingDirectiveClass,
                new Dictionary<string, string>()
                {
                    { "color", "#DCDCDC" }
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
