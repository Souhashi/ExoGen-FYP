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

    public void ClearLists(Map m)
    {
        foreach (Room r in map.rooms)
        {
            r.ClearLists();
        }
            { }
    }

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
               
                
                    layout.SetTile(position, null);
                    stairs.SetTile(stairposition, null);
                

            }

        }

    }

    public void Align(IList<Room> r, int indexA, int indexB)
    {

        if (r[indexA].Type == Room.type.Right || r[indexA].Type == Room.type.TopRight || r[indexA].Type == Room.type.BottomRight)
        {
            r[indexB].SetTranslatePos(r[indexA].getexit().x + 1, r[indexA].getexit().y);
        }
        else if (r[indexA].Type == Room.type.Left || r[indexA].Type == Room.type.TopLeft || r[indexA].Type == Room.type.BottomLeft)
        {
            r[indexB].SetTranslatePos(r[indexA].getexit().x - 1, r[indexA].getexit().y);
        }
       /* else if (r[indexB].position.x == r[indexA].getexit().x - 1)
        {
            r[indexB].SetTranslatePos(r[indexA].getexit().x - 2, r[indexA].getexit().y);

        }
        else if (r[indexB].position.x == r[indexA].getexit().x + 1)
        {
            r[indexB].SetTranslatePos(r[indexA].getexit().x + 2, r[indexA].getexit().y);

        }*/
        
        r[indexB].SetEntrance();
        r[indexB].SetExit();
        r[indexA].SetEntrance();
        r[indexA].SetExit();
        /*foreach (Room a in r) {
            Debug.Log("Aligned Position $: " + a.position.x + ", " + a.position.y);
        }*/
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

    void Selection(IList<Room> temp)
    {

        int room1, room2;
        for (int i = 0; i < 15; i++)
        {
            room1 = Random.Range(0, temp.Count);
            
            room2 = Random.Range(0, temp.Count);
            
            if (temp[room1].Type == temp[room2].Type && room1 != room2 && temp[room1].flipE == temp[room2].flipE)
            {
                //AlignRooms();
                if (room1 > room2)
                {
                    Debug.Log("Index1: " + room1);
                    Debug.Log("Index2: " + room2);
                    Swap1(temp, room2, room1);
                    
                }
                else
                {
                    Debug.Log("EIndex1: " + room1);
                    Debug.Log("EIndex2: " + room2);
                    Swap(temp, room2, room1);
                }
               // AlignRooms();
            }
            else { Debug.Log("Iteration skipped");  continue; }

        }


    }

    public void Swap(IList<Room> temp1, int index, int index1)
    {

        

        Room temp = temp1[index];
        Vector3Int pos = temp1[index1].position;
        int tempeo = temp1[index1].entranceoffset;
        temp1[index] = temp1[index1];
        temp1[index1] = temp;
        temp1[index1].SetPosition(temp1[index].position);
        temp1[index].SetPosition(pos);
       // temp1[index1].entranceoffset = temp1[index].entranceoffset;
       // temp1[index1].entranceoffset = tempeo;
        
        temp1[index].SetEntrance();
        temp1[index].SetExit();
        temp1[index1].SetEntrance();
        temp1[index1].SetExit();


        // NewCoords(temp1[index], temp1[index1]);
        // NewCoords(temp1[index1], temp1[index]);

    }

    public void Swap1(IList<Room> temp1, int index, int index1)
    {
        Room temp = temp1[index];
        Vector3Int pos = temp1[index].position;
        int tempeo = temp1[index1].entranceoffset;
        temp1[index] = temp1[index1];
        temp1[index] = temp;
        
        // temp1[index1].entranceoffset = temp1[index].entranceoffset;
        // temp1[index1].entranceoffset = tempeo;

        temp1[index].SetEntrance();
        temp1[index].SetExit();
        temp1[index1].SetEntrance();
        temp1[index1].SetExit();
    }

    void GetRoomLayout()
    {
        foreach (Room r in clone.rooms) {
            GetCoords(r);

        }
    }

    void ClearRoomLayout()
    {
        foreach (Room r in clone.rooms) {
            ClearRoom(r);
        }
    }

    void SetRoomLayout()
    {
        foreach (Room r in clone.rooms) {
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

    void AlignRooms()
    {
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

        foreach (Room a in clone.rooms) {
            Debug.Log("Aligned Position $: " + a.position.x + ", " + a.position.y);
        }


    }


    // Use this for initialization
    void Awake()
    {
        Debug.Log("Shuffle is executed...");
        CopyMap(map);
        ClearLists(clone);
        SetAllEntrances(clone);
        AlignRooms();
        GetRoomLayout();
        ClearRoomLayout();
        //Swap(clone.rooms, 9, 2);
        SetAllEntrances(clone);
        Selection(clone.rooms);
        //Swap(clone.rooms, 8, 2);
        SetAllEntrances(clone);
        AlignRooms();
        SetRoomLayout();


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