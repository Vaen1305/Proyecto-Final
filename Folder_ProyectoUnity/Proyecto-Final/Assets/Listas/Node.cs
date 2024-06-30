using UnityEngine;

public class Node
{
    public Transform Waypoint { get; set; }
    public SimplyLinkedList<Node> Edges { get; set; }

    public Node(Transform waypoint)
    {
        Waypoint = waypoint;
        Edges = new SimplyLinkedList<Node>();
    }

    public void AddEdge(Node node)
    {
        Edges.InsertNodeAtEnd(node);
    }
}
