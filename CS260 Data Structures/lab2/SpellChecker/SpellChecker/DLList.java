package edu.wou.cs260.SpellChecker;

import java.util.Collection;
import java.util.Iterator;
import java.util.List;
import java.util.ListIterator;
import java.util.Queue;

import edu.wou.cs260.SpellChecker.DLList.DLLNode;

public class DLList<T> implements List<T>, Queue<T> {
	private int size;
	private DLLNode head;
	private DLLNode tail;
	
	public DLList() {
		head = new DLLNode();
		tail = new DLLNode();
		head.next = tail;
		tail.prev = head;
		size = 0;
	}
	
	
	class DLLNode {
		 // Node fields
		 private DLLNode prev;
		 private T data;
		 private DLLNode next;
		 
		 // Node methods
		 DLLNode() {
			 this(null, null, null);
		 }
		 DLLNode(T d) {
			 this(null, d, null);
		 }
		 DLLNode(DLLNode p, T d, DLLNode n) {
			 prev = p;
			 data = d;
			 next = n;
		 }
	}
	
	class DLLIterator implements Iterator<T>{
		private DLLNode nextItem = head.next;
		
		@Override
		public boolean hasNext() {
			// Test for next item
			if(nextItem == null)
				return false;
			
			return true;
		}	
		
		@Override
		public T next() {
			// No remaining items
			if(nextItem == null)
				return null;
			else {
				T temp = nextItem.data;
				nextItem = nextItem.next;
				return temp;
			}
				
		}
		
	}
	
	/**
	 * Main method to test all others.
	 */
	public static void main(String[] args) {
		DLList<Integer> testList = new DLList<Integer>();
		testList.add(1);  // [0]
		testList.add(2);  // [1]
		testList.add(3);  // [2]
		testList.add(4);  // [3]
		testList.add(5);  // [4]
		testList.add(6);  // [5]
		testList.add(7);  // [6]
		testList.add(8);  // [7]
		testList.add(9);  // [8]
		testList.add(10); // [9]
		testList.remove(3);
		testList.add(8,50);

		testList.printList();
		
		testList.getTester(8);
	}

	/**
	 * Print the contents of the list.
	 */
	public void printList() {
		
		Iterator<T> it = iterator();
		
		int index = 0;
		while(it.hasNext()) {
			T content = it.next();
			System.out.println( "["+index+"] " + content );
			index++;
		}
	}
	
	public void getTester(int index) {
		System.out.println(get(index) + " was stored in index: " + index);
		System.out.println("Size of the list is: " + size());
	}
	
	@Override
	public T element() {
		// TODO Auto-generated method stub
		return null;
	}

	@Override
	public boolean offer(T e) {
		// TODO Auto-generated method stub
		return false;
	}

	@Override
	public T peek() {
		get(0);
		return null;
	}

	@Override
	public T poll() {
		remove(0);
		return null;
	}

	@Override
	public T remove() {
		if(size() == 0){
			// throw new IndexOutOfBoundsException("Queue does not exist");
			return null;
		}
		
		else {
			T data = remove(0);
			return data;
		}
	}

	@Override
	public boolean add(T d) {
		if(size == 0)
			add(size, d);
		else
			add(size-1, d);
		
		return true;
		
	}
	
	
	@Override
	public void add(int index, T d){
		DLLNode node = new DLLNode(d);
		DLLNode loc = getNode(index);
		
		if(d == null) {
			throw new NullPointerException("Cannot store null data.");
		}
		
		// If list empty, start list.
		if(size == 0) {
			head.next = node;
			tail.prev = node;
			size++;
		}
		
		//Insert to tail
		else if(index == size-1 && size > 0) {
			node.prev = tail.prev;
			node.prev.next = node;
			
			tail.prev = node;
			size++;
		}
		
		// Insert to head
		else if(index == 0 && size > 0) {
			node.next = head.next;
			node.next.prev = node;
			
			head.next = node;
			size++;
			
		}
		
		else {
			node.next = loc;
			node.prev = loc.prev;
			node.next.prev = node;
			node.prev.next = node;
			size++;
		}
				
	}
	
	@Override
	public boolean addAll(Collection<? extends T> c) {
		// TODO Auto-generated method stub
		return false;
	}

	@Override
	public boolean addAll(int index, Collection<? extends T> c) {
		// TODO Auto-generated method stub
		return false;
	}

	@Override
	public void clear() {
		head.prev = null;
		head.next = null;
		tail.prev = null;
		tail.next = null;
		size = 0;
	}

	@Override
	public boolean contains(Object o) {
		Iterator<T> it = iterator();
		
		if(o == null) {
			throw new NullPointerException("You searched for nothing.");
		}
		
		while(it.hasNext()) {
			if(o.equals(it.next())) {
				return true;
			}
		}
		return false;
	}

	@Override
	public boolean containsAll(Collection<?> c) {
		// TODO Auto-generated method stub
		return false;
	}
	
	/**
	 * Return the node itself at the requested index.
	 * @param index the index you want the node from.
	 * @return the node at index requested.
	 */
	public DLLNode getNode(int index) throws IndexOutOfBoundsException{
		DLLNode n = null;
		
		if(index > size || index < 0) {
			throw new IndexOutOfBoundsException("That index does not exist in this list.");
		}
		
		if(index < size/2) {
			n = head.next;
			
			for(int i = 0; i < index; i++)
				n = n.next;
		}else {
			n = tail;
			for(int i = size; i > index; i--)
				n = n.prev;
		}	
		
		return n;
	}
	
	
	/**
	 *  Return the data of the node;
	 *  @Override
	 */
	public T get(int index){
		DLLNode temp = getNode(index);
		return temp.data;
	}

	@Override
	public int indexOf(Object o) {
		// TODO Auto-generated method stub
		return 0;
	}

	@Override
	public boolean isEmpty() {
		if(size == 0)
			return true;
		return false;
	}

	@Override
	public int lastIndexOf(Object o) {
		// TODO Auto-generated method stub
		return 0;
	}

	@Override
	public boolean remove(Object o) {
		// TODO Auto-generated method stub
		return false;
	}

	@Override
	public T remove(int index) {
		DLLNode remove = getNode(index);
		
		if(index == 0 && size == 1) {
			head.next = tail;
			tail.prev = head;
			size--;
		}
		
		else if(index == 0) {
			head.next = remove.next;
			remove.next.prev = null;
			size--;
		}
		
		else if(index == size) {
			tail.prev = remove.prev;
			remove.prev.next = null;
			size--;
		}
		
		
		else {
			remove.next.prev = remove.prev;
			remove.prev.next = remove.next;
			size--;
		}
		
		return remove.data;
	}

	@Override
	public boolean removeAll(Collection<?> c) {
		// TODO Auto-generated method stub
		return false;
	}

	@Override
	public boolean retainAll(Collection<?> c) {
		// TODO Auto-generated method stub
		return false;
	}

	@Override
	public T set(int index, T element) {
		// TODO Auto-generated method stub
		return null;
	}

	@Override
	public int size() {
		return size;
	}

	@Override
	public List<T> subList(int fromIndex, int toIndex) {
		// TODO Auto-generated method stub
		return null;
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

	@Override
	public Iterator<T> iterator() {
		return new DLLIterator();
	}

	@Override
	public ListIterator<T> listIterator() {
		// TODO Auto-generated method stub
		return null;
	}

	@Override
	public ListIterator<T> listIterator(int index) {
		// TODO Auto-generated method stub
		return null;
	}

}
