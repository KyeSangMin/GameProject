using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour
{
    public int HelloWorld = 2;

    private GameObject currentObject;
  
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
                if (hit.collider.gameObject.GetComponent<GridTile>().getState() == GridTile.TileState.none)
                {
                    GameObject.Find("BattleGrid").GetComponent<BattleGrid>().FindGridTIle((int)currentObject.GetComponent<CharacterStats>().getCharacterPos().x, (int)currentObject.GetComponent<CharacterStats>().getCharacterPos().y).GetComponent<GridTile>().ChangeState(GridTile.TileState.none);
                    currentObject.GetComponent<characterAction>().setMoveGrid(hit.collider.gameObject.GetComponent<GridTile>().getTIle());
                }
                else if (hit.collider.gameObject.GetComponent<GridTile>().getState() == GridTile.TileState.enemypos)
                {
                    GameObject.Find("BattleGrid").GetComponent<BattleGrid>().FindGridTIle((int)currentObject.GetComponent<CharacterStats>().getCharacterPos().x, (int)currentObject.GetComponent<CharacterStats>().getCharacterPos().y).GetComponent<GridTile>().ChangeState(GridTile.TileState.none);
                    currentObject.GetComponent<characterAction>().setAtackObject(hit.collider.gameObject.GetComponent<GridTile>().getTIle());
                }
                else if (hit.collider.gameObject.GetComponent<GridTile>().getState() == GridTile.TileState.allypos)
                {
                    GameObject.Find("BattleGrid").GetComponent<BattleGrid>().FindGridTIle((int)currentObject.GetComponent<CharacterStats>().getCharacterPos().x, (int)currentObject.GetComponent<CharacterStats>().getCharacterPos().y).GetComponent<GridTile>().ChangeState(GridTile.TileState.none);
                    currentObject.GetComponent<characterAction>().setAtackObject(hit.collider.gameObject.GetComponent<GridTile>().getTIle());
                }
                else if (hit.collider.gameObject.GetComponent<GridTile>().getState() == GridTile.TileState.obstacle)
                {
                    return;
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
