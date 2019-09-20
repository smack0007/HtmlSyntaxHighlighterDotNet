using System;
using System.Collections.Generic;

namespace Defer
{
    class DeferredActions : IDisposable
    {
        private List<Action> _actions = new List<Action>();

        public void Push(Action action) => _actions.Add(action);

        void IDisposable.Dispose()
        {
            foreach (var action in _actions)
                action();

            _actions.Clear();
        }
    }

    class Program
    {
        public static void Main(string[] args)
        {
            using (var deferredActions = new DeferredActions())
            {
                deferredActions.Push(() => Console.WriteLine("Deferred Action 1"));
                Console.WriteLine("Doing work...");
                deferredActions.Push(() => Console.WriteLine("Deferred Action 2"));
                Console.WriteLine("Doing more work...");
                deferredActions.Push(() => Console.WriteLine("Deferred Action 3"));
            }
        }
    }
}
