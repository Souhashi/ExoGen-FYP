using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphNode  {

    private Vector3Int position;
   
    private Room room;
    private List<GraphNode> neighbours;
    public static int counter = 0;
    public int nodeID = 0;
    public enum State {White, Grey, Black}
    public State state;

    public GraphNode(Room room)
    {
        counter++;
        nodeID = counter;
        this.room = room;
        neighbours = new List<GraphNode>();
        state = State.White;
    }

    public void Place()
    {
        bool isValid = false;
        
        while (!isValid)
        {
            foreach (Vector3Int pos in room.GetPosition())
            {
                position = new Vector3Int(Random.Range(room.position.x+1, room.position.x + room.width-1), Random.Range(room.position.y+1, room.position.y + room.height-1), 0);
                if (position != pos)
                {
                    isValid = true;
                }
            }
        }
    }

    public List<GraphNode> getNeighbours()
    {
        return neighbours;
    }
}
