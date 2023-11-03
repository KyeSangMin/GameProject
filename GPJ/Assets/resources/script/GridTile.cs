using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridTile : MonoBehaviour
{
   
    private enum TileState 
    { 
        none,
        allypos,
        enemypos,
        obstacle

    }

    private TileState currentState;


    // Start is called before the first frame update
    void Start()
    {

        currentState = TileState.none;
    }

    // Update is called once per frame
    void Update()
    {

    }

    /*
    public void OnMouseDown()
    {
        getTIle();
        Debug.Log(getTIle());
    }
    */
    public GameObject getTIle()
    {
        return this.gameObject;
    }

    public void ChangeState(int state)
    {
        switch(state)
        {
            case 0:
                currentState = TileState.none;
                break;
            case 1:
                currentState = TileState.allypos;
                break;
            case 2:
                currentState = TileState.enemypos;
                break;
            case 3:
                currentState = TileState.obstacle;
                break;

        }

        

    }



}
