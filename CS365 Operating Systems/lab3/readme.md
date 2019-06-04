# CS365 Operating System
## Lab 3

#### Part I: Producer and consumer (multithread)
>Implement the code from Figures 4.13, 4.14, 4.15

First Step was to create the Factory class and setting it up to create a producer and consumer
```java
public class Factory{
    public static void main(String args[]){
        // Create the message queue
        Channel<Date> queue = new MessageQueue<Date>();

        //Create the producer and consumer threads and pass each thread a reference to the MessageQueue object.
        Thread producer = new Thread(new Producer(queue));
        Thread consumer = new Thread(new Consumer(queue));

        // Start the Threads
        producer.start();
        consumer.start();
    }
}  
```
The next step was to create the Consumer class that gets the queue and receives messages and there by “consuming” them.
```java
import java.util.Date;

class Consumer implements Runnable{
    private Channel<Date> queue;
    
    public Consumer(Channel<Date> queue){
        this.queue = queue;
    }

    public void run(){
        Date message;

        while(true){
            // nap for awhile
            SleepUtilities.nap();

            // consume an item from the buffer
            message = queue.receive();
            if(message != null){
                System.out.println("Consumed: " + message);
            } 
        }
    }

}
```
Next I recreated the Producer class which gets the queue and adds a date message to the queue thus “producing” them.
```java
import java.util.Date;

class Producer implements Runnable{
    private Channel<Date> queue;
    
    public Producer(Channel<Date> queue){
        this.queue = queue;
    }

    public void run(){
        Date message;

        while(true){
            // nap for awhile
            SleepUtilities.nap();

            // produce and item and enter it into the buffer
            message = new Date();
            System.out.println("Produced: " + message);
            queue.send(message);
        }
    }

}
```
This create the following output for the queue version:
```debug
Produced: Tue Jun 04 10:29:40 PDT 2019
Consumed: Tue Jun 04 10:29:40 PDT 2019
Produced: Tue Jun 04 10:29:43 PDT 2019
Consumed: Tue Jun 04 10:29:43 PDT 2019
Produced: Tue Jun 04 10:29:47 PDT 2019
Produced: Tue Jun 04 10:29:48 PDT 2019
Produced: Tue Jun 04 10:29:48 PDT 2019
Produced: Tue Jun 04 10:29:48 PDT 2019
Consumed: Tue Jun 04 10:29:47 PDT 2019
```
#### Part II: Producer and consumer (semaphore)
>Implement the code from Figures 6.9 – 6.14

Created an interface class to be used in the bounded buffer.
```java
/**
 * Interface for the Bounded Buffer
 * @param <E>
 */
interface Buffer<E>{
    public void insert(E item);
    public E remove();
}
```
Class constructor initializing the buffer with in, out, mutex, empty, and full states. With implementation of the insert and remove methods from the interface.
```java
public class BoundedBuffer<E> implements Buffer<E> {
    private static final int BUFFER_SIZE = 5;
    private E[] buffer;
    private int in, out;
    private Semaphore mutex;
    private Semaphore empty;
    private Semaphore full;
 
    public BoundedBuffer(){
        // buffer is initially empty
        in = 0;
        out = 0;
        mutex = new Semaphore(1);
        empty = new Semaphore(BUFFER_SIZE);
        full = new Semaphore(0);

        buffer = (E[]) new Object[BUFFER_SIZE];
    }

    public void insert(E item){
        try{
            empty.acquire();
            mutex.acquire();
        }
        catch(InterruptedException e){
            System.out.println(e);
        }
  

        // add an item to the buffer
        buffer[in] = item;
        in = (in + 1) % BUFFER_SIZE;
        
        mutex.release();
        full.release();
    }

    public E remove(){
        E item;
        try{
            full.acquire();
            mutex.acquire();
        }
        catch(InterruptedException e){
            System.out.println(e);
        }
        // remove an item from the buffer
        item = buffer[out];
        out = (out + 1) % BUFFER_SIZE;

        mutex.release();
        empty.release();
        
        return item;
    }
}
```
Updated factory to use the new bounded buffer class
```java
public class Factory{
    public static void main(String args[]){
        // Create the buffer
        Buffer<Date> buffer = new BoundedBuffer<Date>();

        //Create the producer and consumer threads and pass each thread a reference to the buffer.
        Thread producer = new Thread(new Producer(buffer));
        Thread consumer = new Thread(new Consumer(buffer));

        // Start the Threads
        producer.start();
        consumer.start();
    }
}  
```
Updated the consumer to use the buffer and remove() method for "consuming" items
```java
import java.util.Date;

class Consumer implements Runnable{
    private Buffer<Date> buffer;
    
    public Consumer(Buffer<Date> buffer){
        this.buffer = buffer;
    }

    public void run(){
        Date message;

        while(true){
            // nap for awhile
            SleepUtilities.nap();

            // consume an item from the buffer
            message = (Date)buffer.remove();
            System.out.println("Consumed: " + message);
        }
    }

}
```
Updated the producer to use the buffer and insert() method for "producing" items.
```java
import java.util.Date;

class Producer implements Runnable{
    private Buffer<Date> buffer;
    
    public Producer(Buffer<Date> buffer){
        this.buffer = buffer;
    }

    public void run(){
        Date message;

        while(true){
            // nap for awhile
            SleepUtilities.nap();

            // produce and item and enter it into the buffer
            message = new Date();
            buffer.insert(message);
            System.out.println("Produced: " + message);
        }
    }

}
```
This create the following output for the BoundedBuffer version:
```debug
Produced: Tue Jun 04 10:35:48 PDT 2019
Consumed: Tue Jun 04 10:35:48 PDT 2019
Produced: Tue Jun 04 10:35:49 PDT 2019
Consumed: Tue Jun 04 10:35:49 PDT 2019
Produced: Tue Jun 04 10:35:51 PDT 2019
Consumed: Tue Jun 04 10:35:51 PDT 2019
Produced: Tue Jun 04 10:35:54 PDT 2019
Produced: Tue Jun 04 10:35:54 PDT 2019
Produced: Tue Jun 04 10:35:54 PDT 2019
Produced: Tue Jun 04 10:35:54 PDT 2019
```