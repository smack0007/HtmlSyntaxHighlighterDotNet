using System;
using System.Collections.Generic;
using System.Text;

namespace HelloWorld.samples
{
    public static class Progam
    {
        public const string MyVariable1 = nameof(MyVariable1);

        public static void Main()
        {
            var type1 = typeof(Program);
            var default1 = default(int);
        }
    }
}
