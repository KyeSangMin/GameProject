using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[Serializable]
public class Grid : MonoBehaviour
{
   
   

    private int GridX = 9;
    private int GridY = 6;


    [SerializeField]
    public GameObject[,] grids;

    [SerializeField]
    public GameObject[] grid;

    [SerializeField]
    int totalgrids = 0;

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
                Debug.Log(grids[i, j-1]);
            }

           // Debug.Log(grids[i, 5]);
            
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

   
}

