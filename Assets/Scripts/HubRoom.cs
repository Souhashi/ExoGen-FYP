using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HubRoom : Room {

    public HubRoom(Vector3Int pos, Vector3Int a, int w, int h, int type, int eo, int el, bool fE, bool hS, List<int> off, List<int> lengths, bool[] bools) 
        : base(pos, a, w, h, type, eo, el, fE, hS)
    {
        offsets = off;
        this.lengths = lengths;
        isexit = bools;
        isHub = true;
    }
   
    public override void SetExit()
    {
        Debug.Log("Hubroom exits have been set...");
        exits.Clear();
        Debug.Log(isexit[2]);
        switch (Type)
        {
            case type.Right:
                if (isexit.Length == 3 && isexit[0] == true && isexit[1] == true)
                {
                    exits.Add(new Vector3Int(position.x, position.y + height - 1 - lengths[0] - offsets[0], 0));
                    exits.Add(new Vector3Int(position.x + width - 1, position.y + height - 1 - lengths[1] - offsets[1], 0));
                    Debug.Log(exits.Count);
                }
                if (isexit.Length == 3 && isexit[1] == true && isexit[2] == true)
                {
                    exits.Add(new Vector3Int(position.x + width - 1, position.y + height - 1 - lengths[1] - offsets[1], 0));
                    exits.Add(new Vector3Int(position.x + width - 1, position.y + offsets[2] - 1, 0));
                    Debug.Log(exits.Count);
                }
                if (isexit.Length == 3 && isexit[0] == true && isexit[2] == true)
                {
                    exits.Add(new Vector3Int(position.x, position.y + height - 1 -lengths[0] - offsets[0], 0));
                    exits.Add(new Vector3Int(position.x + width - 1, position.y + offsets[2] - 1, 0));
                    Debug.Log(exits.Count);
                }
                if (isexit.Length == 3 && isexit[0] == true && isexit[1] == true && isexit[2] == true)
                {
                    exits.Add(new Vector3Int(position.x, position.y + height - 1 - lengths[0] - offsets[0], 0));
                    exits.Add(new Vector3Int(position.x + width - 1, position.y + height - 1 - lengths[1] - offsets[1], 0));
                    exits.Add(new Vector3Int(position.x + width - 1, position.y + offsets[2] - 1, 0));
                    Debug.Log(exits.Count);
                }
                Debug.Log(exits.Count);
                break;
            case type.TopRight:
                if (isexit.Length == 3 && isexit[0] == true && isexit[1] == true)
                {
                    exits.Add(new Vector3Int(position.x, position.y + height - 1 - entrancelength - entranceoffset, 0));
                    exits.Add(new Vector3Int(position.x + width - 1, position.y + height - 1 - entrancelength - entranceoffset, 0));
                }
                if (isexit.Length == 3 && isexit[1] == true && isexit[2] == true)
                {
                    exits.Add(new Vector3Int(position.x + width - 1, position.y + height - 1 - entrancelength - entranceoffset, 0));
                    exits.Add(new Vector3Int(position.x + width - 1, position.y + entranceoffset - 1, 0));
                }
                if (isexit.Length == 3 && isexit[0] == true && isexit[2] == true)
                {
                    exits.Add(new Vector3Int(position.x, position.y + height - 1 - entrancelength - entranceoffset, 0));
                    exits.Add(new Vector3Int(position.x + width - 1, position.y + entranceoffset - 1, 0));
                }
                if (isexit.Length == 3 && isexit[0] == true && isexit[1] == true && isexit[2] == true)
                {
                    exits.Add(new Vector3Int(position.x, position.y + height - 1 - entrancelength - entranceoffset, 0));
                    exits.Add(new Vector3Int(position.x + width - 1, position.y + height - 1 - entrancelength - entranceoffset, 0));
                    exits.Add(new Vector3Int(position.x + width - 1, position.y + entranceoffset - 1, 0));
                }
                break;
            case type.BottomRight:
                if (isexit.Length == 3 && isexit[0] == true && isexit[1] == true)
                {
                    exits.Add(new Vector3Int(position.x + width - 1, position.y + height - 1 - entrancelength - entranceoffset, 0));
                    exits.Add(new Vector3Int(position.x + width - 1, position.y + entranceoffset - 1, 0));
                }
                if (isexit.Length == 3 && isexit[1] == true && isexit[2] == true)
                {
                    exits.Add(new Vector3Int(position.x + width - 1, position.y + entranceoffset - 1, 0));
                    exits.Add(new Vector3Int(position.x, position.y + entranceoffset - 1, 0));
                }
                if (isexit.Length == 3 && isexit[0] == true && isexit[2] == true)
                {
                    exits.Add(new Vector3Int(position.x + width - 1, position.y + height - 1 - entrancelength - entranceoffset, 0));
                    exits.Add(new Vector3Int(position.x, position.y + entranceoffset - 1, 0));
                }
                if (isexit.Length == 3 && isexit[0] == true && isexit[1] == true && isexit[2] == true)
                    {
                    exits.Add(new Vector3Int(position.x + width - 1, position.y + height - 1 - entrancelength - entranceoffset, 0));
                    exits.Add(new Vector3Int(position.x + width - 1, position.y + entranceoffset - 1, 0));
                    exits.Add(new Vector3Int(position.x, position.y + entranceoffset - 1, 0));
                }
                break;
            case type.Left:
                if (isexit.Length == 3 && isexit[0] == true && isexit[1] == true)
                {
                    exits.Add(new Vector3Int(position.x, position.y + entranceoffset - 1, 0));
                    exits.Add(new Vector3Int(position.x, position.y + height - 1 - entrancelength - entranceoffset, 0));
                }
                if (isexit.Length == 3 && isexit[1] == true && isexit[2] == true)
                {
                    exits.Add(new Vector3Int(position.x, position.y + height - 1 - entrancelength - entranceoffset, 0));
                    exits.Add(new Vector3Int(position.x + width - 1, position.y + height - 1 - entrancelength - entranceoffset, 0));
                }
                if (isexit.Length == 3 && isexit[0] == true && isexit[2] == true)
                {
                    exits.Add(new Vector3Int(position.x, position.y + entranceoffset - 1, 0));
                    exits.Add(new Vector3Int(position.x + width - 1, position.y + height - 1 - entrancelength - entranceoffset, 0));
                }
                if (isexit.Length == 3 && isexit[0] == true && isexit[1] == true && isexit[2] == true)
                {
                    exits.Add(new Vector3Int(position.x, position.y + entranceoffset - 1, 0));
                    exits.Add(new Vector3Int(position.x, position.y + height - 1 - entrancelength - entranceoffset, 0));
                    exits.Add(new Vector3Int(position.x + width - 1, position.y + height - 1 - entrancelength - entranceoffset, 0));
                }
                break;
            case type.TopLeft:
                if (isexit.Length == 3 && isexit[0] == true && isexit[1] == true)
                {
                    exits.Add(new Vector3Int(position.x, position.y + entranceoffset - 1, 0));
                    exits.Add(new Vector3Int(position.x, position.y + height - 1 - entrancelength - entranceoffset, 0));
                }
                if (isexit.Length == 3 && isexit[1] == true && isexit[2] == true)
                {
                    exits.Add(new Vector3Int(position.x, position.y + height - 1 - entrancelength - entranceoffset, 0));
                    exits.Add(new Vector3Int(position.x + width - 1, position.y + height - 1 - entrancelength - entranceoffset, 0));
                }
                if (isexit.Length == 3 && isexit[0] == true && isexit[2] == true)
                {
                    exits.Add(new Vector3Int(position.x, position.y + entranceoffset - 1, 0));
                    exits.Add(new Vector3Int(position.x + width - 1, position.y + height - 1 - entrancelength - entranceoffset, 0));
                }
                if (isexit.Length == 3 && isexit[0] == true && isexit[1] == true && isexit[2] == true)
                {
                    exits.Add(new Vector3Int(position.x, position.y + entranceoffset - 1, 0));
                    exits.Add(new Vector3Int(position.x, position.y + height - 1 - entrancelength - entranceoffset, 0));
                    exits.Add(new Vector3Int(position.x + width - 1, position.y + height - 1 - entrancelength - entranceoffset, 0));
                }
                break;
            case type.BottomLeft:
                if (isexit.Length == 3 && isexit[0] == true && isexit[1] == true)
                {
                    exits.Add(new Vector3Int(position.x, position.y + entranceoffset - 1, 0));
                    exits.Add(new Vector3Int(position.x, position.y + height - 1 - entrancelength - entranceoffset, 0));
                }
                if (isexit.Length == 3 && isexit[1] == true && isexit[2] == true)
                {
                    exits.Add(new Vector3Int(position.x, position.y + entranceoffset - 1, 0));
                    exits.Add(new Vector3Int(position.x + width - 1, position.y + entranceoffset - 1, 0));
                }
                if (isexit.Length == 3 && isexit[0] == true && isexit[2] == true)
                {
                    exits.Add(new Vector3Int(position.x, position.y + height - 1 - entrancelength - entranceoffset, 0));
                    exits.Add(new Vector3Int(position.x + width - 1, position.y + entranceoffset - 1, 0));
                }
                if (isexit.Length == 3 && isexit[0] == true && isexit[1] == true && isexit[2] == true)
                {
                    exits.Add(new Vector3Int(position.x, position.y + entranceoffset - 1, 0));
                    exits.Add(new Vector3Int(position.x + width - 1, position.y + entranceoffset - 1, 0));
                    exits.Add(new Vector3Int(position.x, position.y + height - 1 - entrancelength - entranceoffset, 0));
                }
               
                break;
                
        }
    }
}
