package edu.wou.cs260.SpellChecker;

import java.util.Collection;
import java.util.Iterator;
import java.util.List;
import java.util.ListIterator;
import java.util.NoSuchElementException;
import java.util.Queue;

/**
 * This class implements the Java List interface using a doubly-linked list data
 * structure.
 */
public class DLList<T> implements List<T>, Queue<T>, CompareCount {

	// -----------------------
	// ---- DLList Fields ----
	// -----------------------
	private DLLNode head, tail = null;
	private int size = 0;
	private int lastCompareCount = 0;

	// ----------------------------------------------------------
	// ---- Inner classes to support the list implementation ----
	// ----------------------------------------------------------

	// Inner class definition for a doubly linked list node.
	class DLLNode {
		// fields
		DLLNode prev;
		T data;
		DLLNode next;

		// Base constructor with all 3 fields set to null.
		DLLNode() {
			this(null, null, null);
		}

		// Constructor adding new element.
		// @param d The data stored in this node.
		DLLNode(T d) {
			this(null, d, null);
		}

		DLLNode(DLLNode p, T d, DLLNode n) {
			prev = p;
			data = d;
			next = n;
		}
	}

	// Inner class for Iterator.
	class DllIterator implements Iterator<T> {
		DLLNode nextItem = head;

		@Override
		public boolean hasNext() {
			// Test nextItem
			return (nextItem == null) ? false : true;
		}

		@Override
		public T next() {
			// no more items to return
			if (nextItem == null) {
				return null;
			} else {
				T temp = nextItem.data;
				nextItem = nextItem.next;
				return temp;
			}
		}

		@Override
		public void remove() {
			// remove( nextItem.prev.data);
		}
	}

	// -------------------------------------
	// ---- The List interface methods ----
	// -------------------------------------

	@Override
	public int size() {
		return size;
	}

	@Override
	public void clear() {
		// Reset everything
		head = tail = null;
		size = 0;
	}

	@Override
	public boolean isEmpty() {
		return (size == 0) ? true : false;
	}

	@Override
	public boolean contains(Object o) {
		// Check for common error conditions
		if (o == null)
			throw new NullPointerException();

		// Find the node and count comparisons
		lastCompareCount = 0;
		DllIterator it = new DllIterator();
		while (it.hasNext()) {
			lastCompareCount++;
			if (o.equals(it.next()))
				return true;
		}
		// Fail if we get here
		return false;
	}

	@Override
	public T get(int index) {
		// Test for invalid index request
		if ((index >= size) || (index < 0))
			throw new IndexOutOfBoundsException();
		else {
			DLLNode current = head;
			int counter = 0;
			while (counter < index) {
				current = current.next;
				counter++;
			}
			return current.data;
		}
	}

	@Override
	public Iterator<T> iterator() {
		// return an instance of DllIterator
		return new DllIterator();
	}

	@Override
	public boolean add(T arg0) {
		lastCompareCount = 0;

		if (arg0 == null)
			throw new NullPointerException();

		DLLNode newNode = new DLLNode(arg0);

		try {
			add( size, arg0);
		}
		catch ( Exception e) {
			return false;
		}
		
		return true;
	}

	@Override
	public void add(int index, T element) throws IndexOutOfBoundsException {
		// Test for common error conditions
		if (element == null) {
			throw new NullPointerException("Data object is NULL");
		} else if (index < 0 || index > size) {
			throw new IndexOutOfBoundsException(
					"Index is less than 0 or greater than size: " + size);
		}

		DLLNode newNode = new DLLNode(element);

		if (head == null) {
			// empty case
			head = tail = newNode;
			size = 1;
		} else if (index == size) {
			// tail case
			newNode.prev = tail;
			tail.next = newNode;
			tail = newNode;
			size++;
		} else if (index == 0) {
			// head case
			newNode.next = head;
			head.prev = newNode;
			head = newNode;
			size++;
		} else {
			// general case
			DLLNode current = head;
			int counter = 0;

			// Iterate through list until at correct index
			while (index > counter) {
				current = current.next;
				counter++;
			}

			// Adjust newNode's pointers
			newNode.next = current;
			newNode.prev = current.prev;
			// Adjust other nodes' pointers
			current.prev = newNode;
			newNode.prev.next = newNode;
			// Increment size
			size++;
		}

	}

	@Override
	public boolean remove(Object o) {
		// Check for some common error conditions
		if (o == null) {
			throw new NullPointerException();
		} else if (head == null) {
			return false;
		}

		DLLNode temp = head;

		// Find the node to be deleted
		while ((temp != null) && !(o.equals(temp.data))) {
			temp = temp.next;
		}

		// Delete the node and test for success
		if (removeNode(temp) == null) {
			return false;
		} else {
			return true;
		}
	}

	@Override
	public T remove(int index) {
		// Check for common error conditions
		if ((index < 0) || (index >= size)) { // Invalid index
			throw new IndexOutOfBoundsException();
		} else if (head == null) { // empty case
			return null;
		}

		// Find the node to be removed
		DLLNode current = head;
		int counter = 0;
		while (counter < index) {
			current = current.next;
			counter++;
		}

		// Remove the node and return the element removed
		return removeNode(current);

	}

	// private helper method for the public remove methods
	private T removeNode(DLLNode dNode) {
		// remove a node and return element removed
		if (dNode == null) {
			// Empty list case
			return null;
		} else if ((head == tail) && (head.data.equals(dNode.data))) {
			// single node case
			head = null;
			tail = null;
			size = 0;
		} else if (dNode == head) {
			// head case
			head.next.prev = null;
			head = head.next;
			size--;
		} else if (dNode == tail) {
			// tail case
			tail.prev.next = null;
			tail = tail.prev;
			size--;
		} else {
			// general case
			dNode.prev.next = dNode.next;
			dNode.next.prev = dNode.prev;
			size--;
		}

		return dNode.data;
	}

	@Override
	public Object[] toArray() {
		// TODO Auto-generated method stub
		return null;
	}

	@SuppressWarnings("hiding")
	@Override
	public <T> T[] toArray(T[] a) {
		// TODO Auto-generated method stub
		return null;
	}

	@Override
	public boolean containsAll(Collection<?> c) {
		// TODO Auto-generated method stub
		return false;
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
	public int indexOf(Object o) {
		// TODO Auto-generated method stub
		return 0;
	}

	@Override
	public int lastIndexOf(Object o) {
		// TODO Auto-generated method stub
		return 0;
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

	@Override
	public List<T> subList(int fromIndex, int toIndex) {
		// TODO Auto-generated method stub
		return null;
	}

	// -------------------------------------
	// ---- The Queue interface methods ----
	// -------------------------------------

	@Override
	public T element() {
		if (isEmpty()) {
			throw new NoSuchElementException("The queue is empty");
		} else {
			return get(0);
		}
	}

	@Override
	public boolean offer(T arg0) {
		if (arg0 == null) {
			throw new NullPointerException("Null elements not allowed");
		} else {
			return add(arg0);
		}
	}

	@Override
	public T peek() {
		if (isEmpty()) {
			return null;
		} else {
			return get(0);
		}
	}

	@Override
	public T poll() {
		if (isEmpty()) {
			return null;
		} else {
			return remove(0);
		}
	}

	@Override
	public T remove() {
		if (isEmpty()) {
			throw new NoSuchElementException("Queue is empty");
		} else {
			return remove(0);
		}
	}

	// --------------------------------------------
	// ---- The CompareCount interface methods ----
	// --------------------------------------------

	@Override
	public int getLastCompareCount() {
		return lastCompareCount;
	}

	// ----------------------------
	// ---- smoke test methods ----
	// ----------------------------

	/**
	 * Print list prints all items in the container
	 */
	public void printList() {
		Iterator<T> it = this.iterator();
		while (it.hasNext()) {
			System.out.println("List item: " + it.next());
		}
	}

	/**
	 * Has some testing logic to run with the program at this time.
	 * 
	 * @param args
	 *            The standard container for main method.
	 */
//	public static void main(String[] args) {
//		DLList<String> slList = new DLList<String>();
//		slList.add("One");
//		slList.add("Two");
//		slList.add("Three");
//		slList.add("Four");
//		slList.printList();
//		slList.add(3, "New Three");
//		slList.add(0, "Zero");
//		System.out.println("After adds");
//		slList.printList();
//		System.out.println("Size: " + slList.size());
//		System.out.println("Get 3: " + slList.get(3));
//		System.out.println("Get 0: " + slList.get(0));
//		System.out.println("Get 5: " + slList.get(5));
//		System.out.println("Get 10: " + slList.get(10));
//		System.out.println("Get -3: " + slList.get(-3));
//		System.out.println("Item removed: " + slList.remove(6));
//		System.out.println("Item removed: " + slList.remove(4));
//		System.out.println("Item removed: " + slList.remove(0));
//		System.out.println("Item removed: " + slList.remove(100));
//		System.out.println("Item removed: " + slList.remove(-3));
//		slList.printList();
//	}

}
