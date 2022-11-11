using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

class Ant {
    public Vector2Int pos; //current position
    public int mapWidth; 
    public Vector2Int heightRange;  //init y pos, y- and y+
    public List<Vector3Int> path; //x,y position at start, x distance
    public float tunnelOdds;
    public bool atEdge;
    public bool confirmed;

    public Ant(int x, int y, int verticalRange, float odds, int w) {
        pos = new Vector2Int(x,y);
        mapWidth = w;
        heightRange = new Vector2Int(y-verticalRange, y+verticalRange);
        path = new List<Vector3Int>();
        tunnelOdds = w;
        atEdge = false;
        confirmed = false;
    }
    public bool Tunnel() { return Random.Range(0f, 1f) < tunnelOdds; }
    public void Confirm() { confirmed = true; }
    public void Move() {
        //Debug.Log(pos[0] +" < "+ mapWidth);
        if (pos[0] < mapWidth) {
            int maxDist = (mapWidth - pos[0]);
            int longestDist = 4;
            maxDist = maxDist > longestDist ? longestDist : maxDist;

            int moveDist = Random.Range(2,maxDist+1);

            path.Add(new Vector3Int(pos[0], pos[1], moveDist));
            pos[0] += moveDist+1;
            pos[1] = Random.Range(heightRange[0], heightRange[1]+1);
        }
        else {
            atEdge = true;
            //Debug.Log("IM AT THE EDGE");
        }
    }

};

public class MapGenerator : MonoBehaviour
{
    public bool disabled = false;
    public GameObject FloorTilemapObject;
    public GameObject WallTilemapObject;
    private Tilemap FloorTilemap;
    private Tilemap WallTilemap;
    public TileBase WallTile;
    public TileBase FloorTile;

    public int width;
    public int height;
    public bool RenderFloor;
    public int numAnts;
    // Start is called before the first frame update
    void Start()
    {
        if (!disabled) {
        FloorTilemap = FloorTilemapObject.GetComponent<Tilemap>();
        WallTilemap = WallTilemapObject.GetComponent<Tilemap>();

        var map = GenerateArray(width, height);

        map = GenerateAntsWalk(map);
        

        //map = GenerateRandomWalk(map, width);
        RenderMap(map);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int[,] GenerateAntsWalk(int[,] map) {
        int fakeHeight =  height; //(int)(height/1.25f);
        List<Ant> ants = new List<Ant>();
        for (int i = 0; i < numAnts; i++) {
            ants.Add(new Ant(1, //x pos
                            (int)(((float)(fakeHeight-2)/(numAnts+1)) * (i+1) + Random.Range(-1,3)), //y pos
                            (int)((float)(fakeHeight-2)/(numAnts+1))-1, //y range (up and down)
                            1f, //tunnel odds
                            width-1));
        }

        //move ants:
        //CAN BE ASYC, multi-threading possible:
        int antsAtEdge = 0;
        //while (antsAtEdge < numAnts) {
        for (int k = 0; k < 20; k++) {
            for (int i = 0; i < numAnts; i++) {

                if (!ants[i].confirmed) {
                    if (!ants[i].atEdge) {
                        ants[i].Move();
                    }
                    else {
                        ants[i].Confirm();
                        antsAtEdge++;
                    }
                } //end if ant !confirmed
            } //end for
        } //end while
        Debug.Log(antsAtEdge);

        //decode ants paths into map
        for (int i = 0 ; i < numAnts; i++) {
            List<Vector3Int> path = ants[i].path;
            for (int j = 0; j < path.Count; j++) {
                Vector3Int line = path[j];
                int x = line[0], y = line[1];
                for (int h = 0; h < line[2]; h++) {
                    if (x+h >= width) { Debug.Log("LONGER"); }
                    if (y >= height) { Debug.Log("TALL:" +y); }
                    if (x+h < width && y < height) 
                        map[x+h,y] = 1;
                }
            }
        }
        return map;
    }
    public int[,] GenerateRandomWalk(int[,] map, int w)
    {
        int x = 0, y = map.GetUpperBound(1)/2;
        while (x < w-2) {
            //up, down or right
            if (y > map.GetUpperBound(1)/4 && Random.Range(0f,1f) < 1f/3) {
                //go down
                y--;
            }
            else if (y < map.GetUpperBound(1) - map.GetUpperBound(1)/4 && Random.Range(0f,1f) < 1f/3) {
                //go up
                y++;
            }
            else {
                x++;
            }
            map[x,y] = 1;
        }
        return map;
    }
    public void RenderMap(int[,] map)
    {
        //Clear the map (ensures we dont overlap)
        FloorTilemap.ClearAllTiles();
        WallTilemap.ClearAllTiles();
        //Loop through the width of the map
        int renderVal = RenderFloor ? 1 : 0;
        for (int x = 0; x < map.GetUpperBound(0) ; x++)
        {
            //Loop through the height of the map
            for (int y = 0; y < map.GetUpperBound(1); y++)
            {
                if (map[x, y] == 0)
                {
                    FloorTilemap.SetTile(new Vector3Int(x, y, 0), FloorTile);
                }
                else 
                {
                    WallTilemap.SetTile(new Vector3Int(x, y, 0), WallTile);
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
                map[x,y] = 0;
            }
        }
        return map;
    }
}
