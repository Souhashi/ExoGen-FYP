using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DependencyTree  {

    private List<GraphNode> Nodes;

    public DependencyTree()
    {
        Nodes = new List<GraphNode>();
    }

    public DependencyTree(List<GraphNode> nodes)
    {
        Nodes = nodes;
    }

    public void AddNode(GraphNode node)
    {
        Nodes.Add(node);
    }

    public void AddNode(Room r)
    {
        Nodes.Add(new GraphNode(r));
    }

    public void AddDependency(GraphNode from, GraphNode to)
    {
        from.getNeighbours().Add(to);
    }

    public List<GraphNode> getNodes() {
        return Nodes;
    }
   
	
}
