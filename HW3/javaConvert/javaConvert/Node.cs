using System;
using System.Collections.Generic;
using System.Text;

namespace javaConvert
{

  /// <summary>
  /// Creates a singly linked node class.
  /// </summary>
  /// <typeparam name="T">Generic data type</typeparam>
  class Node<T>
  {
    public T Data;
    public Node<T> Next;

    public Node(T Data, Node<T> Next)
    {
      this.Data = Data;
      this.Next = Next;
    }
  }
}
