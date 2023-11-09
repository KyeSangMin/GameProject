using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridTile : MonoBehaviour
{
   
    public enum TileState 
    { 
        none,
        allypos,
        enemypos,
        obstacle

    }
    [SerializeField]
    private TileState currentState;
    [SerializeField]
    private int TileX;
    [SerializeField]
    private int TileY;

    // Start is called before the first frame update
    void Start()
    {

        //currentState = TileState.none;
    }

    // Update is called once per frame
    void Update()
    {

    }

    
    public void setTileXY(int x, int y)
    {
        TileX = x;
        TileY = y;
    }
    
    public int getTileX()
    {
        return TileX;
    }
    public int getTileY()
    {
        return TileY;
    }

    public GameObject getTIle()
    {
        return this.gameObject;
    }
    public List<GameObject> getSreach(int Range)
    {
        List<GameObject> around = GameObject.Find("BattleGrid").GetComponent<BattleGrid>().SearchTile(this.gameObject, Range);
        /*
        for(int i = 0; i < around.Count; i++) 
        {
            Debug.Log(around[i]);
        }
        */
        GameObject.Find("BattleGrid").GetComponent<BattleGrid>().resetList();
        return around;
    }
    


    public void ChangeState(TileState changeState)
    {
        currentState = changeState;

    }

    public TileState getState()
    {

        return currentState;
    }

}
