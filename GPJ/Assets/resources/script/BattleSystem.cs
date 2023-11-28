using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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


    public GameObject[] allyChars;
    public GameObject[] enemyChars;
    [SerializeField]
    public GameObject turnedObejct;

    [SerializeField]
    public GameObject[] ActionCharacters;

    [SerializeField]
    public int[] SpeedGaugeMap;


    
  
    private List<Tuple<GameObject, int>> PriorityQueue = new List<Tuple<GameObject, int>>();



    // Start is called before the first frame update
    void Start()
    {
       
        currentState = Battlestate.IncreaseGauge;

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


    /*
    private void loadAllyCharacter()
    {
        
    }
    */
    /*
    private void loadEnemyCharacter()
    {

    }
    */


    private void loadActionCharacter()
    {
        
        for (int i = 0; i < allyChars.Length + enemyChars.Length; i++)
        {
            if (i < allyChars.Length)
            {
                ActionCharacters[i] = allyChars[i];
            }
            else
            {
                ActionCharacters[i] = enemyChars[i - allyChars.Length];
            }

        }
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
       
        for (int i = 0; i < allyChars.Length + enemyChars.Length; i++)
        {

            SpeedGaugeMap[i] += ActionCharacters[i].GetComponent<CharacterStats>().getSpeed();
      
        }
       
        StartCoroutine(SpeedGaugeDelay(0.35f));
        CheckSpeedGauge();
    }

    private void CheckSpeedGauge()
    {
 
        for (int i = 0; i < allyChars.Length + enemyChars.Length; i++)
        {
            if (SpeedGaugeMap[i] > 10000)
            {
                // 우선순위 큐 삽입
                Enqueue(ActionCharacters[i], SpeedGaugeMap[i]);
                SpeedGaugeMap[i] -= 10000;

            }
        }

        ChangeState(Battlestate.GivingTrun);

    }

    private void ExecuteAction()
    {
        GameObject tempObject = Dequeue();

        for (int i = 0; i < allyChars.Length + enemyChars.Length; i++)
        {
            if (ActionCharacters[i].Equals(tempObject))
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
        GameObject tile = GameObject.Find("BattleGrid").GetComponent<BattleGrid>().FindGridTIle(x,y);
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
        GameObject.Find("Camera").GetComponent<MouseController>().setCurrentObject(turnedObejct);
    }


    /*----------------------------------------------------------*/
    //enemytrun
    private void EnemyTrun()
    {

    }


    /*----------------------------------------------------------*/
    public GameObject FindCharacter(int x, int y)
    {
        for (int i = 0; i < ActionCharacters.Length; i++)
        {
            if (ActionCharacters[i].GetComponent<CharacterStats>().getCharacterPos().x == x && ActionCharacters[i].GetComponent<CharacterStats>().getCharacterPos().y == y)
            {
                return ActionCharacters[i];
            }
        }

        return null;
    }
    /*----------------------------------------------------------*/
    // endtrun

    public void EndTurn()
    {
        
        turnedObejct = null;
        GameObject.Find("Camera").GetComponent<MouseController>().setCurrentObject(turnedObejct);
        ChangeState(Battlestate.GivingTrun);

    }


   

}
