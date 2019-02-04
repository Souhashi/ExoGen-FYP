using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public class PlaceObjects : MonoBehaviour {
    Shuffle map;
    List<GraphNode> items= new List<GraphNode>();
    DependencyTree tree;
    List<GraphNode> sorted = new List<GraphNode>();
    List<GraphNode> active = new List<GraphNode>();
    int[] nodes;
    public GameObject CollectableGem;

    void InitialiseNodes()
    {
        map = GetComponent<Shuffle>();
        for (int i = 0; i < map.getclones().Count; i++)
        {
            for (int j = 0; j < map.getclones()[i].rooms.Count; j++)
            {

                items.Add(new GraphNode(map.getclones()[i].rooms[j]));

            }
        }
        tree = new DependencyTree(items);
    }

    void GenerateNodes()
    {
        nodes = new int[items.Count];
        for (int i = 0; i < nodes.Length; i++) { nodes[i] = i; }
    }

    int GenerateRandom(int lastnumber)
    {
        int rotate = Random.Range(0, items.Count-1);
        return (lastnumber + rotate) % items.Count;

    }
    //https://stackoverflow.com/questions/12790337/generating-a-random-dag
    void Dependencies()
    {
        StreamWriter writer = new StreamWriter("Test.txt", true);
        int ranks = items.Count;
        int max_per_rank = 3;
        int min_per_rank = 1;
        int chance = 30;
        int nodes = 0;
        writer.WriteLine("digraph G {");
        for (int i = 0; i < ranks; i++)
        {
            int new_nodes = Random.Range(min_per_rank, max_per_rank);

            for (int j = 0; j < nodes; j++)
            {
                for (int k = 0; k < new_nodes; k++)
                {
                    if (Random.Range(0, 100) < chance)
                    {
                        if (k + nodes >= items.Count || j >= items.Count) { continue; }
                        Debug.Log("From " + j +" " +"To " + (k + nodes));
                        tree.AddDependency(tree.getNodes()[j], tree.getNodes()[k + nodes]);
                        writer.WriteLine(j+" " + "->" +" "+ (k + nodes)+";");
                    }
                }
            }
            nodes += new_nodes;
        }
        writer.WriteLine("}");
        writer.Close();
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
            int numitems = Random.Range(1, 3);
            if (PickDep && sorted.Count > numitems)
            {
                
                for (int i = 0; i < numitems; i++)
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
            for (int i = 0; i < active.Count; i++)
            {
                active[i].g = Instantiate(CollectableGem, active[i].Place(), Quaternion.identity);
            }


        }
        else
        {
            return;
        }

    }
	// Use this for initialization
	void Start () {
        map= GetComponent<Shuffle>();
        InitialiseNodes();
        Dependencies();
        Sort();
        QueueNodes();
        foreach (GraphNode n in sorted) {
            Debug.Log("Node:"+ n.nodeID);
            
            
              
        }
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
