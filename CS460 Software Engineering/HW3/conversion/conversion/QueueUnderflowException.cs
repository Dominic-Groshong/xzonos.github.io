using System;
using System.Collections.Generic;
using System.Text;

namespace javaConvert
{

  /// <summary>
  /// A custom unchecked exception to represent situations where 
  /// an illegal operation was performed on an empty queue.
  /// </summary>
  class QueueUnderflowException : Exception
  {
    /// <summary>
    /// Blank constructor
    /// </summary>
    public QueueUnderflowException()
    {

    }

    /// <summary>
    /// Constructor that throws an error message
    /// </summary>
    /// <param name="message">the message to throw</param>
    public QueueUnderflowException(string message)
      : base(message)
    {

    }

  }
}
