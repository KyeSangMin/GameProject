using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //private static GameManager _instance;

    public static GameManager Instance = null;

    private BattleSystem battleSystem;
    private AIController aIController;
    private MouseController mouseController;
    private BattleGrid battleGrid;
    private GridTile gridTile;
    [SerializeField]
    public bool isBattle { get; set; }
    public bool isActiveAI { get; set; }


    private void Awake()
    {
         if(Instance != null)
        {
            return;
        }

        Instance = this;
        
    }

    // Start is called before the first frame update
    void Start()
    {
        isBattle = true;
        isActiveAI = true;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
