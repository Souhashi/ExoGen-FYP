using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Door  {

    int place, map, length, offset;

    public Door(int p, int m, int l, int o)
    {
        place = p;
        map = m;
        length = l;
        offset = o;
    }

   public int getPlace() { return place; }
   public int getMap() { return map; }
    public int getLength() { return length; }

    public void CreateDoor(Tilemap t, TileBase r)
    {
        
        for (int i = offset; i < offset + length; i++)
        {
            Vector3Int pos = new Vector3Int(place, i, 0);
            t.SetTile(pos, r);
        }
        Debug.Log(place + ", " + map + ", " + length + ", " + offset);
    }

    public void DestroyDoor(Tilemap t)
    {
        for (int i = offset; i <offset + length; i++)
        {
            Vector3Int pos = new Vector3Int(place, i, 0);
            t.SetTile(pos, null);
        }
    }


}
