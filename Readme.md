[![The MIT License](https://img.shields.io/badge/license-MIT-orange.svg?style=flat-square)](http://opensource.org/licenses/MIT)

# HtmlSyntaxHighlightDotNet

Produces HTML for showing syntax highlighted code. Uses Roslyn but currently only works for C# code.

## Usage

Easiet way is to use the NuGet package:

```
Install-Package HtmlSyntaxHighlightDotNet
```

Use the `TransformCSharp(string source)` method to generate the HTML:

```c#
var html = HtmlSyntaxHighlighter.TransformCSharp(source);
```

Then use `GetCssStyles()` to get the default CSS for the syntax highlighting. You can provide your own instead if you like.

```c#
var css = HtmlSyntaxHighlighter.GetCssStyles();
```
