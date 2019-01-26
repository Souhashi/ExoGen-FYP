using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Shuffle : MonoBehaviour
{
    //We need to make this better!
    public Map map;
    private Room room1, room2;
    public Tilemap stairs;
    public Tilemap tilemap;
    public Tilemap layout;
    public GameMap gamemap;
    List<Map> clones = new List<Map>();
    Map clone;
    
    /* public void InitializeBounds(Room r) {
         roombounds = new BoundsInt(new Vector3Int(0, 0, 0), new Vector3Int(r.width, r.height, 0));

     }*/

    public void ClearLists(Map m)
    {
        foreach (Room r in map.rooms)
        {
            r.ClearLists();
        }
            
    }
    public List<Map> getclones()
    {
        return clones;
    }

    Map CopyMap(Map m)
    {
        clone = null;
        clone = Object.Instantiate(m) as Map;
        Room t;
        clone.rooms.Clear();
        foreach (Room r in m.rooms)
        {
            t = Object.Instantiate(r) as Room;
            clone.rooms.Add(t);
        }
        return clone;
        
    }

    public Map GetClone() { return clone; }

    void GetCoords(Room r)
    {
        r.GetPosition().Clear();
        Vector3Int position, stairposition;
        if (r.hasStairs)
        {
            r.InitialiseLists();
        }
            for (int x = r.position.x; x <= r.position.x + r.width-1; x++)
        {
            for (int y = r.position.y; y <= r.position.y + r.height-1; y++)
            {
                position = new Vector3Int(x, y, 0);
                stairposition = new Vector3Int(x, y, 0);
                if (layout.HasTile(position))
                {
                    r.GetPosition().Add(position);
                    r.GetTiles().Add(layout.GetTile(position));
                    r.GetTransform().Add(layout.GetTransformMatrix(position));
                    r.GetOffset().Add(new Vector3Int(x - r.position.x, y - r.position.y, 0));
                    

                }
                if (r.hasStairs)
                {

                    if (stairs.HasTile(stairposition))
                    {
                        r.GetStairPosition().Add(stairposition);
                        r.GetStairTiles().Add(stairs.GetTile(stairposition));
                        r.GetStairTransform().Add(stairs.GetTransformMatrix(stairposition));
                        r.GetStairOffset().Add(new Vector3Int(x - r.position.x, y - r.position.y, 0));
                    }

                }
            }

        }

        Debug.Log("Tiles loaded: "+r.GetPosition().Count);
       // Debug.Log("Stair tiles loaded: " + r.GetStairPosition().Count);
     
    }


    void ClearRoom(Room r)
    {
        Vector3Int position, stairposition;
        for (int x = r.position.x; x < r.position.x + r.width; x++)
        {
            for (int y = r.position.y; y < r.position.y + r.height; y++)
            {
                position = new Vector3Int(x, y, 0);
                stairposition = new Vector3Int(x, y, 0);
               
                
                    layout.SetTile(position, null);
                    stairs.SetTile(stairposition, null);
                

            }

        }

    }

    public void Align(IList<Room> r, int indexA, int indexB)
    {
       

        if (r[indexA].Type == Room.type.Right || r[indexA].Type == Room.type.TopRight || r[indexA].Type == Room.type.BottomRight || (r[indexA].Type == Room.type.DeadEnd && !r[indexA].flipE))
        {
            
        r[indexB].SetTranslatePos(r[indexA].getexit().x + 1, r[indexA].getexit().y);
            
        }
        else if (r[indexA].Type == Room.type.Left || r[indexA].Type == Room.type.TopLeft || r[indexA].Type == Room.type.BottomLeft || (r[indexA].Type == Room.type.DeadEnd && r[indexA].flipE))
        {
           
        r[indexB].SetTranslatePos(r[indexA].getexit().x - 1, r[indexA].getexit().y);
            
        }
      
        
        r[indexB].SetEntrance();
        r[indexB].SetExit();
        r[indexA].SetEntrance();
        r[indexA].SetExit();
       
    }
    

   
    void NewCoords(Room r, Room r1)
    {
        for (int i = 0; i < r.GetPosition().Count; i++)
        {
            layout.SetTile(new Vector3Int(r1.position.x + r.GetOffset()[i].x, r1.position.y + r.GetOffset()[i].y, 0), r.GetTiles()[i]);
            layout.SetTransformMatrix(new Vector3Int(r1.position.x + r.GetOffset()[i].x, r1.position.y + r.GetOffset()[i].y, 0), r.GetTransform()[i]);
        }
        if (r.hasStairs)
        {
            for (int j = 0; j < r.GetStairPosition().Count; j++)
            {
                stairs.SetTile(new Vector3Int(r1.position.x + r.GetStairOffset()[j].x, r1.position.y + r.GetStairOffset()[j].y, 0), r.GetStairTiles()[j]);
                stairs.SetTransformMatrix(new Vector3Int(r1.position.x + r.GetStairOffset()[j].x, r1.position.y + r.GetStairOffset()[j].y, 0), r.GetStairTransform()[j]);

            }

        }

    }

    void Replace(Room r)
    {
        Debug.Log("!Position: " + r.position.x + ", " + r.position.y);
        for (int i = 0; i < r.GetPosition().Count; i++)
        {
            layout.SetTile(new Vector3Int(r.position.x + r.GetOffset()[i].x, r.position.y + r.GetOffset()[i].y, 0), r.GetTiles()[i]);
            layout.SetTransformMatrix(new Vector3Int(r.position.x + r.GetOffset()[i].x, r.position.y + r.GetOffset()[i].y, 0), r.GetTransform()[i]);
        }
        if (r.hasStairs)
        {
            for (int j = 0; j < r.GetStairPosition().Count; j++)
            {
                stairs.SetTile(new Vector3Int(r.position.x + r.GetStairOffset()[j].x, r.position.y + r.GetStairOffset()[j].y, 0), r.GetStairTiles()[j]);
                stairs.SetTransformMatrix(new Vector3Int(r.position.x + r.GetStairOffset()[j].x, r.position.y + r.GetStairOffset()[j].y, 0), r.GetStairTransform()[j]);

            }

        }
    }

    bool CheckType(Room a, Room b) {
        if ((a.Type == Room.type.Right || a.Type == Room.type.TopRight || a.Type == Room.type.BottomRight)
            && (b.Type == Room.type.Right || b.Type == Room.type.TopRight || b.Type == Room.type.BottomRight) && (a.isHub == false && b.isHub == false) && (a.flipE == b.flipE))
        {
            return true;
        }
        else if ((a.Type == Room.type.Left || a.Type == Room.type.TopLeft || a.Type == Room.type.BottomLeft)
            && (b.Type == Room.type.Left || b.Type == Room.type.TopLeft || b.Type == Room.type.BottomLeft) && (a.isHub == false && b.isHub == false) && (a.flipE == b.flipE))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void Selection(IList<Room> temp)
    {
        Debug.Log("Selection is ran...");
        int n = temp.Count;
        while (n > 1)
        {
            n--;
            int k = Random.Range(0, n);
            Room r = temp[k];
            if (CheckType(r, temp[n]))
            {
                Debug.Log("Swapped..");
                temp[k] = temp[n];
                temp[n] = r;
            }
            else { Debug.Log("Skipped"); continue; }

        }
        


    }


    void GetRoomLayout(Map m)
    {
        foreach (Room r in m.rooms) {
            GetCoords(r);

        }
    }

    void ClearRoomLayout(Map m)
    {
        foreach (Room r in m.rooms) {
            ClearRoom(r);
        }
    }

    void SetRoomLayout(Map m)
    {
        foreach (Room r in m.rooms) {
            Replace(r);
        }

    }
    void SetAllEntrances(Map m)
    {
        for (int j = 0; j < m.rooms.Count; j++)
        {
            m.rooms[j].SetEntrance();
            m.rooms[j].SetExit();
        }

    }

    void AlignRooms(Map clone)
    {
        int hbc = 0;
        //Debug.Log("Anchor:" + clones[clone.map].rooms[clone.item].position.x + ", " + clones[clone.map].rooms[clone.item].position.y);
        if (clone.isHubAdjacent == true)
        {
            if (clone.rooms[0].Type == Room.type.Right || clone.rooms[0].Type == Room.type.TopRight || clone.rooms[0].Type == Room.type.BottomRight)
            {
                clone.rooms[0].SetTranslatePos(clones[clone.map].rooms[clone.item].getexits()[clone.exit].x + 1, clones[clone.map].rooms[clone.item].getexits()[clone.exit].y );
                clone.rooms[0].SetEntrance();
                clone.rooms[0].SetExit();
            }
            else if (clone.rooms[0].Type == Room.type.Left || clone.rooms[0].Type == Room.type.TopLeft || clone.rooms[0].Type == Room.type.BottomLeft)
            {
                clone.rooms[0].SetTranslatePos(clones[clone.map].rooms[clone.item].getexits()[clone.exit].x - 1, clones[clone.map].rooms[clone.item].getexits()[clone.exit].y);
                clone.rooms[0].SetEntrance();
                clone.rooms[0].SetExit();
            }
        }
        for (int i = 0; i < clone.rooms.Count; i++)
        {
            if (i >= 1)
            {
                
                int indexA = i - 1;
                int indexB = i;
               
                Debug.Log("HBC: " + hbc);
                Align(clone.rooms, indexA, indexB);
               
                Debug.Log((i - 1) + ": " + i);
                
            }

        }
        Debug.Log("Anchor:" + clones[clone.map].rooms[clone.item].position.x + ", " + clones[clone.map].rooms[clone.item].position.y);
        foreach (Room a in clone.rooms) {
            Debug.Log("Aligned Position $: " + a.position.x + ", " + a.position.y);
        }


    }


    // Use this for initialization
    void Awake()
    {
        clones.Clear();
        foreach (Map map in gamemap.allmaps)
        {
            Debug.Log("Shuffle is executed...");
            //CopyMap(map);
            
            clones.Add(CopyMap(map));
            
        }
        foreach (Map cl in clones)
        {
            ClearLists(cl);
            SetAllEntrances(cl);
            AlignRooms(cl);
            GetRoomLayout(cl);
            ClearRoomLayout(cl);
            SetAllEntrances(cl);
            Selection(cl.rooms);
            SetAllEntrances(cl);
            AlignRooms(cl);
            SetRoomLayout(cl);
        }
        // Swap(clones[1].rooms, 0, 5 );
        // SetAllEntrances(clones[1]);
        // 
        

        foreach (Map co in clones)
        {
            foreach (Room r in co.rooms)
            {
                Debug.Log(r);
            }
        }
       

        Debug.Log("I'm still alive!!!");
    }

    // Update is called once per frame
    void Update()
    {

    }
}