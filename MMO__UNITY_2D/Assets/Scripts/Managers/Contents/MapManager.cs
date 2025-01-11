using System.IO;
using UnityEngine;

public class MapManager
{
   public Grid CurrentGrid { get; set; }
   
   public int MinX { get; set; }
   public int MaxX { get; set; }
   public int MinY { get; set; }
   public int MaxY { get; set; }

   bool[,] _collision;

   public bool CanGo(Vector3Int cellPos)
   {
      if(cellPos.x < MinX || cellPos.x > MaxX) return false;
      if(cellPos.y < MinY || cellPos.y > MaxY) return false;
      
      var x = cellPos.x - MinX;
      var y = MaxY - cellPos.y; 
      return !_collision[y, x];
   }
   
   public void LoadMap(int mapId)
   {
      DestoryMap();

      var mapName = "Map_" + mapId.ToString("000");
      var mapPath = $"Maps/{mapName}";
      var go = Managers.Resource.Instantiate(mapPath);
      go.name = mapName;

      var collision = Util.FindChild(go, "Tilemap_Collision", true);
      if(collision != null) collision.SetActive(false);
      
      CurrentGrid = go.GetComponent<Grid>();
      
      var txt = Managers.Resource.Load<TextAsset>($"Maps/{mapName}");
      var reader = new StringReader(txt.text);
      
      MinX = int.Parse(reader.ReadLine());
      MaxX = int.Parse(reader.ReadLine());
      MinY = int.Parse(reader.ReadLine());
      MaxY = int.Parse(reader.ReadLine());

      var xCount = MaxX - MinX + 1;
      var yCount = MaxY - MinY + 1;
      
      _collision = new bool[yCount, xCount];

      for (var y = 0; y < yCount; y++)
      {
         var line = reader.ReadLine();
         for (var x = 0; x < xCount; x++)
         {
            if (line != null)
               _collision[y, x] = line[x] == '1';
         }
      }
   }

   public void DestoryMap()
   {
      var map = GameObject.Find("Map");
      if (map == null) return;
      Object.Destroy(map);
      CurrentGrid = null;
   }
}
