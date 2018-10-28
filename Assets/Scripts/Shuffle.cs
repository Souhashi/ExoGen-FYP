using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Shuffle : MonoBehaviour
{
    //We need to make this better!
    public Map map;
    private Room room1, room2;
    private List<Vector3Int> places = new List<Vector3Int>();
    private List<Vector3Int> places1 = new List<Vector3Int>();
    private List<Vector3Int> offset = new List<Vector3Int>();
    private List<Vector3Int> offset1 = new List<Vector3Int>();
    private List<TileBase> Tiles = new List<TileBase>();
    private List<TileBase> Tiles1 = new List<TileBase>();
    private List<Matrix4x4> transform1 = new List<Matrix4x4>();
    private List<Matrix4x4> transform2 = new List<Matrix4x4>();
    private List<Vector3Int> entrance = new List<Vector3Int>();
    private List<Vector3Int> exit = new List<Vector3Int>();
    enum Direction { Up, Down, Left, Right, Aligned, LeftColliding, RightColliding };


    public Tilemap tilemap;

    /* public void InitializeBounds(Room r) {
         roombounds = new BoundsInt(new Vector3Int(0, 0, 0), new Vector3Int(r.width, r.height, 0));

     }*/

    void GetCoords(Room r, List<Vector3Int> p, List<TileBase> t, List<Matrix4x4> tr)
    {
        // Debug.Log(r.entrance.x + " " + r.entrance.y);
        //  Debug.Log(r.exit.x + " " + r.exit.y);
        Vector3Int position;
        for (int x = r.position.x; x < r.position.x + r.width; x++)
        {
            for (int y = r.position.y; y < r.position.y + r.height; y++)
            {
                position = new Vector3Int(x, y, 0);
                if (tilemap.HasTile(position))
                {

                    p.Add(position);
                    t.Add(tilemap.GetTile(position));
                    tr.Add(tilemap.GetTransformMatrix(position));

                }



            }

        }
        Debug.Log(entrance.Count);
        Debug.Log(exit.Count);
        //Debug.Log(entrance2.x + " " + entrance2.y);
        //Debug.Log(exit2.x + " " + exit2.y);

    }


    void ClearRoom(Room r)
    {

        for (int x = r.position.x; x < r.position.x + r.width; x++)
        {
            for (int y = r.position.y; y < r.position.y + r.height; y++)
            {
                if (tilemap.HasTile(new Vector3Int(x, y, 0)))
                {
                    tilemap.SetTile(new Vector3Int(x, y, 0), null);
                }

            }

        }

    }

    void CalculateOffset(Room r, List<Vector3Int> o, List<Vector3Int> pc)
    {

        for (int i = 0; i < pc.Count; i++)
        {
            o.Add(new Vector3Int(pc[i].x - r.position.x, pc[i].y - r.position.y, 0));

        }

    }

    int CalculateEOf(int y1, int y)
    {

        return Mathf.Abs(y1 - y);
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
       
        r[indexB].SetEntrance();
        r[indexB].SetExit();
        r[indexA].SetEntrance();
        r[indexA].SetExit();
        
        //Debug.Log("Y Offset: " + yOffset);

    }
    

   
    void NewCoords(Room r, List<Vector3Int> ps, List<Vector3Int> of, List<TileBase> tl, List<Matrix4x4> tr1)
    {

        for (int i = 0; i < ps.Count; i++)
        {

            tilemap.SetTile(new Vector3Int(r.position.x + of[i].x, r.position.y + of[i].y, 0), tl[i]);
            tilemap.SetTransformMatrix(new Vector3Int(r.position.x + of[i].x, r.position.y + of[i].y, 0), tr1[i]);
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
        //  Debug.Log("Position: " + r1.x + ", " + r1.y);
        //  Debug.Log("Position: " + r2.x + ", " + r2.y);
        Vector3Int temp = temp1[index].position;
        temp1[index].position = temp1[index1].position;
        temp1[index1].position = temp;
        temp1[index].SetEntrance();
        temp1[index].SetExit();
        temp1[index1].SetEntrance();
        temp1[index1].SetExit();
        // one.SetPosition(r2);
        // two.SetPosition(r1);

        //  Debug.Log("Position: " + r1.x + ", " + r1.y);
        //  Debug.Log("Position: " + r2.x + ", " + r2.y);
    }

    


    // Use this for initialization
    void Awake()
    {


     
        //shuffle.Swap(map.rooms, 1, 4);
        for (int j = 0; j < map.rooms.Count; j++)
        {
            map.rooms[j].SetEntrance();
            map.rooms[j].SetExit();
        }
        for (int i = 0; i < map.rooms.Count; i++)
        {
            if (i >= 1)
            {
                int indexA = i - 1;
                int indexB = i;


                Align(map.rooms, indexA, indexB);

                //shuffle.AlignY(map.rooms, indexA, indexB);
                //shuffle.CheckCollision(map.rooms, indexA, indexB);

                Debug.Log((i - 1) + ": " + i);
            }

        }

        Debug.Log(places.Count);
        // InitializeBounds(rooms.room[1]);

        Debug.Log("I'm still alive!!!");
    }

    // Update is called once per frame
    void Update()
    {

    }
}