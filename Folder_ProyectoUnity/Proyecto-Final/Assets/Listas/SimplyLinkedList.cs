using System;
public class SimplyLinkedList<T>
{
    private class Node
    {
        public T Value { get; set; }
        public Node Next { get; set; }

        public Node(T value)
        {
            this.Value = value;
            this.Next = null;
        }
    }

    private Node head;
    private int length;

    public SimplyLinkedList()
    {
        this.head = null;
        this.length = 0;
    }

    public void InsertNodeAtEnd(T value)
    {
        Node newNode = new Node(value);
        if (head == null)
        {
            head = newNode;
        }
        else
        {
            Node tmp = head;
            while (tmp.Next != null)
            {
                tmp = tmp.Next;
            }
            tmp.Next = newNode;
        }
        length++;
    }

    public T Get(int index)
    {
        if (index < 0 || index >= length)
        {
            throw new ArgumentOutOfRangeException();
        }

        Node tmp = head;
        for (int i = 0; i < index; i++)
        {
            tmp = tmp.Next;
        }

        return tmp.Value;
    }

    public int Count
    {
        get { return length; }
    }
}
