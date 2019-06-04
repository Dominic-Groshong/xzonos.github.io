package edu.wou.cs260.SpellChecker;

import java.util.Collection;
import java.util.Iterator;
import java.util.Queue;
import java.util.Set;

import edu.wou.cs260.SpellChecker.BSTreeSet.BSTIterator;
import edu.wou.cs260.SpellChecker.BSTreeSet.Node;

public class OpenChainHashSet<E> implements Set<E>, CompareCount
 {
	private DLList<E>[] hashTable; 
	private int tableSize;
	private int size;
	
	@SuppressWarnings("unchecked")
	public OpenChainHashSet(int tSize) {
		this.tableSize = tSize;
		hashTable = (DLList<E>[]) new DLList[tableSize];
		size = 0;
	}

	public static void main(String[] args) {
		// TODO Auto-generated method stub

	}
	
	// Inner class for Iterator.
	class HashIterator implements Iterator<E> {
		Queue<E> itQueue = new DLList<E>();
		
		public HashIterator() {
			itQueue.add(root);
		}
		
				
		@Override
		public boolean hasNext() {
			return false;
				
		}

		@Override
		public E next() {
			return null;

		}

		@Override
		public void remove() {
			// remove(nextItem.prev.data);
		}
	}

	@Override
	public int getLastCompareCount() {
		// TODO Auto-generated method stub
		return 0;
	}

	@Override
	public boolean add(E arg0) {
		// If NULL throw error
		if(arg0 == null)
			throw new NullPointerException("NULL is not a value");
		
		// Get index of new item
		int index = findIndex(arg0);
		
		if(contains(arg0)) {
			return false;
		}
		
		// If array slot is empty create new DLL in that location
		else if(hashTable[index] == null) {
			// Create New list
			DLList<E> newList = new DLList<E>();
			
			// Store list in index
			hashTable[index] = newList;
			
			// Add item to list
			hashTable[index].add(arg0);
			size++;
			
			return true;
		}
		else{
			// Add item to existing list.
			hashTable[index].add(arg0);
			size++;
			
			return true;
		}
	}
	
	/**
	 * Find where the data index would be using the hashCode and tableSize
	 * @param d
	 * @return the index of the item
	 */
	private int findIndex(E d) {
		int index = Math.abs(d.hashCode( )) % tableSize;
		return index;
	}

	@Override
	public boolean addAll(Collection<? extends E> arg0) {
		// TODO Auto-generated method stub
		return false;
	}

	@SuppressWarnings("unchecked")
	@Override
	public void clear() {
		hashTable = (DLList<E>[]) new DLList[tableSize];
		size = 0;
	}

	@Override
	public boolean contains(Object arg0) {

		boolean found = false;

		// Find the correct index
		int index = Math.abs(arg0.hashCode( )) % tableSize;
		
		// Check location if not null check if DLList contains item
		if(hashTable[index] != null)
			found = hashTable[index].contains(arg0);

		// Return found or not
		return found;
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
		
		// Else false
		return false;
	}

	@Override
	public Iterator<E> iterator() {
		return new HashIterator();
	}

	@Override
	public boolean remove(Object arg0) {
		
		if(arg0 == null)
			throw new NullPointerException("NULL is not a value");
		
		if(contains(arg0)) {
			// Get index
			int index = Math.abs(arg0.hashCode( )) % tableSize;
			
			// Remove item
			hashTable[index].remove(arg0);
			size--;
			return true;
		}
			
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
	public <T> T[] toArray(T[] a) {
		// TODO Auto-generated method stub
		return null;
	}

}
