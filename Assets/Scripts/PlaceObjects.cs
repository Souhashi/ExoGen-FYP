using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceObjects : MonoBehaviour {
    public Map map;
    List<GraphNode> items= new List<GraphNode>();
    DependencyTree tree;
    List<GraphNode> sorted = new List<GraphNode>();
    void InitialiseNodes()
    {
        foreach (Room r in map.rooms)
        {
            items.Add(new GraphNode(r));
        }
        tree = new DependencyTree(items);
    }

    void Dependencies()
    {
        tree.AddDependency(tree.getNodes()[0], tree.getNodes()[2]);
        tree.AddDependency(tree.getNodes()[2], tree.getNodes()[1]);
        tree.AddDependency(tree.getNodes()[0], tree.getNodes()[4]);
    }

    public void Sort()
    {

        foreach (GraphNode g in items) {
            Spawn(g);
        }
        Debug.Log("Size: "+ items.Count);

    }

    public void Spawn(GraphNode n)
    {
       
        if (n.state == GraphNode.State.Black) {
            return;
        }
        if (n.state == GraphNode.State.Grey) {
            Debug.Log("Not a DAG");
            return;
        }
        n.state = GraphNode.State.Grey;

        foreach (GraphNode r in n.getNeighbours()) {
            Spawn(r);
        }
        n.state = GraphNode.State.Black;
        sorted.Add(n);

    }
	// Use this for initialization
	void Start () {
        InitialiseNodes();
        Dependencies();
        Sort();
        foreach (GraphNode n in items) {
            Debug.Log("Node:"+ n.nodeID);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
