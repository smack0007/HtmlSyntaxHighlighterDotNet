using System.Collections.Generic;

namespace HtmlSyntaxHighlighterDotNet
{
    internal class SyntaxElementStack
    {
        private readonly List<SyntaxElement> _stack;

        public SyntaxElementStack()
        {
            _stack = new List<SyntaxElement>(16);
        }

        public void Push(SyntaxElement element) => _stack.Add(element);

        public void Pop() => _stack.RemoveAt(_stack.Count - 1);

        public bool Peek(SyntaxElement element, int index = 0)
        {
            return _stack.Count > index && _stack[_stack.Count - index - 1] == element;
        }
    }
}
