using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CreateRoom : MonoBehaviour
{


    public Tilemap currentTilemap;
    public RoomPallete rp;
    //public Map map;
    public Shuffle shuffle;
    

    // Use this for initialization
    void Start()
    {
        
        shuffle = GetComponent<Shuffle>();
        // shuffle.AlignX(map.rooms, map.rooms.Count - 2, map.rooms.Count - -1);
        // shuffle.Swap(map.rooms,  0, 5);
        foreach (Room r in shuffle.GetClone().rooms) 
        {
            int i = 0; 
            createRoom(r);
            CreateEntrances(r);
            // Debug.Log("Entrance: " + r.getentrance().x + ", " + r.getentrance().y);
            // Debug.Log("Exit: " + r.getexit().x + ", " + r.getexit().y);
            //Debug.Log("Position: " + r.position.x + ", " + r.position.y);
           // Debug.Log("Room:" + i + " Entrance: " + r.getentrance().x + ", " + r.getentrance().y);
           // Debug.Log("Room:" + i + " Exit: " + r.getexit().x + ", " + r.getexit().y);
           // r.SetTranslatePos(r.getentrance().x, r.getentrance().y);
           Debug.Log("Room:"+i+" List: " + r.GetOffset().Count );
            i++;
            

        }
       // map.rooms[0].SetTranslatePos(map.rooms[0].getentrance().x +1, map.rooms[0].getentrance().y);
       // Debug.Log("Position: " + map.rooms[0].position.x + ", " + map.rooms[0].position.y);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void createRoom(Room r)
    {
        Vector3Int position;
        //Matrix4x4 transform = Matrix4x4.TRS(Vector3Int.zero, Quaternion.Euler(0f, 0f, 180f), Vector3Int.one);
        for (int x = r.position.x; x < r.width + r.position.x; x++)
        {
            for (int y = r.position.y; y < r.height + r.position.y; y++)
            {
                position = new Vector3Int(x, y, 0);
                if (x == r.position.x)
                {
                    currentTilemap.SetTile(position, rp.wallL);

                }
                if (x == r.position.x + r.width - 1)
                {

                    currentTilemap.SetTile(position, rp.wallR);
                    // if (y != r.position.y + r.height - 1 || y != r.position.x + r.width - 1)
                    // {
                    // currentTilemap.SetTileFlags(position, TileFlags.None);
                    // currentTilemap.SetTransformMatrix(position, transform);
                    // }

                }
                if (y == r.position.y)
                {

                    currentTilemap.SetTile(position, rp.surface);
                }
                if (y == r.position.y + r.height - 1)
                {

                    currentTilemap.SetTile(position, rp.ceiling);
                }

            }
        }

        //Matrix4x4 transform = Matrix4x4.TRS(Vector3Int.zero, Quaternion.Euler(0f, 0f, 90f), Vector3Int.one);
        currentTilemap.SetTile(new Vector3Int(r.position.x, r.position.y, 0), rp.surfacecornerL);
        currentTilemap.SetTile(new Vector3Int(r.position.x, r.position.y + r.height - 1, 0), rp.wallcornerL);
        currentTilemap.SetTile(new Vector3Int(r.position.x + r.width - 1, r.position.y, 0), rp.surfacecornerR);
        currentTilemap.SetTile(new Vector3Int(r.position.x + r.width - 1, r.position.y + r.height - 1, 0), rp.wallcornerR);
    }

    public void CreateEntrances(Room r)
    {
        int entrance, length, ex, exlength;
        switch (r.Type)
        {
            case Room.type.Right:
                {
                    entrance = r.position.y + r.entranceoffset;
                    length = r.position.y + r.entranceoffset + r.entrancelength;

                    //r.SetEntrance();
                    //r.SetExit();

                    for (int i = entrance; i < length; i++)
                    {
                        currentTilemap.SetTile(new Vector3Int(r.position.x, i, 0), null);
                        currentTilemap.SetTile(new Vector3Int(r.position.x + r.width - 1, i, 0), null);
                    }


                    // r.SetExit(r.position.x + r.width, entrance - 1);


                    currentTilemap.SetTile(new Vector3Int(r.position.x, length, 0), rp.entrancet);
                    currentTilemap.SetTile(new Vector3Int(r.position.x + r.width - 1, length, 0), rp.exitT);
                    if (r.entranceoffset == 1)
                    {
                        currentTilemap.SetTile(new Vector3Int(r.position.x, entrance - 1, 0), rp.surface);
                        currentTilemap.SetTile(new Vector3Int(r.position.x + r.width - 1, entrance - 1, 0), rp.surface);
                    }
                    else
                    {
                        currentTilemap.SetTile(new Vector3Int(r.position.x, entrance - 1, 0), rp.entranceb);
                        currentTilemap.SetTile(new Vector3Int(r.position.x + r.width - 1, entrance - 1, 0), rp.exitb);
                    }



                    break;
                }

            case Room.type.Left:
                {

                    entrance = r.position.y + r.entranceoffset;
                    length = r.position.y + r.entrancelength + r.entranceoffset;
                   // r.SetEntrance();
                    //r.SetExit();
                    for (int i = entrance; i < length; i++)
                    {
                        currentTilemap.SetTile(new Vector3Int(r.position.x + r.width - 1, i, 0), null);
                        currentTilemap.SetTile(new Vector3Int(r.position.x, i, 0), null);
                    }
                    currentTilemap.SetTile(new Vector3Int(r.position.x, length, 0), rp.entrancet);
                    currentTilemap.SetTile(new Vector3Int(r.position.x + r.width - 1, length, 0), rp.exitT);
                    if (r.entranceoffset == 1)
                    {
                        currentTilemap.SetTile(new Vector3Int(r.position.x + r.width - 1, entrance - 1, 0), rp.surface);
                        currentTilemap.SetTile(new Vector3Int(r.position.x, entrance - 1, 0), rp.surface);
                    }
                    else
                    {
                        currentTilemap.SetTile(new Vector3Int(r.position.x + r.width - 1, entrance - 1, 0), rp.exitb);
                        currentTilemap.SetTile(new Vector3Int(r.position.x, entrance - 1, 0), rp.entranceb);
                    }
                    break;
                }
            case Room.type.TopRight:
                {

                    entrance = r.position.y + r.entranceoffset;
                    length = r.position.y + r.entranceoffset + r.entrancelength;
                    ex = r.position.y + r.height - 1 - r.entranceoffset;
                    exlength = r.position.y + r.height - 1 - r.entranceoffset - r.entrancelength;
                   // r.SetEntrance();
                   // r.SetExit();
                    for (int i = ex; i > exlength; i--)
                    {
                        currentTilemap.SetTile(new Vector3Int(r.position.x + r.width - 1, i, 0), null);
                    }
                    if (r.entranceoffset == 1)
                    {
                        currentTilemap.SetTile(new Vector3Int(r.position.x + r.width - 1, ex - 2, 0), rp.exitb);
                        currentTilemap.SetTile(new Vector3Int(r.position.x + r.width - 1, r.position.y + r.height - 1, 0), rp.ceiling);

                    }
                    else
                    {
                        currentTilemap.SetTile(new Vector3Int(r.position.x + r.width - 1, ex - 2, 0), rp.exitb);
                        currentTilemap.SetTile(new Vector3Int(r.position.x + r.width - 1, ex + 1, 0), rp.exitT);

                    }



                    if (!r.flipE)
                    {
                        for (int i = entrance; i < length; i++)
                        {
                            currentTilemap.SetTile(new Vector3Int(r.position.x, i, 0), null);

                        }
                        currentTilemap.SetTile(new Vector3Int(r.position.x, length, 0), rp.entrancet);
                        if (r.entranceoffset == 1)
                        {
                            currentTilemap.SetTile(new Vector3Int(r.position.x, entrance - 1, 0), rp.surface);

                        }
                        else
                        {
                            currentTilemap.SetTile(new Vector3Int(r.position.x, entrance - 1, 0), rp.entranceb);

                        }

                    }
                    else
                    {
                        for (int i = entrance; i < length; i++)
                        {
                            currentTilemap.SetTile(new Vector3Int(r.position.x + r.width - 1, i, 0), null);

                        }
                        currentTilemap.SetTile(new Vector3Int(r.position.x + r.width - 1, length, 0), rp.exitT);
                        if (r.entranceoffset == 1)
                        {
                            currentTilemap.SetTile(new Vector3Int(r.position.x + r.width - 1, entrance - 1, 0), rp.surface);

                        }
                        else
                        {
                            currentTilemap.SetTile(new Vector3Int(r.position.x + r.width - 1, entrance - 1, 0), rp.exitb);

                        }


                    }

                    break;
                }
            case Room.type.TopLeft:
                {
                    entrance = r.position.y + r.entranceoffset;
                    length = r.position.y + r.entranceoffset + r.entrancelength;
                    ex = r.position.y + r.height - 1 - r.entranceoffset;
                    exlength = r.position.y + r.height - 1 - r.entranceoffset - r.entrancelength;
                   // r.SetEntrance();
                   // r.SetExit();
                    for (int i = ex; i > exlength; i--)
                    {
                        currentTilemap.SetTile(new Vector3Int(r.position.x, i, 0), null);
                    }
                    if (r.entranceoffset == 1)
                    {
                        currentTilemap.SetTile(new Vector3Int(r.position.x, ex - 2, 0), rp.entranceb);
                        currentTilemap.SetTile(new Vector3Int(r.position.x, r.position.y + r.height - 1, 0), rp.ceiling);

                    }
                    else
                    {
                        currentTilemap.SetTile(new Vector3Int(r.position.x, ex - 2, 0), rp.entranceb);
                        currentTilemap.SetTile(new Vector3Int(r.position.x, ex + 1, 0), rp.entrancet);

                    }



                    if (!r.flipE)
                    {

                        for (int i = entrance; i < length; i++)
                        {
                            currentTilemap.SetTile(new Vector3Int(r.position.x + r.width - 1, i, 0), null);

                        }
                        currentTilemap.SetTile(new Vector3Int(r.position.x + r.width - 1, length, 0), rp.exitT);
                        if (r.entranceoffset == 1)
                        {
                            currentTilemap.SetTile(new Vector3Int(r.position.x + r.width - 1, entrance - 1, 0), rp.surface);

                        }
                        else
                        {
                            currentTilemap.SetTile(new Vector3Int(r.position.x + r.width - 1, entrance - 1, 0), rp.exitb);

                        }
                    }
                    else
                    {
                        for (int i = entrance; i < length; i++)
                        {
                            currentTilemap.SetTile(new Vector3Int(r.position.x, i, 0), null);

                        }
                        currentTilemap.SetTile(new Vector3Int(r.position.x, length, 0), rp.entrancet);
                        if (r.entranceoffset == 1)
                        {
                            currentTilemap.SetTile(new Vector3Int(r.position.x, entrance - 1, 0), rp.surface);

                        }
                        else
                        {
                            currentTilemap.SetTile(new Vector3Int(r.position.x, entrance - 1, 0), rp.entranceb);

                        }



                    }

                    break;
                }
            case Room.type.BottomRight:
                {
                    entrance = r.position.y + r.entranceoffset;
                    length = r.position.y + r.entranceoffset + r.entrancelength;
                    ex = r.position.y + r.height - 1 - r.entranceoffset;
                    exlength = r.position.y + r.height - 1 - r.entranceoffset - r.entrancelength;
                   // r.SetEntrance();
                   // r.SetExit();
                    for (int i = entrance; i < length; i++)
                    {
                        currentTilemap.SetTile(new Vector3Int(r.position.x + r.width - 1, i, 0), null);

                    }
                    currentTilemap.SetTile(new Vector3Int(r.position.x + r.width - 1, length, 0), rp.exitT);
                    if (r.entranceoffset == 1)
                    {
                        currentTilemap.SetTile(new Vector3Int(r.position.x + r.width - 1, entrance - 1, 0), rp.surface);

                    }
                    else
                    {
                        currentTilemap.SetTile(new Vector3Int(r.position.x + r.width - 1, entrance - 1, 0), rp.exitb);

                    }
                    if (!r.flipE)
                    {
                        for (int i = ex; i > exlength; i--)
                        {
                            currentTilemap.SetTile(new Vector3Int(r.position.x, i, 0), null);
                        }
                        if (r.entranceoffset == 1)
                        {
                            currentTilemap.SetTile(new Vector3Int(r.position.x, ex - 2, 0), rp.entranceb);
                            currentTilemap.SetTile(new Vector3Int(r.position.x, r.position.y + r.height - 1, 0), rp.ceiling);

                        }
                        else
                        {
                            currentTilemap.SetTile(new Vector3Int(r.position.x, ex - 2, 0), rp.entranceb);
                            currentTilemap.SetTile(new Vector3Int(r.position.x, ex + 1, 0), rp.entrancet);

                        }
                    }
                    else
                    {
                        for (int i = ex; i > exlength; i--)
                        {
                            currentTilemap.SetTile(new Vector3Int(r.position.x + r.width - 1, i, 0), null);
                        }
                        if (r.entranceoffset == 1)
                        {
                            currentTilemap.SetTile(new Vector3Int(r.position.x + r.width - 1, ex - 2, 0), rp.exitb);
                            currentTilemap.SetTile(new Vector3Int(r.position.x + r.width - 1, r.position.y + r.height - 1, 0), rp.ceiling);

                        }
                        else
                        {
                            currentTilemap.SetTile(new Vector3Int(r.position.x + r.width - 1, ex - 2, 0), rp.exitb);
                            currentTilemap.SetTile(new Vector3Int(r.position.x + r.width - 1, ex + 1, 0), rp.exitT);

                        }
                    }

                    break;
                }
            case Room.type.BottomLeft:
                {
                    entrance = r.position.y + r.entranceoffset;
                    length = r.position.y + r.entranceoffset + r.entrancelength;
                    ex = r.position.y + r.height - 1 - r.entranceoffset;
                    exlength = r.position.y + r.height - 1 - r.entranceoffset - r.entrancelength;
                  //  r.SetEntrance();
                  //  r.SetExit();
                    for (int i = entrance; i < length; i++)
                    {
                        currentTilemap.SetTile(new Vector3Int(r.position.x, i, 0), null);

                    }
                    currentTilemap.SetTile(new Vector3Int(r.position.x, length, 0), rp.entrancet);
                    if (r.entranceoffset == 1)
                    {
                        currentTilemap.SetTile(new Vector3Int(r.position.x, entrance - 1, 0), rp.surface);

                    }
                    else
                    {
                        currentTilemap.SetTile(new Vector3Int(r.position.x, entrance - 1, 0), rp.entranceb);

                    }

                    if (!r.flipE)
                    {
                        for (int i = ex; i > exlength; i--)
                        {
                            currentTilemap.SetTile(new Vector3Int(r.position.x + r.width - 1, i, 0), null);
                        }
                        if (r.entranceoffset == 1)
                        {
                            currentTilemap.SetTile(new Vector3Int(r.position.x + r.width - 1, ex - 2, 0), rp.exitb);
                            currentTilemap.SetTile(new Vector3Int(r.position.x + r.width - 1, r.position.y + r.height - 1, 0), rp.ceiling);

                        }
                        else
                        {
                            currentTilemap.SetTile(new Vector3Int(r.position.x + r.width - 1, ex - 2, 0), rp.exitb);
                            currentTilemap.SetTile(new Vector3Int(r.position.x + r.width - 1, ex + 1, 0), rp.exitT);

                        }
                    }
                    else
                    {
                        for (int i = ex; i > exlength; i--)
                        {
                            currentTilemap.SetTile(new Vector3Int(r.position.x, i, 0), null);
                        }
                        if (r.entranceoffset == 1)
                        {
                            currentTilemap.SetTile(new Vector3Int(r.position.x, ex - 2, 0), rp.entranceb);
                            currentTilemap.SetTile(new Vector3Int(r.position.x, r.position.y + r.height - 1, 0), rp.ceiling);

                        }
                        else
                        {
                            currentTilemap.SetTile(new Vector3Int(r.position.x, ex - 2, 0), rp.entranceb);
                            currentTilemap.SetTile(new Vector3Int(r.position.x, ex + 1, 0), rp.entrancet);

                        }

                    }

                    break;
                }
            case Room.type.DeadEnd:
                entrance = r.position.y + r.entranceoffset;
                length = r.position.y + r.entranceoffset + r.entrancelength;
               // r.SetEntrance();
               // r.SetExit();
                {
                    if (!r.flipE)
                    {
                        for (int i = entrance; i < length; i++)
                        {
                            currentTilemap.SetTile(new Vector3Int(r.position.x + r.width - 1, i, 0), null);

                        }
                        currentTilemap.SetTile(new Vector3Int(r.position.x + r.width - 1, length, 0), rp.exitT);
                        if (r.entranceoffset == 1)
                        {
                            currentTilemap.SetTile(new Vector3Int(r.position.x + r.width - 1, entrance - 1, 0), rp.surface);

                        }
                        else
                        {
                            currentTilemap.SetTile(new Vector3Int(r.position.x + r.width - 1, entrance - 1, 0), rp.exitb);

                        }

                    }
                    else
                    {
                        for (int i = entrance; i < length; i++)
                        {
                            currentTilemap.SetTile(new Vector3Int(r.position.x, i, 0), null);

                        }
                        currentTilemap.SetTile(new Vector3Int(r.position.x, length, 0), rp.entrancet);
                        if (r.entranceoffset == 1)
                        {
                            currentTilemap.SetTile(new Vector3Int(r.position.x, entrance - 1, 0), rp.surface);

                        }
                        else
                        {
                            currentTilemap.SetTile(new Vector3Int(r.position.x, entrance - 1, 0), rp.entranceb);

                        }


                    }
                    break;
                }


        }

    }
}