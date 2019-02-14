using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key  {

    int counter;
    public GameObject gameObject;
    Room room;

    public Key(int c)
    {
        counter = c;
    }

    public void setRoom(Room r)
    {
        room = r;
    }

    public Vector3Int Place()
    {
        Vector3Int position = new Vector3Int(room.position.x + 5, room.position.y + 5, 0);
        return position;
    }

    public int GetCounter()
    {
        return counter;
    }
	
}
