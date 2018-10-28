using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class UseGenerator : MonoBehaviour
{

    public Room room;
    public Generator test;
    public Tilemap tilemap;

    public class Generator
    {
        public static int maxroomsize = 10;
        public static int minroomsize = 5;
        List<GameArea> partitions;
        public Room root;



        public class GameArea
        {

            public int x, y, height, width;
            public GameArea right;
            public GameArea left;


            public GameArea(int xc, int yc, int w, int h)
            {
                this.x = xc;
                this.y = yc;
                this.height = h;
                this.width = w;
                left = null;
                right = null;


            }
            public bool split()
            {

                int max = 0;
                if (left != null || right != null)
                {

                    return false;
                }

                bool splith = Random.Range(0.0f, 1.0f) > 0.5f;
                if (width > height && width / height >= 1.25)
                {
                    splith = false;
                }
                else if (height > width && height / width >= 1.25)
                {
                    splith = true;

                }

                if (splith == true)
                {
                    max = height - minroomsize;
                    if (max <= minroomsize)
                    {
                        return false;

                    }
                }
                else
                {
                    max = width - minroomsize;
                    if (max <= minroomsize)
                    {
                        return false;
                    }


                }

                int split = Random.Range(minroomsize, max);
                if (splith == true)
                {
                    left = new GameArea(x, y, width, split);
                    right = new GameArea(x, y + split, width, height - split);

                }
                else
                {
                    left = new GameArea(x, y, split, height);
                    right = new GameArea(x + split, y, width - split, height);

                }
                return true;

            }


        }

        public void generate(int x, int y, int w, int h)
        {
            partitions = new List<GameArea>();
            GameArea root = new GameArea(x, y, w, h);
            partitions.Add(root);

            bool didsplit = true;
            while (didsplit)
            {
                didsplit = false;
                for (int i = partitions.Count - 1; i >= 0; i--)
                {

                    if (partitions[i].left == null && partitions[i].right == null)
                    {

                        if (partitions[i].width > maxroomsize || partitions[i].height > maxroomsize)
                        {
                            if (partitions[i].split())
                            {
                                partitions.Add(partitions[i].left);
                                partitions.Add(partitions[i].right);
                                didsplit = true;

                            }

                        }
                    }


                }


            }


        }

        public List<GameArea> getpartitions()
        {
            return partitions;


        }

    }

    public void CreateRoom(int x, int y, int w, int h, Tile tile)
    {

        for (int i = x; i <= w + x; i++)
        {
            for (int j = y; j <= h + y; j++)
            {

                if (i == x || j == y || i == w + x || j == h + y)
                {
                    if (!tilemap.HasTile(new Vector3Int(i, j, 0)))
                    {

                        tilemap.SetTile(new Vector3Int(i, j, 0), tile);
                    }


                }
            }
        }

    }

    public void CreateHorEntrances(int x, int y, int w, int h)
    {
        for (int i = x; i <= w + x; i++)
        {
            for (int j = y; j <= h + y; j++)
            {
                if (j == y || j == h + y)
                {

                    if (j == room.position.y || j == room.position.y + room.height)
                    {

                        continue;
                    }
                    else
                    {
                        tilemap.SetTile(new Vector3Int(x + 2, j, 0), null);
                        tilemap.SetTile(new Vector3Int(x + 3, j, 0), null);

                    }

                }

            }
        }

    }

    public void RandomizeEntrances(List<Generator.GameArea> alist)
    {

        for (int i = 0; i < 3; i++)
        {
            int index = Random.Range(0, alist.Count - 1);
            Generator.GameArea temp = alist[index];
            CreateHorEntrances(temp.x, temp.y, temp.width, temp.height);
        }

    }


    public void CreateEntrances(int x, int y, int w, int h)
    {


        for (int i = x; i <= w + x; i++)
        {
            for (int j = y; j <= h + y; j++)
            {


                if (i == x || i == w + x)
                {
                    if (i == room.position.x || i == room.position.x + room.width)
                    {
                        continue;
                    }
                    else
                    {

                        if (tilemap.HasTile(new Vector3Int(i - 1, y + 1, 0)) || tilemap.HasTile(new Vector3Int(i - 1, y + 2, 0)) ||

                            tilemap.HasTile(new Vector3Int(i + 1, y + 1, 0)) || tilemap.HasTile(new Vector3Int(i + 1, y + 2, 0)))
                        {
                            tilemap.SetTile(new Vector3Int(i, y + 3, 0), null);
                            tilemap.SetTile(new Vector3Int(i, y + 4, 0), null);
                        }

                        else
                        {
                            tilemap.SetTile(new Vector3Int(i, y + 1, 0), null);
                            tilemap.SetTile(new Vector3Int(i, y + 2, 0), null);


                        }

                    }
                }

            }

        }

    }
    // Use this for initialization

    void Awake()
    {
        test = new Generator();
        test.generate(room.position.x, room.position.y, room.width, room.height);
        foreach (Generator.GameArea a in test.getpartitions())
        {

           // CreateRoom(a.x, a.y, a.width, a.height, room.wall);
            CreateEntrances(a.x, a.y, a.width, a.height);
            Debug.Log("x: " + a.x);
            Debug.Log("y: " + a.y);
            Debug.Log("width: " + a.width);
            Debug.Log("height: " + a.height);



        }
        RandomizeEntrances(test.getpartitions());


    }

    // Update is called once per frame
    void Update()
    {

    }
}
