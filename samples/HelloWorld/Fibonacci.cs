// This file will not be compiled.

using System;
using System.Collections.Generic;
using System.Linq;

/*
 * This file will not be compiled.
 */

public class Program
{
    public static void Main()
    {
        foreach (var i in Fibonacci().Take(20))
        {
            System.Console.WriteLine(i);
        }

        Console.WriteLine("Done.");
    }

    private static IEnumerable<int> Fibonacci(/* void */)
    {
        int current = 1;
        int next = 1;

        while (true)
        {
            yield return current;
            next = current + (current = next);
        }
    }
}
