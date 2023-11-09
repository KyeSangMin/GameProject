using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characterAction : MonoBehaviour
{


    private enum CharacterState
    {
        Idle,
        Move,
        Attack,
        Hit,
        Die
    }
    private Animator animator;

    private CharacterState characterState;
    private List<GameObject> MovedPath = new List<GameObject>();
    public GameObject moveTile;
    public GameObject AttackObject;
    Vector3 target;

    // Start is called before the first frame update
    void Start()
    {
        // animator = gameObject.GetComponent<Animator>();
        animator = gameObject.GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (characterState)
        {
            case CharacterState.Idle:
                if (MovedPath.Count != 0)
                {
                    ChangeState(CharacterState.Move);
                }
                else if (AttackObject != null)
                {
                    ChangeState(CharacterState.Attack);
                }
                isIdle();
                break;
            case CharacterState.Move:
                if (Vector3.Distance(target, this.transform.position) < 0.01f)
                {
                    if (MovedPath[0] == moveTile)
                    {
                        MovedPath.Clear();
                        animator.SetBool("isMove", false);
                        ChangeState(CharacterState.Idle);
                        if (AttackObject == null)
                        {
                            GameObject.Find("Camera").GetComponent<BattleSystem>().EndTurn();
                        }                  
                        break;
                    }
                    else
                    {
                        MovedPath.RemoveAt(0);                        
                    }
                }
                isMove(MovedPath[0]);
                break;
            case CharacterState.Attack:
                if (AttackObject == null)
                {
                    animator.SetBool("isAttack", false);
                    ChangeState(CharacterState.Idle);
                    GameObject.Find("Camera").GetComponent<BattleSystem>().EndTurn();
                    break;
                }
                isAttack();
                break;



            case CharacterState.Hit:
                break;

            case CharacterState.Die:
                break;
        }
    }






    /*-------------------------------------------------------*/

    private void UpdateState(CharacterState characterState)
    {
        switch(characterState)
        {
            case CharacterState.Idle:
                break;

            case CharacterState.Attack:
                break;

            case CharacterState.Move:
                break;

            case CharacterState.Hit:
                break;

            case CharacterState.Die:
                break;
        }
    }

    private void EnterState(CharacterState characterState)
    {
        switch (characterState)
        {
            case CharacterState.Idle:
                break;

            case CharacterState.Attack:
                break;

            case CharacterState.Move:

                break;

            case CharacterState.Hit:
                break;

            case CharacterState.Die:
                break;
        }
    }

    private void ChangeState(CharacterState updateState)
    {
        characterState = updateState;
    }


    /*-------------------------------------------------------*/

    private void isIdle()
    {
       
    }

    private void isMove(GameObject path)
    {
        animator.SetBool("isMove", true);
        target = MovedPath[0].transform.position + new Vector3(0, 0.6f, 0);
        if (target.x  < this.transform.position.x)
        {
            this.GetComponentInChildren<SpriteRenderer>().flipX = true;
        }
        else
        {
            this.GetComponentInChildren<SpriteRenderer>().flipX = false;
        }   
        this.transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime*5);
        this.gameObject.GetComponent<CharacterStats>().setCharacterPos(new Vector2(moveTile.GetComponent<GridTile>().getTileX(), moveTile.GetComponent<GridTile>().getTileY()));
        if(this.gameObject.CompareTag("Ally"))
        {
            moveTile.GetComponent<GridTile>().ChangeState(GridTile.TileState.allypos);
        }
        else
        {
            moveTile.GetComponent<GridTile>().ChangeState(GridTile.TileState.enemypos);
        }
    }

    private void SetMovedPath()
    {
        MovedPath = AStarSearch.Search(SearchTile(), moveTile);

        for(int i = 0; i< MovedPath.Count; i++)
        {
            Debug.Log(MovedPath[i]);
        }
        
    }

    private void isAttack()
    {
        animator.SetBool("isAttack", true);
        //GameObject.Find("Camera").GetComponent<BattleSystem>().FindCharacter(AttackObject.GetComponent<GridTile>().getTileX(), AttackObject.GetComponent<GridTile>().getTileY());
    }

   

    private void isHit(int damage)
    {
        this.GetComponent<CharacterStats>().setHP(damage);
    }

    private void isDie()
    {

    }


    public void setMoveGrid(GameObject GridTile)
    {
        moveTile = GridTile;
        GameObject.Find("Camera").GetComponent<MouseController>().initCurrentObject();
        SetMovedPath();
    }

    public GameObject getMoveGrid()
    {
        return moveTile;
    }

    private GameObject SearchTile()
    {
        Vector2 position = this.gameObject.GetComponent<CharacterStats>().getCharacterPos();
        GameObject tile = GameObject.Find("BattleGrid").GetComponent<BattleGrid>().FindGridTIle((int)position.x, (int)position.y);
        return tile;
    }

    public void setAtackObject(GameObject Atacked)
    {

        AttackObject = Atacked;
        if (Atacked == null)
        {
            return;
        }

       
        GameObject.Find("Camera").GetComponent<MouseController>().initCurrentObject();
        moveTile = SearchAtackTile(AttackObject);
        setMoveGrid(moveTile);
    }

    private GameObject SearchAtackTile(GameObject AtackTile)
    {
        List<GameObject> AtackTiles = AtackTile.GetComponent<GridTile>().getSreach(1);
        if(AtackTiles.Contains(AttackObject))
        {
            int key = AtackTiles.IndexOf(AttackObject);
            AtackTiles.RemoveAt(key);
        }
        GameObject Nearbytile = AtackTiles[0];
        float distance = Vector2.Distance(AtackTiles[0].transform.position, this.transform.position);
        foreach (GameObject tempObject in AtackTiles) 
        {
            var Tag = tempObject.GetComponent<GridTile>().getState();
            float currentdistance = Vector2.Distance(tempObject.transform.position, this.transform.position);
            if (distance > currentdistance && Tag == GridTile.TileState.none)
            {
                Nearbytile = tempObject;
            }

        }
        return Nearbytile;
    }
}
