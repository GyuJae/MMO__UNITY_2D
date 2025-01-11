using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TestCollision : MonoBehaviour
{
    public Tilemap tilemap;
    
    void Start()
    {
        
    }

    void Update()
    {
        List<Vector3Int> blocked = new();

        foreach (var pos in tilemap.cellBounds.allPositionsWithin)
        {
            var tile = tilemap.GetTile(pos);
            if(tile != null) blocked.Add(pos);
        }
    }
    
}
