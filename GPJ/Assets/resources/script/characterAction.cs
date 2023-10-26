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

    private CharacterState characterState;
    public GameObject moveTile;
    Vector3 target;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (characterState)
        {
            case CharacterState.Idle:
                if(moveTile != null)
                {
                    ChangeState(CharacterState.Move);
                }
                break;
            case CharacterState.Move:

                isMove();
                if (Vector3.Distance(target, this.transform.position) < 0.1f)
                {
                    moveTile = null;
                    GameObject.Find("Camera").GetComponent<BattleSystem>().EndTurn();

                    ChangeState(CharacterState.Idle);

                }
                break;

            case CharacterState.Attack:
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

    private void isMove()
    {
      
        target = moveTile.transform.position + new Vector3(0, 1, 0);
        this.transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime*5);
       
    }


    private void isAttack()
    {

    }

    private void isHit()
    {

    }

    private void isDie()
    {

    }


    public void setMoveGrid(GameObject GridTile)
    {
        moveTile = GridTile;
    }

    public GameObject getMoveGrid()
    {
        return moveTile;
    }


}
