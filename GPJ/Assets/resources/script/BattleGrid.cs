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
    //public GameObject[] grid;
    public List<GameObject> grid;

    [SerializeField]
    int totalgrids = 0;
    List<GameObject> ArountTile = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        totalgrids = this.transform.childCount;
        grids = new GameObject[GridY, GridX];


        for (int i = 0; i < GridY; i++)
        {
            for (int j = 1; j < GridX+1; j++)
            {
                grids[i, j-1] = this.transform.GetChild(i*9+j-1).gameObject;
                this.transform.GetChild(i * 9 + j - 1).gameObject.GetComponent<GridTile>().setTileXY(j-1,i);
            }

           // Debug.Log(grids[i, 5]);
            
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*
   private void LoadMapData()
    {

    }
    */
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

    public void resetList()
    {
        ArountTile = new List<GameObject>();
    }

    public GameObject FindGridTIle(int x, int y)
    {
        GameObject currentgrid = grids[y, x];
        return currentgrid;
    }
}

