
package edu.wou.cs260.SpellChecker;

/**
 * Create a self balancing tree using the BST class.
 * @author Dominic Groshong
 * @version 2/27/2018
 *
 */

public class AVLTreeSet<T extends Comparable<T>> extends BSTreeSet<T>{

	Node root;
	int compareCount;
	
	public AVLTreeSet() {
		root = null;
		compareCount = 0;
	}

	public static void main(String[] args) {
		// TODO Auto-generated method stub

	}
	
	@Override
	public boolean add(T d) {
		// Test d to see if null
		if(d == null)
			throw new NullPointerException("Inserted data must not be null");
		
		// Try and insert new node using recursive methods.
		else {
			try {
				// Insert Node normally
				treeInsert(d);
				return true;
			}
			
			catch(NullPointerException e) {
				return false;
			}
		}
	}

	@Override
    public void treeInsert(T d) {
       root = treeInsertRec(root, d);
   
    }
	

	private Node treeInsertRec(Node node, T d) {
    	Node x = new Node(d);
    	
        // If the slot is empty, insert the new node.
        if (node == null) {
        	node = x;
            size++;
            return node;
        }
 
        // Else, recur down until null 
        if (compare(x.item, node.item) < 0)
        	node.left = treeInsertRec(node.left, d);
        
        else if (compare(x.item, node.item) > 0)
        	node.right = treeInsertRec(node.right, d);
        
        fixHeight(node);
        
        
        // Update height
        node.height = 1 + Math.max(getHeight(node.left), getHeight(node.right));
 
        // Find balance
        int balance = getBalance(node);
 
        //  Left Left Case
        if (balance > 1 && compare(d, node.left.item) < 0) 
            return rightRotate(node);
 
        // Right Right Case
        if (balance < -1 && compare(d, node.right.item) < 0)
            return leftRotate(node);
 
        // Left Right Case
        if (balance > 1 && compare(d, node.left.item) > 0) {
            node.left = leftRotate(node.left);
            return rightRotate(node);
        }
 
        // Right Left Case
        if (balance < -1 && compare(d, node.right.item) < 0) {
            node.right = rightRotate(node.right);
            return leftRotate(node);
        }
 
        // Return node
        return node;
       
    }
	
	/**
	 * Get the balance of the current node.
	 * @param n the node you want the balance of.
	 * @return numerical value representing balance of node.
	 */
	private int getBalance(Node n) {
        if (n == null)
            return 0;
 
        return getHeight(n.left) - getHeight(n.right);
    }
	
	/**
	 * Rotate the node left.
	 * @param n the node needing rotated.
	 * @return
	 */
	private Node leftRotate(Node n) {
        Node x = n.right;
        Node y = x.left;
 
        // Perform rotation
        x.left = n;
        n.right = y;
 
        //  Update heights
        n.height = Math.max(getHeight(n.left), getHeight(n.right)) + 1;
        x.height = Math.max(getHeight(x.left), getHeight(x.right)) + 1;
 
        // Return new root
        return x;
    }
	
	/**
	 * Rotate the node right once.
	 * @param y
	 * @return
	 */
	private Node rightRotate(Node n) {
        Node x = n.left;
        Node y = x.right;
 
        // Perform rotation
        x.right = n;
        n.left = y;
 
        // Update heights
        n.height = Math.max(getHeight(n.left), getHeight(n.right)) + 1;
        x.height = Math.max(getHeight(x.left), getHeight(x.right)) + 1;
 
        // Return new root
        return x;
    }

}
