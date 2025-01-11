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
        var gos = Resources.LoadAll<GameObject>("Prefabs/Maps");
        foreach (var go in gos)
        {
            var tBase = Util.FindChild<Tilemap>(go, "Tilemap_Base", true);
            var tm = Util.FindChild<Tilemap>(go, "Tilemap_Collision", true);
            
            using var writer = File.CreateText($"Assets/Resources/Maps/{go.name}.txt");
            writer.WriteLine(tBase.cellBounds.xMin);
            writer.WriteLine(tBase.cellBounds.xMax);
            writer.WriteLine(tBase.cellBounds.yMin);
            writer.WriteLine(tBase.cellBounds.yMax);

            for (var y = tBase.cellBounds.yMax; y >= tBase.cellBounds.yMin; y--)
            {
                for (var x = tBase.cellBounds.xMin; x <= tBase.cellBounds.xMax; x++)
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
