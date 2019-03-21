using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;


public class MapLoader : MonoBehaviour
{
    string directorypath = "Assets/TextFiles";
    string line;
    public GameMap GameLevel = new GameMap();
    public class GameMap
    {
        public List<Map> allmaps;

        public GameMap()
        {
            allmaps = new List<Map>();
        }
    }

    public bool[] ParseBoolean(string s)
    {
        Debug.Log(s);
        string[] sp = s.Split(' ');
        bool[] booleans = new bool[sp.Length];
        for (int i = 0; i < sp.Length; i++)
        {
            booleans[i] = bool.Parse(sp[i]);
        }

        return booleans;
    }

    public List<int> ParseInts(string s)
    {
        string[] sp = s.Split(' ');
        List<int> ints = new List<int>();
        for (int i = 0; i < sp.Length; i++)
        {
            ints.Add(Int32.Parse(sp[i]));
        }

        return ints;

    }

    public List<bool> ParseBoolList(string s)
    {
        List<bool> b = new List<bool>();
        string[] t = s.Split(' ');
        for (int i = 0; i < t.Length; i++) { b.Add(bool.Parse(t[i])); }
        return b;
    }

    public bool ToBoolean(string f)
    {
        return Boolean.Parse(f);

    }

    public Vector3Int ToVector3(string d)
    {
        string[] v = d.Split(' ');
        return new Vector3Int(Int32.Parse(v[0]), Int32.Parse(v[1]), Int32.Parse(v[2]));
    }

    public int ToInt(string i)
    {
        return Int32.Parse(i);
    }

    public Map ParseMap(List<string> info)
    {
        string[] mapinfo = info[0].Split(',');
        Map newmap = new Map( ToInt(mapinfo[0]), ToInt(mapinfo[1]), ToInt(mapinfo[2]), ToBoolean(mapinfo[3]), ToBoolean(mapinfo[4]));
        for (int i = 1; i < info.Count; i++)
        {
            string[] rinfo = info[i].Split(',');
            if (rinfo[0] == "R")
            {
                newmap.rooms.Add(new Room(ToVector3(rinfo[1]), ToVector3(rinfo[2]), ToInt(rinfo[3]), ToInt(rinfo[4]), ToInt(rinfo[5]), 
                    ToInt(rinfo[6]), ToInt(rinfo[7]), ToInt(rinfo[8]), ToInt(rinfo[9]), ToBoolean(rinfo[10]), ParseBoolList(rinfo[11])));
            }
            if (rinfo[0] == "HR")
            {
                newmap.rooms.Add(new HubRoom(ToVector3(rinfo[1]), ToVector3(rinfo[2]), ToInt(rinfo[3]), ToInt(rinfo[4]), ToInt(rinfo[5]),
                    ToInt(rinfo[6]), ToInt(rinfo[7]), ToInt(rinfo[8]), ToInt(rinfo[9]), ToBoolean(rinfo[10]), ParseBoolList(rinfo[11]), ParseInts(rinfo[12]), ParseInts(rinfo[13]), ParseBoolean(rinfo[14])));

            }
         }

        Debug.Log(newmap.rooms.Count);
        return newmap;

    }




    public void Awake()
    {
        
        DirectoryInfo dir = new DirectoryInfo(directorypath);
        FileInfo[] info = dir.GetFiles("*.txt");
        List<string> lines = new List<string>();
        Debug.Log(info.Length);

        for (int j = 0; j < info.Length; j++)
        {
            lines.Clear();
            StreamReader file = new StreamReader(info[j].FullName);
            Debug.Log("File: " + info[j].FullName + "read...");
            while ((line = file.ReadLine()) != null)
            {
                string[] parts = line.Split('\t');
                for (int i = 0; i < parts.Length; i++)
                {
                    lines.Add(parts[i]);
                    //Debug.Log(parts[i]);
                }
            }
           // Debug.Log(lines.Count);
          
            file.Close();
            GameLevel.allmaps.Add(ParseMap(lines));
        }
        foreach (Map m in GameLevel.allmaps)
        {
            foreach (Room r in m.rooms)
            {
                Debug.Log(r.SaveToString());
            }
           
        }
    }
}
   
