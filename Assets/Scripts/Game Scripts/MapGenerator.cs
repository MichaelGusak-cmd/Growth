using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapGenerator : MonoBehaviour
{
    public GameObject tilemapObject;
    private Tilemap tilemap;
    public TileBase WallTile;
    public TileBase FloorTile;

    public int width;
    public int height;
    // Start is called before the first frame update
    void Start()
    {
        tilemap = tilemapObject.GetComponent<Tilemap>();
        RenderMap(GenerateArray(width, height));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RenderMap(int[,] map)
    {
        //Clear the map (ensures we dont overlap)
        tilemap.ClearAllTiles();
        //Loop through the width of the map
        for (int x = 0; x < map.GetUpperBound(0) ; x++)
        {
            //Loop through the height of the map
            for (int y = 0; y < map.GetUpperBound(1); y++)
            {
                if (map[x, y] == 1)
                {
                    tilemap.SetTile(new Vector3Int(x, y, 0), WallTile);
                }
                else 
                {
                    tilemap.SetTile(new Vector3Int(x, y, 0), FloorTile);
                }
            }
        }
    }
    public int[,] GenerateArray(int w, int h)
    {
        int[,] map = new int[w, h];
        for (int x = 0; x < map.GetUpperBound(0); x++)
        {
            for (int y = 0; y < map.GetUpperBound(1); y++)
            {
                map[x,y] = Random.Range(0,2);
            }
        }
        return map;
    }
}
