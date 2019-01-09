using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class HubRoom : Room {

   
    public override void SetExit()
    {
        exits.Clear();
        switch (Type)
        {
            case type.Right:
                if (isexit.Length == 3 && isexit[0] == true && isexit[1] == true)
                {
                    exits.Add(new Vector3Int(position.x, position.y + height - 1 - lengths[0] - offsets[0], 0));
                    exits.Add(new Vector3Int(position.x + width - 1, position.y + height - 1 - lengths[1] - offsets[1], 0));
                    Debug.Log(exits[0].x + ", " + exits[0].y);
                }
                if (isexit.Length == 3 && isexit[1] == true && isexit[2] == true)
                {
                    exits.Add(new Vector3Int(position.x + width - 1, position.y + height - 1 - lengths[1] - offsets[1], 0));
                    exits.Add(new Vector3Int(position.x + width - 1, position.y + offsets[2] - 1, 0));
                }
                if (isexit.Length == 3 && isexit[0] == true && isexit[2] == true)
                {
                    exits.Add(new Vector3Int(position.x, position.y + height - 1 -lengths[0] - offsets[0], 0));
                    exits.Add(new Vector3Int(position.x + width - 1, position.y + offsets[2] - 1, 0));
                }
                if (isexit.Length == 3 && isexit[0] == true && isexit[1] == true && isexit[2] == true)
                {
                    exits.Add(new Vector3Int(position.x, position.y + height - 1 - lengths[0] - offsets[0], 0));
                    exits.Add(new Vector3Int(position.x + width - 1, position.y + height - 1 - lengths[1] - offsets[1], 0));
                    exits.Add(new Vector3Int(position.x + width - 1, position.y + offsets[2] - 1, 0));
                }
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
