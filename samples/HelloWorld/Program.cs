using System.IO;
using System.Reflection;
using HtmlSyntaxHighlighterDotNet;

namespace HelloWorld
{
    class Program
    {
        public static void Main()
        {
            var assemblyLocation = Assembly.GetEntryAssembly()?.Location ?? "";
            var assemblyDirectoryName = Path.GetDirectoryName(assemblyLocation) ?? "";
            var samplesPath = Path.Combine(assemblyDirectoryName, "samples");

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
