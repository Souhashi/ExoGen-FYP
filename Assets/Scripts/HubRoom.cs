using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class HubRoom : Room {
   

    public override void SetExit()
    {
       
        switch (Type)
        {
            case type.Right:
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
