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