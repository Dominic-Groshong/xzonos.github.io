import java.util.Date;

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