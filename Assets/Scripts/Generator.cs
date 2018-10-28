using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Generator  {
    public static int maxroomsize = 20;
    public  static int minroomsize = 10;
    List<GameArea> partitions;
    public Room root;
    
    

  public class GameArea {

       public  int x, y, height, width;
       public   GameArea right;
       public GameArea left;
        

       public  GameArea(int xc, int yc, int w, int h) {
            this.x = xc;
            this.y = yc;
            this.height = h;
            this.width = w;
            left = null;
            right = null;


        }
       public  bool split()
        {
            
            int max = 0;
            if (left != null || right != null) {

                return false;
            }

            bool splith = Random.Range(0.0f, 1.0f) > 0.5f;
            if (width > height && width / height >= 1.25)
            {
                splith = false;
            }
            else if (height > width && height / width >= 1.25) {
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
            else {
                max = width - minroomsize;
                if (max <= minroomsize) {
                    return false;
                }


            }

            int split = Random.Range(minroomsize, max);
            if (splith == true)
            {
                left = new GameArea(x, y, width, split);
                right = new GameArea(x, y + split, width, height - split);

            }
            else {
                left = new GameArea(x, y, split, height);
                right = new GameArea(x + split, y, width - split, height);

            }
            return true;

    }


    }

    public void generate(int x, int y, int w, int h) {
        partitions = new List<GameArea>();
        GameArea root = new GameArea(x, y, w, h);
        partitions.Add(root);
       
        bool didsplit = true;
        while (didsplit) {
            didsplit = false;
            for (int i = partitions.Count-1; i>=0; i--) {
                
                  if (partitions[i].left == null &&partitions[i].right == null)
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

    public List<GameArea> getpartitions() {
        return partitions;


    }

    public void create(List<Generator.GameArea> list) {
       

    }

   

}
