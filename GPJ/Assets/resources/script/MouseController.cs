using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour
{
    public int HelloWorld = 2;

    private GameObject currentObject;

    private Cursor cursor;

    private BattleGrid battleGrid;
    private BattleSystem battleSystem;
    // Start is called before the first frame update
    void Start()
    {
        battleGrid = GameObject.Find("BattleGrid").GetComponent<BattleGrid>();
        battleSystem = GameObject.Find("Camera").GetComponent<BattleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            inputAction(pos);
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
    public void inputAction(Vector2 _pos)
    {
        Debug.Log("startInputAction");
            Vector2 pos = _pos;
            RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);

            if (hit.collider == null)
            {
            Debug.Log("nulltarget");
                return;
            }

            if (hit.collider.CompareTag("GridTile") && currentObject != null)
            {
                var tileState = hit.collider.gameObject.GetComponent<GridTile>().getState();
                var isMoveTIle = hit.collider.gameObject.GetComponent<GridTile>().getTileState();
                switch (tileState)
                {
                    case GridTile.TileState.None:
                        if (isMoveTIle == GridTile.MoveTile.CantTile)
                        {
                            Debug.Log("cantileNone");
                            break;
                        }
                        battleGrid.FindGridTIle((int)currentObject.GetComponent<CharacterStats>().getCharacterPos().x, (int)currentObject.GetComponent<CharacterStats>().getCharacterPos().y).GetComponent<GridTile>().ChangeState(GridTile.TileState.None);
                        currentObject.GetComponent<characterAction>().setMoveGrid(hit.collider.gameObject.GetComponent<GridTile>().getTIle());
                        battleGrid.resetTileState();
                        break;

                    case GridTile.TileState.Allypos:
                        if (isMoveTIle == GridTile.MoveTile.CantTile || battleGrid.FindGridTIle((int)currentObject.GetComponent<CharacterStats>().getCharacterPos().x, (int)currentObject.GetComponent<CharacterStats>().getCharacterPos().y) == hit.collider.gameObject)
                        {
                            Debug.Log("cantileAllyPos");
                            break;
                    }
                        else if (isMoveTIle == GridTile.MoveTile.CanMoveTile && battleGrid.FindGridTIle((int)currentObject.GetComponent<CharacterStats>().getCharacterPos().x, (int)currentObject.GetComponent<CharacterStats>().getCharacterPos().y).GetComponent<GridTile>().getState() == tileState)
                        {
                            GameObject hitObject = battleSystem.FindCharacter(hit.collider.gameObject.GetComponent<GridTile>().getTileX(), hit.collider.gameObject.GetComponent<GridTile>().getTileY());
                            currentObject.GetComponent<characterAction>().switchPosition(hitObject);
                            battleGrid.resetTileState();
                            break;
                        }
                        else
                        {
                            battleGrid.FindGridTIle((int)currentObject.GetComponent<CharacterStats>().getCharacterPos().x, (int)currentObject.GetComponent<CharacterStats>().getCharacterPos().y).GetComponent<GridTile>().ChangeState(GridTile.TileState.None);
                            currentObject.GetComponent<characterAction>().setAtackObject(hit.collider.gameObject.GetComponent<GridTile>().getTIle());
                            GameObject attackedObject2 = battleSystem.FindCharacter(hit.collider.gameObject.GetComponent<GridTile>().getTileX(), hit.collider.gameObject.GetComponent<GridTile>().getTileY());
                            attackedObject2.GetComponent<characterAction>().isAttacking = true;
                            Debug.Log(attackedObject2);
                            battleGrid.resetTileState();
                            break;
                        }

                    case GridTile.TileState.Enemypos:
                        if (isMoveTIle == GridTile.MoveTile.CantTile || battleGrid.FindGridTIle((int)currentObject.GetComponent<CharacterStats>().getCharacterPos().x, (int)currentObject.GetComponent<CharacterStats>().getCharacterPos().y) == hit.collider.gameObject)
                        {
                            Debug.Log("cantileEnemyPos");
                            break;
                        }
                        battleGrid.FindGridTIle((int)currentObject.GetComponent<CharacterStats>().getCharacterPos().x, (int)currentObject.GetComponent<CharacterStats>().getCharacterPos().y).GetComponent<GridTile>().ChangeState(GridTile.TileState.None);
                        currentObject.GetComponent<characterAction>().setAtackObject(hit.collider.gameObject.GetComponent<GridTile>().getTIle());
                        GameObject attackedObject = battleSystem.FindCharacter(hit.collider.gameObject.GetComponent<GridTile>().getTileX(), hit.collider.gameObject.GetComponent<GridTile>().getTileY());
                        attackedObject.GetComponent<characterAction>().isAttacking = true;
                        battleGrid.resetTileState();
                        break;

                    case GridTile.TileState.Obstacle:
                        break;
                }
            }

     }
    

}
