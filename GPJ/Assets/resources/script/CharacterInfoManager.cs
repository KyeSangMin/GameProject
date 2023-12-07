using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInfoManager : MonoBehaviour
{
    //[SerializeField]
    private List<GameObject> AllyCharacterList;
    private List<GameObject> EnemyCharacterList;
    [SerializeField]
    private GameObject[] AllyCharater;
    [SerializeField]
    private GameObject[] EnemyCharater;

    // Start is called before the first frame update

    private void Awake()
    {
        if (AllyCharacterList == null)
        {
            AllyCharacterList = new List<GameObject>();
        }

        if(EnemyCharacterList == null)
        {
            EnemyCharacterList = new List<GameObject>();
        }

        for(int i =0; i<5; i++)
        {
            if (AllyCharater[i] != null)
            {
                AllyCharacterList.Add(AllyCharater[i]);
            }
        }
        for (int i = 0; i < 5; i++)
        {
            if (EnemyCharater[i] != null)
            {
                EnemyCharacterList.Add(EnemyCharater[i]);
            }
        }
    }

    public void setAllyCharacterList(List<GameObject> allyList)
    {
        AllyCharacterList = allyList;
    }

    public List<GameObject> getAllyCharacterList()
    {
        return AllyCharacterList;
    }

    public List<GameObject> getEnemyCharacterList()
    {
        return EnemyCharacterList;
    }

    public void AddAllyList(int num, GameObject gameObject)
    {

        AllyCharacterList.Insert(num, gameObject);


    }






}
