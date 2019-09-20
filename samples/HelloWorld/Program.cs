using System.IO;
using System.Reflection;
using HtmlSyntaxHighlighterDotNet;

namespace HelloWorld
{
    class Program
    {
        public static void Main(string[] args)
        {
            var samplesPath = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "samples");

            foreach (var file in Directory.EnumerateFiles(samplesPath, "*.cs", SearchOption.TopDirectoryOnly))
            {
                var source = File.ReadAllText(file);

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

                File.WriteAllText(Path.Combine(samplesPath, Path.GetFileNameWithoutExtension(file) + ".html"), html);
            }
        }
    }
}
