using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceObjects : MonoBehaviour {
    Shuffle map;
    List<GraphNode> items= new List<GraphNode>();
    DependencyTree tree;
    List<GraphNode> sorted = new List<GraphNode>();
    List<GraphNode> active = new List<GraphNode>();
    public GameObject CollectableGem;

    void InitialiseNodes()
    {
        map = GetComponent<Shuffle>();
        for (int i = 0; i< map.getclones()[2].rooms.Count; i++)
        {
          
            
                items.Add(new GraphNode(map.getclones()[2].rooms[i]));
            
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
    public void IsActive()
    {
        
            if (!AreAllObjsActive())
            {
                QueueNodes();

            }
        

    }

    bool AreAllObjsActive()
    {
        for (int i = 0; i < active.Count; i++)
        {
            if (active[i].g.activeSelf == true)
            {

                return true;
            }
        }
        return false;
    }

    public void QueueNodes()
    {
        if (sorted.Count != 0)
        {
            active.Clear();
            bool PickDep = (Random.value > 0.5);
            if (PickDep)
            {
                for (int i = 0; i < 2; i++)
                {
                    active.Add(sorted[i]);
                    sorted.RemoveAt(i);
                }
                
            }
            else
            {
                active.Add(sorted[0]);
                sorted.RemoveAt(0);
            }
           for (int i = 0; i< active.Count; i++)
            {
               active[i].g = Instantiate(CollectableGem, active[i].Place(), Quaternion.identity);
            }

        }

    }
	// Use this for initialization
	void Start () {
        map= GetComponent<Shuffle>();
        InitialiseNodes();
        Dependencies();
        Sort();
        QueueNodes();
        /*foreach (GraphNode n in sorted) {
            Debug.Log("Node:"+ n.nodeID);
            
            
                n.g = Instantiate(CollectableGem, n.Place(), Quaternion.identity);
            
            Debug.Log("Node: " + n.nodeID + ": " + n.g.activeSelf);
        }*/
	}
	
	// Update is called once per frame
	void Update () {
        IsActive();
        //Check if nodes in active array have active objects
        //If yes break, if not remove from active array
        //check if active array empty
        //if yes load the the next level of nodes to be instantiated.

		
	}
}
