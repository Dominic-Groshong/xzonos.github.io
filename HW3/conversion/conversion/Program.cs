using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace javaConvert
{

  class Program
  {
    static LinkedList<String> GenerateBinaryRepresentationList(int n)
    {
      // Create an empty queue of strings with which to preform the traversal
      LinkedQueue<StringBuilder> q = new LinkedQueue<StringBuilder>();

      // List for returning the binary values
      LinkedList<String> Output = new LinkedList<string>();


      if(n < 1)
      {
        // Binary representation of negative values is not supported
        // return an empty list.
        return Output;
      }

      // Enqueue the first binary number. Use a dynamic string to avoid string concat
      q.Push(new StringBuilder("1"));

      // BFS

      while(n-- > 0)
      {
        // Print the front of queue
        StringBuilder sb = q.Pop();
        Output.AddLast(sb.ToString());

        // Make a copy
        StringBuilder sbc = new StringBuilder(sb.ToString());

        // Left child
        sb.Append('0');
        q.Push(sb);
        // Right child
        sbc.Append('1');
        q.Push(sbc);
      }
      return Output;
    }


    static void Main(string[] args)
    {
      int n = 10;

      if(args.Length < 1)
      {
        Console.WriteLine("Please invoke with the max value to print binary up to, like this:");
        Console.WriteLine("\tjava Main 12");
        return;
      }

      try
      {
        n = int.Parse(args[0]);
      }

      catch(FormatException e)
      {
        Console.WriteLine("I'm sorry, I can't understand the number: " + args[0]);
        return;
      }

      LinkedList<String> output = GenerateBinaryRepresentationList(n);

      // Print it right justified. Longest string is the last one.
      // Print enough spaces to move it over the correct distane

      int maxLength = output.Count();

      foreach(String s in output)
      {
        for(int i = 0; i < maxLength - s.Length; ++i)
        {
          Console.Write(" ");
        }

        Console.WriteLine(s);
      }
      
    }
  }
}
