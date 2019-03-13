using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Shuffle : MonoBehaviour
{
    //We need to make this better!
    public Map map;
    private Room room1, room2;
   
    public MapLoader gamemap;
    public List<Tilemap> tilemaps;
    List<Map> clones = new List<Map>();
    Map clone;
    
    /* public void InitializeBounds(Room r) {
         roombounds = new BoundsInt(new Vector3Int(0, 0, 0), new Vector3Int(r.width, r.height, 0));

     }*/

    public void ClearLists(Map m)
    {
        foreach (Room r in m.rooms)
        {
           r.ClearLists();
        }
            
    }
    public List<Map> getclones()
    {
        return clones;
    }

  

    public Map GetClone() { return clone; }

    void GetCoords(Room r)
    {
        Debug.Log("A"+r.anchor.ToString()+r.width+", "+r.height);
        
        Vector3Int position, stairposition;
       
        if (r.anchor != Vector3Int.zero)
        {
            for (int i = 0; i < r.tilemaps.Count; i++) {
                Debug.Log(r.tilemaps[i]);
                if (r.tilemaps[i]) {
                    for (int x = r.anchor.x; x <= r.anchor.x + r.width - 1; x++)
                    {
                        for (int y = r.anchor.y; y <= r.anchor.y + r.height - 1; y++)
                        {
                            position = new Vector3Int(x, y, 0);
                            stairposition = new Vector3Int(x, y, 0);
                            if (tilemaps[i].HasTile(position))
                            {
                                r.templateinfo[i].tiles.Add(tilemaps[i].GetTile(position));
                                r.templateinfo[i].tileposition.Add(position);
                                r.templateinfo[i].tiletransform.Add(tilemaps[i].GetTransformMatrix(position));
                                r.templateinfo[i].offset.Add(new Vector3Int(x - r.anchor.x, y - r.anchor.y, 0));

                            }

                        }

                    }

                }

            }
               
        }

     
       // Debug.Log("Stair tiles loaded: " + r.GetStairPosition().Count);
     
    }


    void ClearRoom(Room r)
    {
        for (int i = 0; i < r.tilemaps.Count; i++)
        {

            if (r.tilemaps[i])
            {
                Vector3Int position;
                for (int x = r.anchor.x; x < r.anchor.x + r.width; x++)
                {
                    for (int y = r.anchor.y; y < r.anchor.y + r.height; y++)
                    {
                        position = new Vector3Int(x, y, 0);
                        


                        tilemaps[i].SetTile(position, null);
                        


                    }

                }
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
    

   
   

    void Replace(Room r)
    {
        for (int i = 0; i < r.tilemaps.Count; i++)
        {

            if (r.tilemaps[i])
            {
                Debug.Log("!Position: " + r.position.x + ", " + r.position.y);
                for (int j = 0; j < r.templateinfo[i].tileposition.Count; j++)
                {
                    tilemaps[i].SetTile(new Vector3Int(r.position.x + r.templateinfo[i].offset[j].x, r.position.y + r.templateinfo[i].offset[j].y, 0), r.templateinfo[i].tiles[j]);
                    tilemaps[i].SetTransformMatrix(new Vector3Int(r.position.x + r.templateinfo[i].offset[j].x, r.position.y + r.templateinfo[i].offset[j].y, 0), r.templateinfo[i].tiletransform[j]);
                }
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
            Debug.Log(gamemap.GameLevel.allmaps[clone.map].rooms[clone.item].getexits().Count);


            if (clone.rooms[0].Type == Room.type.Right || clone.rooms[0].Type == Room.type.TopRight || clone.rooms[0].Type == Room.type.BottomRight)
            {
                clone.rooms[0].SetTranslatePos(gamemap.GameLevel.allmaps[clone.map].rooms[clone.item].getexits()[clone.exit].x + 1, gamemap.GameLevel.allmaps[clone.map].rooms[clone.item].getexits()[clone.exit].y);
                clone.rooms[0].SetEntrance();
                clone.rooms[0].SetExit();
            }
            else if (clone.rooms[0].Type == Room.type.Left || clone.rooms[0].Type == Room.type.TopLeft || clone.rooms[0].Type == Room.type.BottomLeft)
            {
                
                clone.rooms[0].SetTranslatePos(gamemap.GameLevel.allmaps[clone.map].rooms[clone.item].getexits()[clone.exit].x - 1, gamemap.GameLevel.allmaps[clone.map].rooms[clone.item].getexits()[clone.exit].y);
                clone.rooms[0].SetEntrance();
                clone.rooms[0].SetExit();
            }
        }
        else if (clone.isRoute == true && clone.isHubAdjacent == false)
        {
            if (clone.rooms[0].Type == Room.type.Right || clone.rooms[0].Type == Room.type.TopRight || clone.rooms[0].Type == Room.type.BottomRight || 
                (clone.rooms[0].Type == Room.type.DeadEnd && clone.rooms[0].flipE == true))
            {
                clone.rooms[0].SetTranslatePos(gamemap.GameLevel.allmaps[clone.map].rooms[clone.item].getexit().x + 1, gamemap.GameLevel.allmaps[clone.map].rooms[clone.item].getexit().y);
                clone.rooms[0].SetEntrance();
                clone.rooms[0].SetExit();
            }
            else if (clone.rooms[0].Type == Room.type.Left || clone.rooms[0].Type == Room.type.TopLeft || clone.rooms[0].Type == Room.type.BottomLeft|| 
                (clone.rooms[0].Type == Room.type.DeadEnd && clone.rooms[0].flipE == false))
            {
                clone.rooms[0].SetTranslatePos(gamemap.GameLevel.allmaps[clone.map].rooms[clone.item].getexit().x - 1, gamemap.GameLevel.allmaps[clone.map].rooms[clone.item].getexit().y);
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
     //   Debug.Log("Anchor:" + clones[clone.map].rooms[clone.item].position.x + ", " + clones[clone.map].rooms[clone.item].position.y);
        foreach (Room a in clone.rooms) {
            Debug.Log("Aligned Position $: " + a.position.x + ", " + a.position.y);
        }


    }


    // Use this for initialization
    void Awake()
    {
        //clones.Clear();
        gamemap = GetComponent<MapLoader>();
        Debug.Log(gamemap.GameLevel.allmaps.Count);

        foreach (Map m in gamemap.GameLevel.allmaps)
        {
            ClearLists(m);
            SetAllEntrances(m);
           // AlignRooms(m);
            GetRoomLayout(m);
            ClearRoomLayout(m);
        }
        foreach (Map cl in gamemap.GameLevel.allmaps)
        {
            Debug.Log(cl.rooms.Count);
            
            SetAllEntrances(cl);
            Selection(cl.rooms);
            SetAllEntrances(cl);
            AlignRooms(cl);
            SetRoomLayout(cl);
        }
        // Swap(clones[1].rooms, 0, 5 );
        // SetAllEntrances(clones[1]);
        // 
        

         
       

        Debug.Log("I'm still alive!!!");
    }

    // Update is called once per frame
    void Update()
    {

    }
}