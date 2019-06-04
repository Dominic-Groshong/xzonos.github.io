package edu.wou.cs260;

import java.util.Random;

/**
 *  SortSearch sets up an array of random numbers of size listsize to test several
 *  sorting and searching algorithms which will then be instrumented to count the
 *  number of comparisons done and the execution times at various list sizes
 *  
 *  @author Mitchel Fry & Dominic Groshong
 *  @version CS260 Lab #1, 1/18/18
 */
public class SortSearch
{
    private int listsize = 10;
    private final int RANGE = 100000;
    private int[ ] numList;
    private Random rand;
    private long count;
    private long timeStart;
    private long timeEnd;
    
    /**
     * Default constructor, just initialize the numList array 
     */
    public SortSearch( )
    {
        numList = new int[ listsize];
    }

    /**
     * Non-default constructor, initialize the numList array to the given size
     * 
     * @param size of array to create
     */
    public SortSearch( int size)
    {
        listsize = size;
        numList = new int[ listsize];
    }

    /**
     * Fills the array with listsize integers
     */
    public void fillArray( )
    {
        rand = new Random( );

        for ( int i = 0; i < listsize; i++) {
            numList[ i] = rand.nextInt( RANGE);
        }
    }

    /**
     * Prints the entire array in indexed order
     */
    public void printArray( )
    {
        for ( int i = 0; i < numList.length; i++) {
            System.out.println( "Element " + i + ": " + numList[ i]);
        }
    }
    
    /**
     *  Subtracts the system time from when the sort started and
     *  subtracts it from the system time at end of sorting
     */
    public long checkSpeed() {
    	return timeEnd - timeStart;
    }
    
    //================== Sorting Methods =======================      
    /**
     * Sorts the array using Insertion Sort logic
     */
    public void insertionSort( )
    {	
    	count = 0;
    	timeStart = getSystemTime();
    	
        for ( int i = 1; i < numList.length; i++) {
            // insert numList[i] into numList[0:i-1]
            int j, t = numList[ i];

            for ( j = i - 1; j >= 0 && t < numList[ j]; j--) {
                numList[ j + 1] = numList[ j]; //shuffle the empty space
                count++;
            }

            numList[ j + 1] = t; //assign t into the empty space
        }
        
        timeEnd = getSystemTime();
    }
 
    /**
     * Sorts the array using Insertion Sort logic
     */
    public void selectionSort( )
    {
    	count = 0;
    	timeStart = getSystemTime();
        int t;

        for ( int i = 0; i < numList.length; i++) {
            t = i; //Index of the smallest element in this pass

            for ( int j = i; j < numList.length; j++) {
            	count++;
                
            	if ( numList[ j] < numList[ t]) {
                    t = j;
                }
            }
            swap( i, t); //swap the values at these positions
        }
        timeEnd = getSystemTime();
    }

 
    /**
     * Sorts the array using Bubble Sort logic (bad way to sort)
     */
    public void bubbleSort( )
    {
    	count = 0;
    	timeStart = getSystemTime();

        for ( int out = numList.length - 1; out > 0; out--) { // outer loop (backward)
            for ( int in = 0; in < out; in++) { // inner loop (forward)
            	
                if ( numList[ in] > numList[ in + 1]) { // out of order?
                    swap( in, in + 1);
                    count++;
                }
            }
        }
        timeEnd = getSystemTime();
    }



    //================== Search Methods =======================    
    /**
     * A shell method for your linear search logic
     * 
     * @param searchNum the value to search for in the array 
     * @return returns true is searchNum is found in the array, otherwise false
     */
    public boolean searchLinear( int searchNum)
    {	
    	count = 0;
    	timeStart = getSystemTime();
    	for (int i = 0; i < numList.length-1; i++)
        {
    		count++;
            if (numList[i] == searchNum)
                return true;
            timeEnd = getSystemTime();
        }
    	
    	timeEnd = getSystemTime();
        return false;

    }
  
    /**
     * A shell method for your binary search logic
     * 
     * @param searchNum the value to search for in the array 
     * @return returns true is searchNum is found in the array, otherwise false
     */
    public boolean searchBinary(int searchNum)
    {
    	count = 0;
    	timeStart = getSystemTime();
    	
        int low = 0;
        int high = numList.length - 1;
        
        while (low <= high)
        {
        	count++;
            int mid = low + (high-low)/2;
 
            // Check if x is present at mid
            if (numList[mid] == searchNum)
                return true;
            	timeEnd = getSystemTime();
 
            // If x greater, ignore left half
            if (numList[mid] < searchNum)
            	low = mid + 1;
 
            // If x is smaller, ignore right half
            else
            	high = mid - 1;
        }
 
        // if we reach here, then element was 
        // not present
        timeEnd = getSystemTime();
        return false;
    }

    /**
     * This method can be used to print your results of a run.  Refactor this as appropriate for
     * what ever method you choose to collect your data points.  I would suggest writing to a 
     * comma delimited text file so that you can easily import the data into a spreadsheet for
     * graphing and analysis.
     * 
     * @param sortType A string stating the type of sort that was done
     */
    public void printSortingMetrics( String sortType )
    {
        
        long averageTime = 0;
        long totalTime = 0;
        long averageCompare = 0;
        long totalCompare = 0;
        
        for(int i = 1; i<100; i++) {
        	int temp = rand.nextInt(RANGE);
        	//searchLinear(temp);
        	searchBinary(temp);
        	totalTime = totalTime + checkSpeed();
        	totalCompare = totalCompare + count;
        }
        averageTime = totalTime/100;
        averageCompare = totalCompare/100;
        System.out.println("Average time to compleate: " + averageTime);
        System.out.println("Average number of comparisons: " + averageCompare + "\n");
    }
    
    //============ Private utility methods ==============
    /**
     * This value is the internal system time that is the measured in
     * the number of nano-seconds since Jan. 1, 1970.
     * 
     * @return The current time of the system clock
     */
    private long getSystemTime( )
    {
        return System.nanoTime( );
    }

    
    /**
     * Swap routine to re-order the ith and jth array values
     *
     * @param  i   an array location to be swapped with j
     * @param  j   an array location to be swapped with i
     */
    private void swap(int i, int j)
    {
        int temp = numList[i];
        numList[i] = numList[j];
        numList[j] = temp;
    }

    
    //============Program entry point MAIN======================
    /**
     * Standard class method "main".  Modify this to collect the
     * empherical metrics data on the sorting and searching methods
     */
    public static void main( String[ ] args)
    {
        int i = 1;
        for ( i = 1; i <= 10; i++) {
            SortSearch test = new SortSearch(10000 * i);

            //test.fillArray();
            //test.bubbleSort();
            //test.printArray();            
            //test.printSortingMetrics("BubbleSort");
            
            test.fillArray();
            test.insertionSort();
            //test.printArray();
            test.printSortingMetrics("InsertionSort");
            
            //test.fillArray();
            //test.selectionSort();
            //test.printArray();
            //test.printSortingMetrics("SelectionSort");
            
        }
    }
}