using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Map  {
   
    public List<Room> rooms;
       
    public int map, item, exit;
    public bool isHubAdjacent;
    public bool isRoute;


    public Map(int map, int item, int exit, bool iha, bool ir)
    {
        rooms = new List<Room>();
        this.map = map;
        this.item = item;
        this.exit = exit;
        isHubAdjacent = iha;
        isRoute = ir;
    }

    public string SaveToString()
    {
        return JsonUtility.ToJson(this);
    }
}
