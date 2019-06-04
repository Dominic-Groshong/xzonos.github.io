/**
 * Interface for the Bounded Buffer
 * @param <E>
 */
interface Buffer<E>{
    public void insert(E item);
    public E remove();
}