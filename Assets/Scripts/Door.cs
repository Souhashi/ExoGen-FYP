using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Door  {

    int place, map, length, offset;
    GameObject g;
    
    public enum state {Locked, Unlocked };
    state State;

    public Door(int p, int m, int l, int o)
    {
        place = p;
        map = m;
        length = l;
        offset = o;
       
        State = state.Locked;
    }

   public int getPlace() { return place; }
   public int getMap() { return map; }
    public int getLength() { return length; }

   /* void setUpCollider(Tilemap t)
    {
        boxcollider = g.GetComponent<BoxCollider2D>();
        boxcollider.transform.position = new Vector3Int(place-1, offset, 0);
        boxcollider.isTrigger = true;
        boxcollider.size.Set(t.cellSize.x* 2, t.cellSize.x*2);
        
    }*/
    public void CreateDoor(Tilemap t, TileBase r)
    {
        
        for (int i = offset; i < offset + length; i++)
        {
            Vector3Int pos = new Vector3Int(place, i, 0);
            t.SetTile(pos, r);
            
        }
        //setUpCollider(t);
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

    

    public void SetUnlocked()
    {
        State = state.Unlocked;
    }

    public state GetState()
    {
        return State;
    }


}
