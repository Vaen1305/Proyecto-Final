using System;
using System.Collections.Generic;

public class PriorityQueue<T>
{
    private class Node
    {
        public T Value { get; set; }
        public int Priority { get; set; }
        public Node Next { get; set; }

        public Node(T value, int priority)
        {
            Value = value;
            Priority = priority;
            Next = null;
        }
    }

    private Node head;
    private int length;

    public PriorityQueue()
    {
        head = null;
        length = 0;
    }

    public void Enqueue(T value, int priority)
    {
        Node newNode = new Node(value, priority);
        if (head == null || head.Priority > priority)
        {
            newNode.Next = head;
            head = newNode;
        }
        else
        {
            Node current = head;
            while (current.Next != null && current.Next.Priority <= priority)
            {
                current = current.Next;
            }
            newNode.Next = current.Next;
            current.Next = newNode;
        }
        length++;
    }

    public T Dequeue()
    {
        if (head == null)
        {
            throw new InvalidOperationException("");
        }
        T value = head.Value;
        head = head.Next;
        length--;
        return value;
    }

    public int Count
    {
        get { return length; }
    }

    public bool Contains(T value)
    {
        Node current = head;
        while (current != null)
        {
            if (current.Value.Equals(value))
            {
                return true;
            }
            current = current.Next;
        }
        return false;
    }

    public void Remove(T value)
    {
        if (head == null)
        {
            return;
        }

        if (head.Value.Equals(value))
        {
            head = head.Next;
            length--;
            return;
        }

        Node current = head;
        while (current.Next != null && !current.Next.Value.Equals(value))
        {
            current = current.Next;
        }

        if (current.Next != null)
        {
            current.Next = current.Next.Next;
            length--;
        }
    }
}
