using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;



public class Room 
{

    public Vector3Int position;
    public Vector3Int anchor;
    protected Vector3Int entrance;
    Vector3Int exit;
    public int width;
    public int height;
    public type Type;
    public enum type { Left, Right, TopLeft, TopRight, BottomLeft, BottomRight, DeadEnd};
    public int entranceoffset;
    public int entrancelength;
    public bool flipE;
    public bool hasStairs;
    public List<TemplateInfo> templateinfo;
    public List<bool> tilemaps;
    public class TemplateInfo
    {
        public List<TileBase> tiles;
        public List<Vector3Int> offset;
       public List<Matrix4x4> tiletransform;
        public List<Vector3Int> tileposition;

        public TemplateInfo() {
            tiles = new List<TileBase>();
            offset = new List<Vector3Int>();
            tiletransform = new List<Matrix4x4>();
            tileposition = new List<Vector3Int>();
        }
    }
    protected int numexits;
    protected List<Vector3Int> exits = new List<Vector3Int>();
    public List<int> offsets;
    public List<int> lengths;
    public bool[] isexit;
    public bool isHub;
    public Room(Vector3Int pos, Vector3Int a, int w, int h, int type, int eo, int el, bool fE, List<bool> tilemaps)
    {
        position = pos;
        anchor = a;
        width = w;
        height = h;
        Type = (type)type;
        entranceoffset = eo;
        entrancelength = el;
        flipE = fE;
        templateinfo = new List<TemplateInfo>();
        isHub = false;
        InitialiseTemplates(tilemaps.Count);
        this.tilemaps = tilemaps;
    }

    void InitialiseTemplates(int x) {
        for (int i = 0; i < x; i++) {
            templateinfo.Add(new TemplateInfo());
        }
    }

    public void ClearLists() {
        for (int i = 0; i < templateinfo.Count; i++) {
            templateinfo[i].offset.Clear();
            templateinfo[i].tileposition.Clear();
            templateinfo[i].tiles.Clear();
            templateinfo[i].tiletransform.Clear();
        }
    }

    public Vector3Int getexit()
    {
        return exit;
    }
    public List<Vector3Int> getexits()
    {
        return exits;
    }

    public Vector3Int getentrance() { return entrance; }

    public void SetPosition(Vector3Int newpos)
    {
        position = newpos;
    }

    public Vector3Int GetPos()
    {
        return position;
    }

    

   

    public bool Contains(Room room)
    {
        return (position.x <= room.position.x + room.width &&
            position.x + width >= room.position.x && position.y <= room.position.y + room.height &&
            position.y + height >= room.position.y);
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

    public virtual void SetExit()
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

    public string SaveToString()
    {
        return JsonUtility.ToJson(this);
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



