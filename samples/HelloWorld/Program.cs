using System;
using System.IO;
using RoslynHtmlSyntaxHighlighter;

namespace HelloWorld
{
    class Program
    {
        public static void Main(string[] args)
        {
            var source =
@"using System;
using System.Collections.Generic;
using System.Linq;

public class Program
{
  public static void Main()
  {
    foreach (var i in Fibonacci().Take(20))
    {
      Console.WriteLine(i);
    }
  }

  private static IEnumerable<int> Fibonacci()
  {
    int current = 1, next = 1;

    while (true) 
    {
      yield return current;
      next = current + (current = next);
    }
  }
}
";

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
