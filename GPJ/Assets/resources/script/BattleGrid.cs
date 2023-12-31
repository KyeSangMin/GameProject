using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[Serializable]
public class BattleGrid : MonoBehaviour
{
   
   

    private int GridX = 9;
    private int GridY = 6;


    [SerializeField]
    public GameObject[,] grids;

    [SerializeField]
    public List<GameObject> grid;

    [SerializeField]
    int totalgrids = 0;
    List<GameObject> ArountTile = new List<GameObject>();
    private CSVParser csvParser;

    private void Awake()
    {
        csvParser = this.GetComponent<CSVParser>();
        totalgrids = this.transform.childCount;
    }
    // Start is called before the first frame update
    void Start()
    {
        if(grids == null)
        {
            grids = new GameObject[GridY, GridX];
        } 
        List<List<string>> mapData = csvParser.getMapdata();
        for (int i = 0; i < GridY; i++)
        {
            List<string> row = mapData[i];
            row.Reverse();
            for (int j = 1; j < GridX+1; j++)
            {
                grids[i, j-1] = this.transform.GetChild(i*9+j-1).gameObject;
                this.transform.GetChild(i * 9 + j - 1).gameObject.GetComponent<GridTile>().setTileXY(j-1,i);
                grid.Add(grids[i, j - 1]);
                LoadMapData(grids[i, j - 1], row[j-1]);
              
            }

        }

    }
   private void LoadMapData(GameObject grid, string data)
    {
        if (data == "0")
        {
            grid.GetComponent<GridTile>().ChangeState(GridTile.TileState.None);

        }
        else if (data == "1")
        {
            grid.GetComponent<GridTile>().ChangeState(GridTile.TileState.Allypos);
        }
        else if (data == "2")
        {
            grid.GetComponent<GridTile>().ChangeState(GridTile.TileState.Enemypos);
        }

    }
    
    public List<GameObject> SearchTile(GameObject starttile, int range)
    {      
        int Xcoordinate = starttile.GetComponent<GridTile>().getTileX();
        int Ycoordinate = starttile.GetComponent<GridTile>().getTileY();
        
        for (int j = Ycoordinate - range; j <= Ycoordinate + range; j++)
        {
            int temp = j;
            if (j > 5 || j < 0)
            {
                j = Mathf.Clamp(j, 0, 5);
            }
            if (!ArountTile.Contains(grids[j, Xcoordinate]))
            {
                ArountTile.Add(grids[j, Xcoordinate]);        
            }
            j = temp;
        }
        for (int i = Xcoordinate - range; i <= Xcoordinate + range; i++)
        {
            int temp = i;
            if(i > 8 || i < 0)
            {
                i = Mathf.Clamp(i, 0, 8);
            }         
            if(!ArountTile.Contains(grids[Ycoordinate,i ]))
            {
                ArountTile.Add(grids[Ycoordinate,i]);
            }
            i = temp;
        }
        return ArountTile;
    }

    public void resetTileState()
    {
        for (int i = 0; i < GridY; i++)
        {
            for (int j = 1; j < GridX + 1; j++)
            {
                grids[i, j - 1].GetComponent<GridTile>().ChangeMoveTileState(GridTile.MoveTile.CantTile);
                grids[i, j - 1].GetComponent<GridTile>().resetTileAni();
            }

        }
    }

    public void resetList()
    {
        ArountTile = new List<GameObject>();
    }

    public GameObject FindGridTIle(int x, int y)
    {
        GameObject currentgrid = grids[y, x];
        return currentgrid;
    }

    public List<GameObject> FindPosTile(GridTile.TileState tileState)
    {
        List<GameObject> tempTileList = new List<GameObject>();

        for (int i = 0; i < GridY; i++)
        {
            for (int j = 1; j < GridX + 1; j++)
            {
                if(grids[i, j - 1].GetComponent<GridTile>().getState() == tileState)
                {
                    tempTileList.Add(grids[i, j - 1]);
                }          
            }
        }
        return tempTileList;
    }
}

