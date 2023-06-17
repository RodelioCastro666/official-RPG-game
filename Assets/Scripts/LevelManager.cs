using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    private Transform map;

    [SerializeField]
    private Texture2D[] mapData;

    [SerializeField]
    private MapElement[] mapElements;

    [SerializeField]
    private Sprite deafaultTile;

    private Dictionary<Point, GameObject> waterTiles = new Dictionary<Point, GameObject>();

    private Vector3 WorldStartPos
    {
        get 
        {
            return Camera.main.ScreenToWorldPoint(new Vector3(0, 0));
        }
    }

    void Start()
    {
        
        GenerateMap();
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void GenerateMap() 
    {
        int height = mapData[0].height;
        int width = mapData[0].width;

        for (int i = 0; i < mapData.Length; i++)
        {
            for (int x = 0; x < mapData[i].width; x++)
            {
                for (int y = 0; y < mapData[i].height; y++)
                {
                    Color c = mapData[i].GetPixel(x, y);
                     
                    MapElement newElement = Array.Find(mapElements, e => e.MyColor == c);

                    if (newElement != null)
                    {
                        float xPos = WorldStartPos.x + (deafaultTile.bounds.size.x * x);
                        float yPos = WorldStartPos.y + (deafaultTile.bounds.size.y * y);

                        GameObject go = Instantiate(newElement.MyElementPrefab);
                        go.transform.position = new Vector2(xPos, yPos);

                        if (newElement.MyTileTag == "Water")
                        {
                            waterTiles.Add(new Point(x,y),go);
                        }

                        if (newElement.MyTileTag == "Tree")
                        {
                            go.GetComponent<SpriteRenderer>().sortingOrder = height*2 - y*2 ;
                        }

                        go.transform.parent = map;  
                    }
                }
            }
        }
        CheckWater();
    }

    private void CheckWater()
    {
        foreach(KeyValuePair<Point,GameObject> tile in waterTiles)
        {
            string composition = TileCheck(tile.Key);
        }
    }

    public string TileCheck(Point currentPoint)
    {
        string composition = string.Empty;

        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if (x != 0 || y != 0)
                {
                    if (waterTiles.ContainsKey(new Point(currentPoint.MyX+x, currentPoint.MyY+y)))
                    {
                        composition += "W";
                    }
                    else
                    {
                        composition += "E";
                    }
                }
            }
        }

        Debug.Log(composition);
        return composition;
    }


   

}

[Serializable]
public class MapElement
{
    [SerializeField]
    private string tileTag;

    [SerializeField]
    private Color color;

    [SerializeField]
    private GameObject elementPrefab;

    public GameObject MyElementPrefab { get => elementPrefab; }

    public Color MyColor { get => color; }

    public string MyTileTag { get => tileTag; }
}

public struct Point
{
    public int MyX { get; set;  }
    public int MyY { get; set; }

    public Point(int x, int y)
    {
        this.MyX = x;
        this.MyY = y;
    }

}

