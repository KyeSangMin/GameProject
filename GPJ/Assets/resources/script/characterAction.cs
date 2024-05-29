using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characterAction : MonoBehaviour
{


    public enum CharacterState
    {
        Idle,
        Move,
        Attack,
        Hit,
        Die,
        CounterAttack
    }
    private Animator animator;
    private BattleSystem battleSystem;
    public CharacterState characterState;
    private List<GameObject> MovedPath = new List<GameObject>();
    private GameObject moveTile;
    private GameObject AttackObject;
    private Vector3 target;
    [SerializeField]
    private int IncomeDamage;
    public bool isAttacking;

    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponentInChildren<Animator>();
        isAttacking = false;
        battleSystem = GameObject.Find("BattleSystem").GetComponent<BattleSystem>();
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
                    break;
                }
                else if (AttackObject != null)
                {
                    ChangeState(CharacterState.Attack);
                    break;
                }
                else if (this.GetComponent<CharacterStats>().getCurrentHP() <= 0)
                {
                    ChangeState(CharacterState.Die);
                }
                isIdle();
                break;

            case CharacterState.Move:
                if (Vector3.Distance(target, this.transform.position) == 0.0f)
                {
                    if (MovedPath[0] == moveTile)
                    {
                        MovedPath.Clear();
                        animator.SetBool("isMove", false);
                        ChangeState(CharacterState.Idle);
                        if (AttackObject == null)
                        {
                            battleSystem.EndTurn();
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
                    ChangeState(CharacterState.Idle);
                    battleSystem.EndTurn();
                    break;
                }
                isAttack();
                break;
            case CharacterState.Hit:
                if(!isAttacking)
                {
                    animator.SetBool("isHit", false);
                    

                    
                    if (this.GetComponent<CharacterStats>().getCurrentHP() <= 0)
                    {
                        ChangeState(CharacterState.Die);
                        break;
                    }
                    ChangeState(CharacterState.CounterAttack);      
                    break;
                }
                isHit();
                break;

            case CharacterState.Die:
                isDie();
                break;

            case CharacterState.CounterAttack:
                isCounterAttack();
                break;

                
        }
    }






    /*-------------------------------------------------------*/

    public void ChangeState(CharacterState updateState)
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
            moveTile.GetComponent<GridTile>().ChangeState(GridTile.TileState.Allypos);
        }
        else
        {
            moveTile.GetComponent<GridTile>().ChangeState(GridTile.TileState.Enemypos);
        }
    }

    private void SetMovedPath()
    {
        MovedPath = AStarSearch.Search(SearchTile(), moveTile);

    }

    private void isAttack()
    {
        animator.SetBool("isAttack", true);

        GameObject tile = this.gameObject.GetComponentInParent<characterAction>().AttackObject;       
        GameObject enemy = battleSystem.FindCharacter(tile.GetComponent<GridTile>().getTileX(), tile.GetComponent<GridTile>().getTileY());
        enemy.GetComponent<characterAction>().setIncomeDamage(this.gameObject.GetComponent<CharacterStats>().getDamage());

        if (enemy.GetComponent<characterAction>().characterState != CharacterState.Hit && enemy.GetComponent<characterAction>().isAttacking == true)
        {
            enemy.GetComponent<characterAction>().ChangeState(CharacterState.Hit);
        }
      
        target = battleSystem.FindCharacter(tile.GetComponent<GridTile>().getTileX(), tile.GetComponent<GridTile>().getTileY()).transform.position + new Vector3(0, 0.6f, 0);

        if (target.x < this.transform.position.x)
        {
            this.GetComponentInChildren<SpriteRenderer>().flipX = true;
        }
        else
        {
            this.GetComponentInChildren<SpriteRenderer>().flipX = false;
        }
    }

    public void isHit()
    {
        animator.SetBool("isHit",true);
        isAttacking = false;
        this.GetComponent<CharacterStats>().setHP(true, IncomeDamage);

    }

    private void isDie()
    {
        animator.SetBool("isdead", true);

    }

    private void isCounterAttack()
    {
        int i = Random.Range(1,10);
        if (i % 2 ==3)
        {
            animator.SetBool("isAttack", true);
        }
        
        ChangeState(CharacterState.Idle);

    }

    //********************************************************************************//
    public void setIncomeDamage(int damage)
    {
        IncomeDamage = damage;
    }

    public void setMoveGrid(GameObject GridTile)
    {
        moveTile = GridTile;
        GameObject.Find("BattleSystem").GetComponent<MouseController>().initCurrentObject();
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

    public void setAtackObject(GameObject Attacked)
    {

        AttackObject = Attacked;      
        if (Attacked == null )
        {
            return;
        }
        Vector2 position = this.gameObject.GetComponent<CharacterStats>().getCharacterPos();
        GameObject tile = GameObject.Find("BattleGrid").GetComponent<BattleGrid>().FindGridTIle((int)position.x, (int)position.y);
        moveTile = SearchAtackTile(AttackObject);
        //Debug.Log(moveTile);
        if(moveTile == tile)
        {
           
            if(gameObject.CompareTag("Ally"))
            {
                tile.GetComponent<GridTile>().ChangeState(GridTile.TileState.Allypos);
            }
            else
            {
                tile.GetComponent<GridTile>().ChangeState(GridTile.TileState.Enemypos);
            }
            GameObject.Find("BattleSystem").GetComponent<MouseController>().initCurrentObject();
            return;
        }
        setMoveGrid(moveTile);
        GameObject.Find("BattleSystem").GetComponent<MouseController>().initCurrentObject();
    }

    public void switchPosition(GameObject ally2)
    {
        GameObject allyobject1 = this.gameObject;
        GameObject allyobject2 = ally2;
        GameObject MoveTile1 = GameObject.Find("BattleGrid").GetComponent<BattleGrid>().FindGridTIle((int)allyobject1.GetComponent<CharacterStats>().getCharacterPos().x, (int)allyobject1.GetComponent<CharacterStats>().getCharacterPos().y);
        GameObject MoveTile2 = GameObject.Find("BattleGrid").GetComponent<BattleGrid>().FindGridTIle((int)allyobject2.GetComponent<CharacterStats>().getCharacterPos().x, (int)allyobject2.GetComponent<CharacterStats>().getCharacterPos().y);
        allyobject1.GetComponent<characterAction>().setMoveGrid(MoveTile2);
        allyobject2.GetComponent<characterAction>().setMoveGrid(MoveTile1);      
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
            var MoveTag = tempObject.GetComponent<GridTile>().getTileState();
            float currentdistance = Vector2.Distance(tempObject.transform.position, this.transform.position);
            if (distance > currentdistance && Tag == GridTile.TileState.None && MoveTag == GridTile.MoveTile.CanMoveTile)
            {
                Nearbytile = tempObject;
            }

        }
        return Nearbytile;
    }
}
