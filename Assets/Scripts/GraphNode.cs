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
    public GameObject g;

    public GraphNode(Room room)
    {
        counter++;
        nodeID = counter;
        this.room = room;
        neighbours = new List<GraphNode>();
        state = State.White;
    }

    public Vector3Int Place()
    {
       // bool isValid = false;
        position = new Vector3Int(Random.Range(room.position.x+2, room.position.x + room.width-1), Random.Range(room.position.y+2, room.position.y + room.height-1), 0);
        foreach (Vector3Int p in room.GetPosition())
        {
            if (position == p) {
                position = new Vector3Int(Random.Range(room.position.x + 2, room.position.x + room.width - 1), Random.Range(room.position.y + 2, room.position.y + room.height - 1), 0);
            }
        }
        Debug.Log("RPos: " + room.position.x + ", " + room.position.y);      
        Debug.Log("Pos: " + position.x + ", " + position.y);
        return position;
    }

    

    public List<GraphNode> getNeighbours()
    {
        return neighbours;
    }

    public GameObject GetGameObject() {
        return g;
    }
}
