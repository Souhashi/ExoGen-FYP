using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


[CreateAssetMenu]
public class Room : ScriptableObject
{

    public Vector3Int position;
    Vector3Int entrance;
    Vector3Int exit;
    public int width;
    public int height;
    public type Type;
    public enum type { Left, Right, TopLeft, TopRight, BottomLeft, BottomRight, DeadEnd };
    public int entranceoffset;
    public int entrancelength;
    public bool flipE;
    List<TileBase> tiles = new List<TileBase>();
    List<Vector3Int> offset = new List<Vector3Int>();
    List<Matrix4x4> tiletransform = new List<Matrix4x4>();

    public Vector3Int getexit()
    {
        return exit;
    }

    public Vector3Int getentrance() { return entrance; }

    public List<TileBase> GetTiles() { return tiles; }

    public List<Vector3Int> GetOffset() { return offset; }

    public List<Matrix4x4> GetTransform() { return tiletransform; }

    public void SetPosition(Vector3Int newpos)
    {
        position = newpos;
    }

    public void SetEntrance()
    {
        switch (Type)
        {
            case type.Right:
                {
                    entrance = new Vector3Int(position.x, position.y + entranceoffset - 1, 0);
                    break;
                }
            case type.Left:
                {
                    entrance = new Vector3Int(position.x + width - 1, position.y + entranceoffset - 1, 0);
                    break;
                }
            case type.TopRight:
                {
                    if (!flipE)
                    {
                        entrance = new Vector3Int(position.x, position.y + entranceoffset - 1, 0);

                    }
                    else
                    {

                        entrance = new Vector3Int(position.x + width - 1, position.y + entranceoffset - 1, 0);
                    }
                    break;
                }
            case type.TopLeft:
                {
                    if (!flipE)
                    {
                        entrance = new Vector3Int(position.x + width - 1, position.y + entranceoffset - 1, 0);
                    }
                    else
                    {
                        entrance = new Vector3Int(position.x, position.y + entranceoffset - 1, 0);
                    }
                    break;
                }
            case type.BottomRight:
                {
                    if (!flipE)
                    {
                        entrance = new Vector3Int(position.x, position.y + height - 1 - entrancelength - entranceoffset, 0);
                    }
                    else
                    {
                        entrance = new Vector3Int(position.x + width - 1, position.y + height - 1 - entrancelength - entranceoffset, 0);
                    }
                    break;
                }
            case type.BottomLeft:
                {
                    if (!flipE)
                    {
                        entrance = new Vector3Int(position.x + width - 1, position.y + height - 1 - entrancelength - entranceoffset, 0);
                    }
                    else
                    {
                        entrance = new Vector3Int(position.x, position.y + height - 1 - entrancelength - entranceoffset, 0);
                    }
                    break;
                }
            case type.DeadEnd:
                {
                    if (!flipE)
                    {
                        entrance = new Vector3Int(position.x + width - 1, position.y + entranceoffset - 1, 0);
                    }
                    else
                    {
                        entrance = new Vector3Int(position.x, position.y + entranceoffset - 1, 0);
                    }
                    break;
                }
        }

        //entrance = new Vector3Int(r. y, 0);
    }

    public void SetExit()
    {

        switch (Type)
        {
            case type.Right:
                {
                    exit = new Vector3Int(position.x + width - 1, position.y + entranceoffset - 1, 0);
                    break;
                }
            case type.Left:
                {
                    exit = new Vector3Int(position.x, position.y + entranceoffset - 1, 0);
                    break;
                }
            case type.TopRight:
                {
                    exit = new Vector3Int(position.x + width - 1, position.y + height - 1 - entrancelength - entranceoffset, 0);
                    break;
                }
            case type.TopLeft:
                {
                    exit = new Vector3Int(position.x, position.y + height - 1 - entrancelength - entranceoffset, 0);
                    break;
                }
            case type.BottomRight:
                {
                    exit = new Vector3Int(position.x + width - 1, position.y + entranceoffset - 1, 0);
                    break;
                }
            case type.BottomLeft:
                {
                    exit = new Vector3Int(position.x, position.y + entranceoffset - 1, 0);
                    break;
                }
            case type.DeadEnd:
                {
                    if (!flipE)
                    {
                        exit = new Vector3Int(position.x + width - 1, position.y + entranceoffset - 1, 0);
                    }
                    else
                    {
                        exit = new Vector3Int(position.x, position.y + entranceoffset - 1, 0);
                    }
                    break;
                }

        }

    }

    public void SetTranslatePos(int x, int y)
    {
        switch (Type)
        {
            case type.Right:
                {
                    position = new Vector3Int(x, y - entranceoffset + 1, 0);
                    break;
                }
            case type.Left:
                {
                    position = new Vector3Int(x - width +1, y - entranceoffset + 1, 0);
                    break;
                }
            case type.TopRight:
                {
                    if (!flipE)
                    {
                        position = new Vector3Int(x, y - entranceoffset + 1, 0);

                    }
                    else
                    {

                        position = new Vector3Int(x - width + 1, y - entranceoffset + 1, 0);
                    }
                    break;
                }
            case type.TopLeft:
                {
                    if (!flipE)
                    {
                        position = new Vector3Int(x - width + 1, y - entranceoffset + 1, 0);
                    }
                    else
                    {
                        position = new Vector3Int(x, y - entranceoffset + 1, 0);
                    }

                    break;
                }
            case type.BottomRight:
                {
                    if (!flipE)
                    {
                        position = new Vector3Int(x, y - height + 1 + entrancelength + entranceoffset, 0);
                    }
                    else
                    {
                        position = new Vector3Int(x - width + 1, y - height + 1 + entrancelength + entranceoffset, 0);
                    }
                    break;
                }
            case type.BottomLeft:
                {
                    if (!flipE)
                    {
                        position = new Vector3Int(x - width + 1, y - height + 1 + entrancelength + entranceoffset, 0);
                    }
                    else
                    {
                        position = new Vector3Int(x, y - height + 1 + entrancelength + entranceoffset, 0);
                    }
                    break;
                }
            case type.DeadEnd:
                {
                    if (!flipE)
                    {
                        position = new Vector3Int(x - width + 1, y - entranceoffset + 1, 0);
                    }
                    else
                    {
                        position = new Vector3Int(x, y - entranceoffset + 1, 0);
                    }
                    break;
                }
        }

    }




}



