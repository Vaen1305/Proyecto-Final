using System;
using System.Collections.Generic;

public class PriorityQueue<T> where T : IComparable<T>
{
    private SortedSet<T> set = new SortedSet<T>();

    public int Count => set.Count;

    public void Enqueue(T item)
    {
        set.Add(item);
    }

    public void Dequeue(T item)
    {
        set.Remove(item);
    }

    public T Peek()
    {
        if (set.Count == 0) throw new InvalidOperationException("The queue is empty.");
        return set.Min;
    }

    public T Dequeue()
    {
        if (set.Count == 0) throw new InvalidOperationException("The queue is empty.");
        T min = set.Min;
        set.Remove(min);
        return min;
    }

    public IEnumerator<T> GetEnumerator()
    {
        return set.GetEnumerator();
    }
}
