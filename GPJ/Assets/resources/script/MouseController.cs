using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour
{
    public int HelloWorld = 2;

    private GameObject currentObject;

    private Cursor cursor;
  
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);

            if(hit.collider == null)
            {
                return;
            }

            if (hit.collider.CompareTag("GridTile") && currentObject != null)
            {
                var tileState = hit.collider.gameObject.GetComponent<GridTile>().getState();
                var isMoveTIle = hit.collider.gameObject.GetComponent<GridTile>().getTileState();
                switch (tileState)
                {
                    case GridTile.TileState.None:
                        if(isMoveTIle == GridTile.MoveTile.CantTile)
                        {
                            break;
                        }
                        GameObject.Find("BattleGrid").GetComponent<BattleGrid>().FindGridTIle((int)currentObject.GetComponent<CharacterStats>().getCharacterPos().x, (int)currentObject.GetComponent<CharacterStats>().getCharacterPos().y).GetComponent<GridTile>().ChangeState(GridTile.TileState.None);
                        currentObject.GetComponent<characterAction>().setMoveGrid(hit.collider.gameObject.GetComponent<GridTile>().getTIle());
                        GameObject.Find("BattleGrid").GetComponent<BattleGrid>().resetTileState();
                        break;

                    case GridTile.TileState.Allypos:
                        if (isMoveTIle == GridTile.MoveTile.CantTile || GameObject.Find("BattleGrid").GetComponent<BattleGrid>().FindGridTIle((int)currentObject.GetComponent<CharacterStats>().getCharacterPos().x, (int)currentObject.GetComponent<CharacterStats>().getCharacterPos().y) == hit.collider.gameObject)
                        {
                            break;
                        }
                        else if(isMoveTIle == GridTile.MoveTile.CanMoveTile && GameObject.Find("BattleGrid").GetComponent<BattleGrid>().FindGridTIle((int)currentObject.GetComponent<CharacterStats>().getCharacterPos().x, (int)currentObject.GetComponent<CharacterStats>().getCharacterPos().y).GetComponent<GridTile>().getState() == tileState)
                        {
                            GameObject hitObject = GameObject.Find("Camera").GetComponent<BattleSystem>().FindCharacter(hit.collider.gameObject.GetComponent<GridTile>().getTileX(), hit.collider.gameObject.GetComponent<GridTile>().getTileY());
                            currentObject.GetComponent<characterAction>().switchPosition(hitObject);                        
                            GameObject.Find("BattleGrid").GetComponent<BattleGrid>().resetTileState();
                            break;
                        }
                        else
                        {
                            GameObject.Find("BattleGrid").GetComponent<BattleGrid>().FindGridTIle((int)currentObject.GetComponent<CharacterStats>().getCharacterPos().x, (int)currentObject.GetComponent<CharacterStats>().getCharacterPos().y).GetComponent<GridTile>().ChangeState(GridTile.TileState.None);
                            currentObject.GetComponent<characterAction>().setAtackObject(hit.collider.gameObject.GetComponent<GridTile>().getTIle());
                            GameObject.Find("BattleGrid").GetComponent<BattleGrid>().resetTileState();
                            break;
                        }                      

                    case GridTile.TileState.Enemypos:
                        if (isMoveTIle == GridTile.MoveTile.CantTile || GameObject.Find("BattleGrid").GetComponent<BattleGrid>().FindGridTIle((int)currentObject.GetComponent<CharacterStats>().getCharacterPos().x, (int)currentObject.GetComponent<CharacterStats>().getCharacterPos().y) == hit.collider.gameObject)
                        {
                            break;
                        }
                        GameObject.Find("BattleGrid").GetComponent<BattleGrid>().FindGridTIle((int)currentObject.GetComponent<CharacterStats>().getCharacterPos().x, (int)currentObject.GetComponent<CharacterStats>().getCharacterPos().y).GetComponent<GridTile>().ChangeState(GridTile.TileState.None);
                        currentObject.GetComponent<characterAction>().setAtackObject(hit.collider.gameObject.GetComponent<GridTile>().getTIle());
                        GameObject.Find("BattleGrid").GetComponent<BattleGrid>().resetTileState();
                        break;

                    case GridTile.TileState.Obstacle:
                        break;
                }
            }




        }
    }

    public void setCurrentObject(GameObject Ally)
    {
        currentObject = Ally;
    }

    public GameObject getCurrentObject()
    {
        return currentObject;
    }

    public void initCurrentObject()
    {
        currentObject = null;
    }


}
