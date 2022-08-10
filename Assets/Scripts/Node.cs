using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    /// <summary>
    ///   Total cost of shortest path to this node
    /// </summary>


     private float _pathWeight = int.MaxValue;

    public float PathWeight
    {

        get { return _pathWeight; }
        set { _pathWeight = value; }

    }
    /// <summary>
    /// following the shortest path, previousNode is previous step on that path
    /// </summary>

   private Node _previousNode = null;

    public Node PreviousNode
    {
        get { return _previousNode; }
        set { _previousNode = value; }
    }


    [SerializeField] private List<Node> _neighbourNode;
    public List<Node> NeighbourNodes
    {
        get
        {
            List<Node> result = new List<Node>(_neighbourNode);
            return result;
        }

    }



    private void Start()
    {
        ResetNode();
        ValidateNeighbour();
    }


    public void ResetNode()
    {
        _pathWeight = int.MaxValue;
        _previousNode = null;
    }

    public void AddNeighbourNode(Node node)
    {
        _neighbourNode.Add(node);
    }


    private void OnDrawGizmos()
    {
        foreach (Node node in _neighbourNode)
        {
            if (node == null) continue;
            Gizmos.color = Color.red;
            Gizmos.DrawLine (transform.position, node.transform.position);
        }

    }

    private void OnValidate()
    {

        ValidateNeighbour();
    }

    private void ValidateNeighbour()
    {
        foreach (Node node in _neighbourNode)
        {
            if (node == null) continue;

            if (!node._neighbourNode.Contains(this))
            {
                node.AddNeighbourNode(this);
            }
        }
    }


}
