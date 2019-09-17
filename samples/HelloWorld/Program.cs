using System.IO;
using HtmlSyntaxHighlighterDotNet;

namespace HelloWorld
{
    class Program
    {
        public static void Main(string[] args)
        {
            var source = File.ReadAllText("Fibonacci.cs");

            var syntaxHtml = HtmlSyntaxHighlighter.TransformCSharp(source);

            var html =
$@"<!DOCTYPE html>
<html>
    <head>
        <style>
            code {{
                display: block;
                background-color: #1E1E1E;
                color: white;
            }}

{ HtmlSyntaxHighlighter.GetCssStyles() }
        </style>
    </head>
    <body>
        <code>
            <pre>{syntaxHtml}</pre>  
        </code>
    </body>
</html>";

            File.WriteAllText("index.html", html);
        }
    }
}
