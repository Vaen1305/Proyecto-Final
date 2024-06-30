using System;
using UnityEngine;

public class Graph
{
    private SimplyLinkedList<Node> nodes;

    public Graph()
    {
        nodes = new SimplyLinkedList<Node>();
    }

    public void AddNode(Node node)
    {
        nodes.InsertNodeAtEnd(node);
    }

    public Node GetNode(int index)
    {
        return nodes.Get(index);
    }

    public int Count
    {
        get { return nodes.Count; }
    }
}
