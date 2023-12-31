using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BattleSystem : MonoBehaviour
{
    private enum Battlestate
    {
        IncreaseGauge,
        GivingTrun,
        AllyTrun,
        EnemyTrun,
        EndBattle
    }

    private Battlestate currentState;

    public List<GameObject> allyCharList = new List<GameObject>();
    public List<GameObject> enemyCharList = new List<GameObject>();

    public GameObject turnedObejct;

    public List<GameObject> ActionCharacterList = new List<GameObject>();

    public List<int> SpeedGaugeList = new List<int> ();

    private BattleGrid battleGrid;
    private MouseController MouseController;
    private CharacterInfoManager characterInfoManager;
     
  
    private List<Tuple<GameObject, int>> PriorityQueue = new List<Tuple<GameObject, int>>();



    // Start is called before the first frame update
    void Start() 
    {
        battleGrid = GameObject.Find("BattleGrid").GetComponent<BattleGrid>();
        MouseController = GameObject.Find("Camera").GetComponent<MouseController>();
        characterInfoManager = GameObject.Find("Camera").GetComponent<CharacterInfoManager>();
        currentState = Battlestate.IncreaseGauge;
        CreateCharatertoGrid();
        loadActionCharacter();


    } 

    // Update is called once per frame
    void Update()
    {

       
        switch (currentState)
        {
            case Battlestate.IncreaseGauge:
                UpdateSpeedGauge();
                break;

            case Battlestate.GivingTrun:
                if (PriorityQueue.Count == 0)
                {
                    ChangeState(Battlestate.IncreaseGauge);
                    break;
                }
                ExecuteAction();
                break;

            case Battlestate.AllyTrun:
                if(turnedObejct == null)
                {
                    ChangeState(Battlestate.IncreaseGauge);
                    break;
                }
                AllyTrun();
                break;

            case Battlestate.EnemyTrun:
                if (turnedObejct == null)
                {
                    ChangeState(Battlestate.IncreaseGauge);
                    break;
                }
                AllyTrun();
                break;

            case Battlestate.EndBattle:
                break;
        }


    }


    public void updateIncreaseSpeed()
    {
        currentState = Battlestate.IncreaseGauge;
    }
 
    private void loadActionCharacter()
    {
        for (int i = 0; i < allyCharList.Count + enemyCharList.Count; i++) 
        {
            if (i < allyCharList.Count)
            {
                ActionCharacterList.Add(allyCharList[i]);
            }
            else
            {
                ActionCharacterList.Add(enemyCharList[i - allyCharList.Count]);
            }
            SpeedGaugeList.Add(0);  
        }
    }

    private void CreateCharatertoGrid()
    {
        List<GameObject> AllyTileList = battleGrid.FindPosTile(GridTile.TileState.Allypos);
        List<GameObject> AllyList = characterInfoManager.getAllyCharacterList();

        List<GameObject> EnemyTileList = battleGrid.FindPosTile(GridTile.TileState.Enemypos);
        List<GameObject> EnemyList = characterInfoManager.getEnemyCharacterList();

        for (int allyNum = 0; allyNum < AllyTileList.Count; allyNum++) 
        {
            GameObject Allyprefap = SpawnCharaterObject(allyNum, AllyTileList[allyNum], AllyList);
            Vector2 allyvector2 = new Vector2(AllyTileList[allyNum].GetComponent<GridTile>().getTileX(), AllyTileList[allyNum].GetComponent<GridTile>().getTileY());
            allyCharList.Add(Allyprefap);
            allyCharList[allyNum].GetComponent<CharacterStats>().setCharacterPos(allyvector2);
        }       

        for(int enemyNum = 0; enemyNum < EnemyTileList.Count; enemyNum++)
        {
            GameObject Enenmyprefap = SpawnCharaterObject(enemyNum, EnemyTileList[enemyNum], EnemyList);
            Vector2 enemyvector2 = new Vector2(EnemyTileList[enemyNum].GetComponent<GridTile>().getTileX(), EnemyTileList[enemyNum].GetComponent<GridTile>().getTileY());
            enemyCharList.Add(Enenmyprefap);
            enemyCharList[enemyNum].GetComponent<CharacterStats>().setCharacterPos(enemyvector2);
        }
         
    }

    private GameObject SpawnCharaterObject(int allyNum, GameObject tiles, List<GameObject> tempList) 
    {
        Vector3 target = tiles.transform.position + new Vector3(0, 0.6f, 0);
        return Instantiate(tempList[allyNum], target, Quaternion.identity);
    }

    private void UpdateState(Battlestate battlestate)
    {
        switch (battlestate)
        {
            case Battlestate.IncreaseGauge:
                break;
            case Battlestate.AllyTrun:
                break;
            case Battlestate.EnemyTrun:
                break;
        }

    }

    private void ChangeState(Battlestate battlestate)
    {

        currentState = battlestate;

    }


  



    /*----------------------------------------------------------*/
    // Speed Gage 


    private void UpdateSpeedGauge()
    {

        for (int i = 0; i < allyCharList.Count + enemyCharList.Count; i++)
        {

                SpeedGaugeList[i] += ActionCharacterList[i].GetComponent<CharacterStats>().getSpeed();
        } 

        StartCoroutine(SpeedGaugeDelay(0.35f));
        CheckSpeedGauge();
    }

    private void CheckSpeedGauge()
    {

        for (int i = 0; i < allyCharList.Count + enemyCharList.Count; i++)
        {
            
            if (SpeedGaugeList[i] > 10000)
            {
                // 우선순위 큐 삽입
                Enqueue(ActionCharacterList[i], SpeedGaugeList[i]);
                SpeedGaugeList[i] -= 10000;

            }

        }
        ChangeState(Battlestate.GivingTrun);

    }

    private void ExecuteAction()
    {
        GameObject tempObject = Dequeue();
        for (int i = 0; i < allyCharList.Count + enemyCharList.Count; i++)
        {
            if (ActionCharacterList[i].Equals(tempObject))
            {
                turnedObejct = tempObject;
                CheckTurn(tempObject);

            }
        }

    }



    IEnumerator SpeedGaugeDelay(float time)
    {

        yield return new WaitForSeconds(time);

    }


    /*----------------------------------------------------------*/
    // PriorityQueue

  
    public void Enqueue(GameObject gameObject, int SpeedGauge)
    {
        PriorityQueue.Add(new Tuple<GameObject, int >(gameObject, SpeedGauge));
        PriorityQueue.Sort((x, y) => x.Item2.CompareTo(y.Item2));
    }

    public GameObject Dequeue()
    {
        if(PriorityQueue.Count == 0)
        {
            return null;
        } 
        GameObject tempObject = PriorityQueue[0].Item1;
        PriorityQueue.RemoveAt(0);
        PriorityQueue.Sort((x, y) => x.Item2.CompareTo(y.Item2));
        return tempObject;
    }

  
    /*----------------------------------------------------------*/
    // Turn

    private void CheckTurn(GameObject actionCharacters)
    {

        if (actionCharacters == null)
        {
            return;
        }
        if (actionCharacters.CompareTag("Ally"))
        {
            SearchAnimation();
            ChangeState(Battlestate.AllyTrun);
           
        }
        else if (actionCharacters.CompareTag("Enemy"))
        {
            SearchAnimation();
            ChangeState(Battlestate.EnemyTrun);
        }

    }


    private void SearchAnimation()
    {
        int x = (int)turnedObejct.GetComponent<CharacterStats>().getCharacterPos().x;
        int y = (int)turnedObejct.GetComponent<CharacterStats>().getCharacterPos().y;
        int range = turnedObejct.GetComponent<CharacterStats>().getRange();

        GameObject tile = battleGrid.FindGridTIle(x,y);
        List<GameObject> TempAnimationTile = new List<GameObject>();
        TempAnimationTile = AStarSearch.bfsSearch(tile, range);

        foreach (GameObject next in TempAnimationTile)
        {
            next.GetComponent<GridTile>().ChangeMoveTileState(GridTile.MoveTile.CanMoveTile);
        }
        
    }

    /*----------------------------------------------------------*/
    //allytrun
    private void AllyTrun()
    {
        MouseController.setCurrentObject(turnedObejct);
    }


    /*----------------------------------------------------------*/
    //enemytrun
    private void EnemyTrun()
    {

    }


    /*----------------------------------------------------------*/
    public GameObject FindCharacter(int x, int y)
    {
     
        for (int i = 0; i < ActionCharacterList.Count; i++)
        {
            if (ActionCharacterList[i].GetComponent<CharacterStats>().getCharacterPos().x == x && ActionCharacterList[i].GetComponent<CharacterStats>().getCharacterPos().y == y)
            {
                return ActionCharacterList[i];
            }
        }

        return null;
    }
    /*----------------------------------------------------------*/
    // endtrun

    public void EndTurn()
    {
        
        turnedObejct = null;
        MouseController.setCurrentObject(turnedObejct);
        ChangeState(Battlestate.GivingTrun);

    }


    public void DestroyCharacter(GameObject gameObject)
    {
        
        for (int i = 0; i < allyCharList.Count + enemyCharList.Count; i++)
        {
            if (i < allyCharList.Count && ActionCharacterList[i] == gameObject)
            {           
                ActionCharacterList.RemoveAt(i);
                allyCharList.RemoveAt(i);
                SpeedGaugeList.RemoveAt(i);
            }
            else if (ActionCharacterList[i] == gameObject)
            {
                ActionCharacterList.RemoveAt(i);
                enemyCharList.RemoveAt(i - allyCharList.Count);
                SpeedGaugeList.RemoveAt(i);
            }

        }
        if (turnedObejct == gameObject)
        {
            turnedObejct = null;
        }
        /*
        else if(PriorityQueue[0].Item1 == gameObject)
        {
            PriorityQueue.RemoveAt(0);
            
        }
        */
    }


}
