using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridTile : MonoBehaviour
{
    
     public enum TileState
     { 
         None,
         Allypos,
         Enemypos, 
         Obstacle
     }
      
    public enum MoveTile
     {
         CanMoveTile,
         CantTile
     }

  [SerializeField]
    private TileState currentState;
    [SerializeField]
    private MoveTile currentMoveTile;
    [SerializeField]
    private int TileX;
    [SerializeField]
    private int TileY;

    private Animator animator;
    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {

        //currentState = TileState.None;
        currentMoveTile = MoveTile.CantTile;
        animator = this.gameObject.GetComponent<Animator>();
        spriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

        SwichColorTile();
    }

    private void SwichColorTile()
    {

        switch(currentMoveTile)
        {
            case MoveTile.CanMoveTile:
                if(currentState == TileState.Enemypos)
                {        
                    setTileAnimation(0.6f, 0.0f, 0.0f, 0.7f);
                }
                else if(currentState == TileState.Allypos)
                {
                    setTileAnimation(0.0f, 0.0f, 0.6f, 0.7f);
                }
                else if(currentState == TileState.Obstacle)
                {
                    ChangeMoveTileState(MoveTile.CantTile);
                    break;
                }    
                else
                {
                    setTileAnimation(0.0f, 0.0f, 0.0f, 0.7f);           
                }
                break;

            case MoveTile.CantTile:
                break;     
        }     
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
        GameObject.Find("BattleGrid").GetComponent<BattleGrid>().resetList();
        return around;
    }

    
    public TileState getState()
    {

        return currentState;
    }

    public MoveTile getTileState()
    {

        return currentMoveTile;
    }

    public void ChangeState(TileState changeState)
    {
        currentState = changeState;

    }
    public void ChangeMoveTileState(MoveTile changeState)
    {
        currentMoveTile = changeState;

    }

  
    public void OnMouseEnter()
    {

        switch (currentState)
        {
            case TileState.None:
                setTileAnimation(0.0f, 0.0f, 0.0f, 0.7f);
                break;

            case TileState.Allypos:
                setTileAnimation(0.0f, 0.0f, 0.6f, 0.7f);
                break;

            case TileState.Enemypos:
                setTileAnimation(0.6f, 0.0f, 0.0f, 0.7f);
                break;

            case TileState.Obstacle:
                break;

        }
    }

    public void OnMouseExit( )
    {
        resetTileAni();
    }

    public void resetTileAni()
    {
        animator.SetBool("CanMove", false);
        animator.SetBool("isIdle", true);
        spriteRenderer.color = new Color(0.6f, 0.6f, 0.6f, 0.25f);
    }

   private void setTileAnimation(float r, float g, float b, float a)
    {
        animator.SetBool("CanMove", true);
        animator.SetBool("isIdle", false);
        spriteRenderer.color = new Color(r, g, b, a);
    }

}
