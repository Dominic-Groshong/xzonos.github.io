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

    public T Push(T element)
    {
      if(element == null)
      {
        throw new NullReferenceException();
      }
      else if( isEmpty())
      {
        Node<T> tmp = new Node<T>(element, null);
        rear = front = tmp;
      }
      else
      {
        Node<T> tmp = new Node<T>(element, null);
        rear.Next = tmp;
        rear = tmp;
      }

      return element;
    }

    public T Pop()
    {
      T tmp = default(T);

      if ( isEmpty())
      {
        throw new QueueUnderflowException("The queue was empty when pop was invoiked.");
      }

      else if( front == rear)
      { // one item in queue
        tmp = front.Data;
        front = null;
        rear = null;
      }

      else
      {
        tmp = front.Data;
        front = front.Next;
      }

      return tmp;
    }

 

    public bool isEmpty()
    {
      if ( front == null && rear == null)
      {
        return true;
      }

      else
      {
        return false;
      }
    }
  }
}
