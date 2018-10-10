using System;
using System.Collections.Generic;
using System.Text;

namespace javaConvert
{
  class LinkedQueue<T> : IQueue<T>
  {

    private Node<T> front;
    private Node<T> rear;

    public LinkedQueue()
    {
      front = null;
      rear = null;
    }

    public T Push(T Element)
    {
      if(Element == null)
      {
        throw new NullReferenceException;
      }
    }

    public T Pop()
    {
      throw new NotImplementedException();
    }

 

    public bool IsEmpty()
    {
      throw new NotImplementedException();
    }
  }
}
