using UnityEngine;

#if UNITY_EDITOR
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine.Tilemaps;

#endif

public class MapEditor 
{
#if UNITY_EDITOR
    [MenuItem("Tools/Generate Map")]
    private static void GenerateMap()
    {
        var gos = Resources.LoadAll<GameObject>("Prefabs/Map");
        foreach (var go in gos)
        {
            var tm = Util.FindChild<Tilemap>(go, "Tilemap_Collision", true);
            
            using var writer = File.CreateText($"Assets/Resources/Map/{go.name}.txt");
            writer.WriteLine(tm.cellBounds.xMin);
            writer.WriteLine(tm.cellBounds.xMax);
            writer.WriteLine(tm.cellBounds.yMin);
            writer.WriteLine(tm.cellBounds.yMax);

            for (var y = tm.cellBounds.yMax; y >= tm.cellBounds.yMin; y--)
            {
                for (var x = tm.cellBounds.xMin; x <= tm.cellBounds.xMax; x++)
                {
                    var tile = tm.GetTile(new Vector3Int(x, y, 0));
                    writer.Write(tile == null ? "0" : "1");
                }
                writer.WriteLine();
            }
        }
    }
#endif    
}
