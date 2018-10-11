using System;
using System.Collections.Generic;
using System.Text;

namespace javaConvert
{

  /// <summary>
  /// A FIFO queue interface. This ADT is suitable for a linked queue.
  /// </summary>
  /// <typeparam name="T"></typeparam>
  interface IQueue<T>
  {
    /// <summary>
    /// Add an element to the rear of the queue
    /// </summary>
    /// <param name="element">Element to be added</param>
    /// <returns>the element that was enqueued</returns>
    T Push(T element);

    /// <summary>
    /// remove element from queue, will throw error if empty
    /// </summary>
    /// <returns>element at front</returns>
    T Pop();

    /// <summary>
    /// Test if queue is empty
    /// </summary>
    /// <returns>true if empty; false otherwise.</returns>
    Boolean isEmpty();

  }
}
