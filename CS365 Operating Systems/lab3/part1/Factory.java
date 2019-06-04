import java.util.Date;

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