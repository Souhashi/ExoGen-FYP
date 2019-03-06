using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GenerateDoors : MonoBehaviour {

    public Tilemap walls;
    public Shuffle shuffle;
    public Tile tile;
    List<Door> doors = new List<Door>();
    List<Key> keys = new List<Key>();
    public GameObject CollectableGem;
    

void InitialiseDoors()
    {
        shuffle = GetComponent<Shuffle>();
        for (int i = 1; i < shuffle.gamemap.GameLevel.allmaps.Count; i++)
        {
            bool chance = true;//(Random.value > 0.5);
            if (chance) { 


                doors.Add(new Door(shuffle.gamemap.GameLevel.allmaps[i].rooms[0].getentrance().x, i, shuffle.gamemap.GameLevel.allmaps[i].rooms[0].entrancelength, 
                    shuffle.gamemap.GameLevel.allmaps[i].rooms[0].getentrance().y+1));
            }
        }
        Debug.Log(doors.Count);
       

    }

    void DrawDoors()
    {
        foreach (Door d in doors)
        {
            d.CreateDoor(walls, tile);
        }
    }

    public void CreateBarriers()
    {
       InitialiseDoors();
        DrawDoors();
    }

    public void GenerateKeys()
    {
        if (doors.Count != 0)
        {
            for (int i = 0; i < doors.Count; i++)
            {
                int mapindex = Random.Range(0, doors[i].getMap());
             Room r = shuffle.gamemap.GameLevel.allmaps[mapindex].rooms[Random.Range(0, shuffle.gamemap.GameLevel.allmaps[mapindex].rooms.Count)];
                Key k = new Key(i);
                k.setRoom(r);
                keys.Add(k);

            }
        }
        Debug.Log("Keys generated...");
    }

    public void PlaceKeys()
    {
        
        for (int i = 0; i < keys.Count; i++)
        {
            keys[i].gameObject = Instantiate(CollectableGem, keys[i].Place(), Quaternion.identity);
        }
        Debug.Log("Keys placed...");
        foreach (Key k in keys)
        {
            Debug.Log(k.gameObject);
        }
    }

    public Key CheckIfCollected()
    {
        if (keys.Count != 0)
        {
            for (int i = 0; i < keys.Count; i++)
            {
                if (keys[i].gameObject.activeSelf == false)
                {
                    Key k = keys[i];
                    keys.RemoveAt(i);
                    return k;
                }
            }
        }
        return null;
    }

    public void UnlockDoor()
    {
        Key k = CheckIfCollected();
        if (k != null)
        {
            doors[k.GetCounter()].DestroyDoor(walls);
            Debug.Log("Door:" + k.GetCounter() + " destroyed");
           // k = null;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Wooohoooo");
    }

    // Use this for initialization
    void Start () {
        //  InitialiseDoors();
        //  DrawDoors();
       // GenerateKeys();
       // PlaceKeys();
	}
	
	// Update is called once per frame
	void Update () {
        UnlockDoor();
	}
}
