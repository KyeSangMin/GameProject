using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSystem : MonoBehaviour
{
    private enum Battlestate
    {
        IncreaseGauge,
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


    /***********************************************/

    // 임시 변수

    int speedValue = 100;
    int speedValue2 = 250;
    int speedValue3 = 500;

    /************************************************/




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

            case Battlestate.AllyTrun:
                if (PriorityQueue.Count == 0)
                {
                    Debug.Log("empty");
                    ChangeState(Battlestate.IncreaseGauge);
                }
                AllyTurn();
                
                break;




            case Battlestate.EnemyTrun:
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

            //SpeedGaugeMap[i] += ActionCharacters[i].GetComponent<CharacterStat>().getSpeed();

            if (i == 0)
            {
                SpeedGaugeMap[i] += speedValue;
            }
            else if(i == 1)
            {
                SpeedGaugeMap[i] += speedValue2;
            }
            else if (i == 2)
            {
                SpeedGaugeMap[i] += speedValue3;
            }
      
        }
       
        StartCoroutine(SpeedGaugeDilay(0.35f));
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

        ExecuteAction();

    }

    private void ExecuteAction()
    {

        if(PriorityQueue.Count == 0)
        {
            return;
        }

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



    IEnumerator SpeedGaugeDilay(float time)
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
            Debug.Log("dequeue null");
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
            ChangeState(Battlestate.AllyTrun);
           
        }
        else if (actionCharacters.CompareTag("Enemy"))
        {
            ChangeState(Battlestate.EnemyTrun);
        }

    }

    /*----------------------------------------------------------*/
    //allytrun
    private void AllyTurn()
    {
        GameObject.Find("Camera").GetComponent<MouseController>().setCurrentObject(turnedObejct);
    }


    /*----------------------------------------------------------*/
    //enemytrun
    private void EnemyTurn()
    {

    }



    /*----------------------------------------------------------*/
    // endtrun

    public void EndTurn()
    {
        turnedObejct = null;
        GameObject.Find("Camera").GetComponent<MouseController>().setCurrentObject(turnedObejct);
        ExecuteAction();
    
     }

}
