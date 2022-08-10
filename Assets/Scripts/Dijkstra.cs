using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dijkstra : MonoBehaviour
{
    private GameObject[] _nodes;
    public Node start;
    public Node end;

    private void Start()
    {
        List<Node> shortestPath = FindShortestPath(start, end);

        Node prevNode = null;   
        foreach (Node node in shortestPath)
        {
            if (prevNode != null)
            {
                Debug.DrawLine(node.transform.position + Vector3.up, prevNode.transform.position + Vector3.up, Color.blue, 5f);
            }
            Debug.Log(node.gameObject.name);
            prevNode = node;
        }
    }


    public List<Node> FindShortestPath(Node start, Node end)
    {
        _nodes = GameObject.FindGameObjectsWithTag("Nodes");


   

        if (DijkstraAlgorithm(start, end))
        {
            List<Node> result = new List<Node>();
            Node node = end;
            do
            {
                result.Insert(0, node);
                node = node.PreviousNode;
            } while (node != null);

            return result;
        }

        return null;

    }
    private bool DijkstraAlgorithm(Node start, Node end)
    {
        List<Node> unexplorerd = new List<Node>();
        foreach (GameObject obj in _nodes)
        {
            Node n = obj.GetComponent<Node>();
            if (n == null) continue; //if n is null, then exit the current loop and go to the next foreach loop
            {
                n.ResetNode();
                unexplorerd.Add(n);
            }
        }

        if (!unexplorerd.Contains(start) && !unexplorerd.Contains(end))
        {
            return false;
        }

        start.PathWeight = 0;
        while (unexplorerd.Count > 0)
        {
            //order based on path
            unexplorerd.Sort(
                (x, y) => x.PathWeight.CompareTo(y.PathWeight));

            //current is the shortest path possibility
            Node current = unexplorerd[0];
            if (current == end)
            {
                break;
            }
            unexplorerd.RemoveAt(0);

            foreach (Node neighbourNode in current.NeighbourNodes)
            {
                if (!unexplorerd.Contains(neighbourNode)) continue;

                float distance = Vector3.Distance(neighbourNode.transform.position, current.transform.position);
                distance += current.PathWeight;

                if (distance < neighbourNode.PathWeight)
                {
                    neighbourNode.PathWeight = distance;
                    neighbourNode.PreviousNode = current;
                }
            }
        }

        return true;
    }
}
