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
    Map clone;
    /* public void InitializeBounds(Room r) {
         roombounds = new BoundsInt(new Vector3Int(0, 0, 0), new Vector3Int(r.width, r.height, 0));

     }*/

    void CopyMap(Map m)
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
        Debug.Log(clone.rooms.Count);
        foreach (Room r in clone.rooms) {
            Debug.Log("Position: " + r.position.x + ", " + r.position.y);
        }
        foreach (Room r in map.rooms) {
            Debug.Log("Position: " + r.position.x + ", " + r.position.y);
        }
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
                if (layout.HasTile(position))
                {
                    layout.SetTile(position, null);
                }
                else if (stairs.HasTile(stairposition))
                {
                    stairs.SetTile(stairposition, null);
                }

            }

        }

    }

    public void Align(IList<Room> r, int indexA, int indexB)
    {

        if (r[indexB].position.x > r[indexA].position.x)
        {
            r[indexB].SetTranslatePos(r[indexA].getexit().x + 1, r[indexA].getexit().y);
        }
        else if (r[indexB].position.x < r[indexA].position.x)
        {
            r[indexB].SetTranslatePos(r[indexA].getexit().x - 1, r[indexA].getexit().y);
        }
        else if (r[indexB].position.x == r[indexA].getexit().x - 1)
        {
            r[indexB].SetTranslatePos(r[indexA].getexit().x + 2, r[indexA].getexit().y);

        }
        else if (r[indexB].position.x == r[indexA].getexit().x + 1)
        {
            r[indexB].SetTranslatePos(r[indexA].getexit().x - 2, r[indexA].getexit().y);

        }
        r[indexB].SetEntrance();
        r[indexB].SetExit();
        r[indexA].SetEntrance();
        r[indexA].SetExit();
        foreach (Room a in r) {
            Debug.Log("Aligned Position: " + a.position.x + ", " + a.position.y);
        }
        //Debug.Log("Y Offset: " + yOffset);

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

    void Selection()
    {

        //bool hasmatched = false;
        int index = Random.Range(0, map.rooms.Count - 1);
        room1 = map.rooms[index];
        Debug.Log(index);
        for (int i = 0; i < map.rooms.Count; i++)
        {

            if (map.rooms[i].Type == room1.Type && index != i)
            {
                room2 = map.rooms[i];
                Debug.Log(i);
                break;
            }

        }


    }

    public void Swap(IList<Room> temp1, int index, int index1)
    {
      
        int tempwidth = temp1[index].width;
        int tempheight = temp1[index].height;


        temp1[index].SetEntrance();
        temp1[index].SetExit();
        temp1[index1].SetEntrance();
        temp1[index1].SetExit();

        temp1[index].width = temp1[index1].width;
        temp1[index1].width = tempwidth;

        temp1[index].height = temp1[index1].height;
        temp1[index1].height = tempheight;
        
    }

    void SetAllEntrances(Map m)
    {
        for (int j = 0; j < m.rooms.Count; j++)
        {
            m.rooms[j].SetEntrance();
            m.rooms[j].SetExit();
        }

    }


    // Use this for initialization
    void Awake()
    {

        CopyMap(map);

        //Swap(clone.rooms, 0, 2);
        SetAllEntrances(clone);
        Swap(clone.rooms, 0, 2);
        SetAllEntrances(clone);
        for (int i = 0; i < map.rooms.Count; i++)
        {
            if (i >= 1)
            {
                int indexA = i - 1;
                int indexB = i;

                Align(clone.rooms, indexA, indexB);

                Debug.Log((i - 1) + ": " + i);
            }

        }
        GetCoords(clone.rooms[0]);
        GetCoords(clone.rooms[2]);
        ClearRoom(clone.rooms[0]);
        ClearRoom(clone.rooms[2]);
        NewCoords(clone.rooms[2], clone.rooms[0]);
        NewCoords(clone.rooms[0], clone.rooms[2]);
        
       /* foreach (Room r in clone.rooms)
        {
            Debug.Log("Final Clone Position: " + r.position.x + ", " + r.position.y);
        }
        foreach (Room r in map.rooms)
        {
            Debug.Log("Final Original Position: " + r.position.x + ", " + r.position.y);
        }*/

        // Debug.Log(places.Count);
        // InitializeBounds(rooms.room[1]);

        Debug.Log("I'm still alive!!!");
    }

    // Update is called once per frame
    void Update()
    {

    }
}