using System;
using System.IO;
using RoslynHtmlSyntaxHighlighter;

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
        <link rel=""stylesheet"" type=""text/css"" href=""RoslynHtmlSyntaxHighlighter.css"">
        <style>
            code {{
                display: block;
                background-color: #1E1E1E;
                color: white;
            }}
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
