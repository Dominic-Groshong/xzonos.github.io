package edu.wou.cs260.SpellChecker;

import java.util.Collection;
import java.util.Iterator;
import java.util.Queue;
import java.util.Set;

import edu.wou.cs260.SpellChecker.DLList.DLLNode;
import edu.wou.cs260.SpellChecker.DLList.DllIterator;

/**
 * Creates a new binary search tree storing generic information.
 * @author Dominic Groshong
 * @version 2/15/2018
 *
 */

public class BSTreeSet<T extends Comparable<T>> implements Set<T>, CompareCount {

	private Node root;
	private int size;
	private int compareCount;
	
	class Node{
		T item;
		int height;
		Node left;
		Node right;
		
		
		// Constructors
		Node(){
			this(null, null, null);
		}
		
		Node(T item){
			this(null, item, null);
		}
		
		Node(Node left, T item, Node right){
			this.left = left;
			this.item = item;
			this.right = right;
			height = 0;
		}
		
		// Methods
		public T getData() {
			return item;
		}
	}
	
	// Inner class for Iterator.
		class BSTIterator implements Iterator<T> {
			Queue<Node> itQueue = new DLList<Node>();
			
			public BSTIterator() {
				itQueue.add(root);
			}
			
			@Override
			public boolean hasNext() {
				return (itQueue.isEmpty()) ? false : true;
			}

			@Override
			public T next() {
				
				// Queue is empty, return null
				if(itQueue.isEmpty())
					return null;
				
				// Add next item to queue, removing current from queue and returning the removed item.
				else {
					Node temp = itQueue.remove();
					if(temp.left != null)
						itQueue.add(temp.left);
					if(temp.right != null)
						itQueue.add(temp.right);
					return temp.item;
				}
			}

			@Override
			public void remove() {
				// remove(nextItem.prev.data);
			}
		}
	
	
	public BSTreeSet() {
		// TODO Auto-generated constructor stub
		root = null;
		size = 0;
		compareCount = 0;
	}

	public static void main(String[] args) {
		// TODO Auto-generated method stub
		BSTreeSet<Integer> temp = new BSTreeSet<Integer>();
		temp.add(5);
		temp.add(6);
		temp.add(1);
		temp.add(2);

		temp.iterator();
	}

	@Override
	public int getLastCompareCount() {
		return compareCount;
	}

	@Override
	public boolean add(T d) {
		// Test d to see if null
		if(d == null)
			throw new NullPointerException("Inserted data must not be null");
		
		// Try and insert new node using recursive methods.
		else {
			try {
				treeInsert(d);
				return true;
			}
			
			catch(NullPointerException e) {
				return false;
			}
		}
		
//		Non-Recursive Method (Functional)
//
//		Node z = new Node(d);
//		Node y = null;
//		Node x = root;
//		
//		while(x != null) {
//			y = x;
//			
//			if(compare(z.item, y.item) < 0)
//				x = x.left;
//			else
//				x = x.right;
//		}
//		if(y == null) {
//			root = z;
//			size++;
//		}
//		else if( compare(z.item, y.item) < 0) {
//			y.left = z;
//			size++;
//		}
//		else {
//			y.right = z;
//			size++;
//		}

	}
	
	/**
	 * Insert node using user inputed data.
	 * @param d
	 */
    public void treeInsert(T d) {
       root = treeInsertRec(root, d);
    }
     
    /**
     * Insert the node recursively.
     * @param parent an existing node your comparing the new node too.
     * @param d the new node
     * @return the left or right parent child to recur again or returns the new node.
     */
    private Node treeInsertRec(Node parent, T d) {
    	Node x = new Node(d);
    	
        // If the slot is empty, insert the new node.
        if (parent == null) {
            parent = x;
            size++;
            return parent;
        }
 
        // Else, recur down until null 
        if (compare(x.item, parent.item) < 0)
            parent.left = treeInsertRec(parent.left, d);
        
        else if (compare(x.item, parent.item) > 0)
            parent.right = treeInsertRec(parent.right, d);
        
        fixHeight(parent);
        return parent;
    }
 
    /**
     * Return the height of the node -1 for null node pointers.
     * @param current
     * @return
     */
	public int getHeight(Node current) {
		return (current == null) ? -1 : current.height;
	}
	
	/**
	 * Adjust the height of the current node changing it to represent the new height ammount.
	 * @param current
	 */
    public void fixHeight(Node current) {
    	if(current == null) return;
    	int lch = getHeight(current.left);
    	int rch = getHeight(current.right);
    	
    	current.height = Math.max(lch, rch) + 1;
    }
    
	/*
	 * Compares two sets of data and 
	 * returns an integer value depending on the results
	 */
	public int compare(T x, T y) {
		return x.compareTo(y);
	}
	
	@Override
	public boolean addAll(Collection<? extends T> arg0) {
		// TODO Auto-generated method stub
		return false;
	}

	@Override
	public void clear() {
		root = null;
		size = 0;
	}
	/**
	 * Checks to see if the tree contains searched parameter. 
	 * @param d
	 * @return
	 */
	public boolean contains(T d) {	
		compareCount = 0;
		return search(root, d);
	}
	
	/**
	 * Searches tree for the item and returns true if found
	 * @param parent Node searched
	 * @param k data your searching for
	 * @return
	 */
	public boolean search(Node parent, T k) {	
		
		if(parent == null)
			return false;
		
		else if(compare(k, parent.item) == 0) {
			compareCount++;
			return true;
		}
		
		else if(compare(k, parent.item) < 0) {
			compareCount++;
			return search(parent.left, k);
		}
		else {
			compareCount++;
			return search(parent.right, k);
		}
	}
	
	@Override
	public boolean containsAll(Collection<?> arg0) {
		// TODO Auto-generated method stub
		return false;
	}

	@Override
	public boolean isEmpty() {
		
		if(size == 0)
			return true;

		return false;
	}

	@Override
	public Iterator<T> iterator() {
			
		return new BSTIterator();
	}

	@Override
	public boolean remove(Object arg0) {
		// TODO Auto-generated method stub
		return false;
	}

	@Override
	public boolean removeAll(Collection<?> arg0) {
		// TODO Auto-generated method stub
		return false;
	}

	@Override
	public boolean retainAll(Collection<?> arg0) {
		// TODO Auto-generated method stub
		return false;
	}

	@Override
	public int size() {
		return size;
	}

	@Override
	public Object[] toArray() {
		// TODO Auto-generated method stub
		return null;
	}

	@Override
	public <T> T[] toArray(T[] arg0) {
		// TODO Auto-generated method stub
		return null;
	}

}
