using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CreateBackground : MonoBehaviour {

    public List<Room> rooms = new List<Room>();
    public Tilemap tilemap;

    public void CreateRoom(int x, int y, int w, int h, Tile tile)
    {

        for (int i = x-1; i < w + x+1; i++)
        {
            for (int j = y-1; j < h + y+1; j++)
            {

                
                    tilemap.SetTile(new Vector3Int(i, j, 0), tile);
                
            }
        }

    }

    // Use this for initialization
    void Awake () {
        foreach (Room room in rooms)
        {

            //CreateRoom(room.position.x, room.position.y, room.width, room.height, room.bckg);
        }
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
