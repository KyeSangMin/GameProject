using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    private BattleSystem battleSystem;
    private GameManager gameManager;

    //private BTNode rootnode;
    private SelectorNode rootnode;
    private BTNode root;

    private MouseController mouseController;
    private BattleGrid battleGrid;

    private GameObject currentObject;
    private Vector2 currentPos;
    private int maxHp = 1;
    private int currnetHp = 1;


    [SerializeField]
    public bool onOff1 = false;
    public bool onOff2 = false;
    public bool onOff3 = false;
    public bool onOff4 = true;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.Instance;

        mouseController = GameObject.Find("BattleSystem").GetComponent<MouseController>(); 
        battleGrid = GameObject.Find("BattleGrid").GetComponent<BattleGrid>();
        battleSystem = GameObject.Find("BattleSystem").GetComponent<BattleSystem>();

        List<BTNode> rootChildren = new List<BTNode>();

        List<BTNode> MAChildren = new List<BTNode>();
        List<BTNode> subSequenceCheckTrun = new List<BTNode>();
        // CheckTrun ¼­ºê ½ÃÄö½º
        subSequenceCheckTrun.Add(new ConditionNode(CheckEnemyTrun));
        subSequenceCheckTrun.Add(new BTActionNode(CheckTurnAction));
        rootChildren.Add(new SequenceNode(subSequenceCheckTrun));

        // move+attack conditionnode
        MAChildren.Add(new ConditionNode(CheckAllyInRange));

        // Move ¼­ºê ½ÃÄö½º
        List<BTNode> subSequenceMove = new List<BTNode>();
        subSequenceMove.Add(new ConditionNode(CheckEnemyHpTypeMove));
        subSequenceMove.Add(new BTActionNode(moveAction));
        subSequenceMove.Add(new BTActionNode(endAction));
        MAChildren.Add(new SequenceNode(subSequenceMove));

        // Attack ¼­ºê ½ÃÄö½º
        List<BTNode> subSequenceAttack = new List<BTNode>();
        subSequenceAttack.Add(new ConditionNode(CheckEnemyHpTypeAttack));
        subSequenceAttack.Add(new BTActionNode(attackAction));
        subSequenceAttack.Add(new BTActionNode(endAction));
        MAChildren.Add(new SequenceNode(subSequenceAttack));

        // move+attack
        List<BTNode> subSequenceMA = new List<BTNode>();
        subSequenceMA.Add(new SelectorNode(MAChildren));
        rootChildren.AddRange(subSequenceMA);



        // Flee ¼­ºê ½ÃÄö½º
        List<BTNode> subSequenceFlee = new List<BTNode>();
        subSequenceFlee.Add(new ConditionNode(CheckEnemyHpTypeFlee));
        subSequenceFlee.Add(new BTActionNode(fleeAction));
        subSequenceFlee.Add(new BTActionNode(endAction));
        rootChildren.Add(new SequenceNode(subSequenceFlee));

        // End ½ÃÄö½º 
        rootChildren.Add(new BTActionNode(endAction));
        
        root = new SelectorNode(rootChildren);



    }

    // Update is called once per frame
    void Update()
    {
        if(gameManager.isActiveAI)
        {
            if(!battleSystem.CheckAllyCharater())
            {

                //rootnode.Evaluate();
                root.Evaluate();

                if (!onOff1&&!onOff2&&!onOff3)
                {
                    //onOff4 = false;
            
                }
            }
        }
    }




    private BTNode.BTNodeState moveAction()
    {
        if (currentObject == null)
        {
            return BTNode.BTNodeState.Failure;
        }

        Vector2 _pos = currentPos;

        mouseController.inputAction(_pos);
        return BTNode.BTNodeState.Success;
    }

    private BTNode.BTNodeState attackAction()
    {
        if (currentObject == null)
        {
            return BTNode.BTNodeState.Failure;
        }
  

        Vector2 _pos = currentPos;

        mouseController.inputAction(_pos);
        return BTNode.BTNodeState.Success;
    }

    private BTNode.BTNodeState fleeAction()
    {
        if (currentObject == null)
        {
            return BTNode.BTNodeState.Failure;
        }

        GameObject currentTile = battleGrid.FindGridTIle((int)currentObject.GetComponent<CharacterStats>().getCharacterPos().x, (int)currentObject.GetComponent<CharacterStats>().getCharacterPos().y);
        GameObject tempObject = AStarSearch.bfsFarawaySearch(currentTile, 2);
        //Debug.Log(tempObject);
        
        if(tempObject == null)
        {
            return BTNode.BTNodeState.Success;
        }
        
        currentPos = new Vector2(tempObject.transform.position.x, tempObject.transform.position.y);
        Vector2 _pos = currentPos;
        mouseController.inputAction(_pos);



        return BTNode.BTNodeState.Success;
    }

    private BTNode.BTNodeState endAction()
    {

        onOff1 = false;
        onOff2 = false;
        onOff4 = true;
        currentObject = null;
        currnetHp = 1;
        maxHp = 1;
        return BTNode.BTNodeState.Success;

    }

    private BTNode.BTNodeState CheckTurnAction()
    {
        if (currentObject == null)
        {
            return BTNode.BTNodeState.Failure;
        }


        return BTNode.BTNodeState.Success;
    }

    private bool CheckAllyInRange()
    {
        
        if(currentObject == null)
        {
            return false;
        }
        
        GameObject currentTile = battleGrid.FindGridTIle((int)currentObject.GetComponent<CharacterStats>().getCharacterPos().x, (int)currentObject.GetComponent<CharacterStats>().getCharacterPos().y);
        GameObject tempObject = AStarSearch.bfsDistanceSearch(currentTile, 2);
        currentPos = new Vector2(tempObject.transform.position.x, tempObject.transform.position.y);


        if (tempObject.GetComponent<GridTile>().getState() == GridTile.TileState.None)
        {
            onOff1 = true;
            return false;
        }
        else
        {
   
            onOff2 = true;
            return false;
        }


    }

    private bool CheckEnemyHpTypeMove()
    {

        if(maxHp != 0)
        {
            if (currnetHp / maxHp * 100 > 30 && onOff2 == false)
            {
                onOff1 = true;
            }
            else
            {
                onOff1 = false;
            }
            
        }
        return onOff1;

    }

    private bool CheckEnemyHpTypeAttack()
    {


        return onOff2;
    }

    private bool CheckEnemyHpTypeFlee()
    {
        
               if (currnetHp / maxHp * 100 < 30 )
               {
                   onOff3 = true;
               }
        
        return onOff3;
    }
    private bool CheckEnemyTrun()
    {
 
        /**
        if (currentObject == null)
        {
            return false;
        }
        */
        return onOff4;
    }

    public void setEnemyAI(bool _CheckAi)
    {
        onOff4 = _CheckAi;
    }

    public void setcurrentObject(GameObject _gameObject)
    {
        currentObject = _gameObject;
        maxHp = _gameObject.GetComponent<CharacterStats>().getMaxHP();
        currnetHp = _gameObject.GetComponent<CharacterStats>().getCurrentHP();
    }




}
