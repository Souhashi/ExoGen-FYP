using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class HubRoom : Room {
    public int numexits;
    List<Vector3Int> exits;
    public List<Vector3Int> offsets;
    public List<Vector3Int> lengths;

    public override void SetExit()
    {
    
    }
}
