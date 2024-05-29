using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //private static GameManager _instance;

    public static GameManager Instance = null;
    private SoundManager soundManager;

    [SerializeField]
    public bool isBattle { get; set; }
    public bool isActiveAI { get; set; }
    [SerializeField]
    public int ScenesNum { get; set; }

    public int TrunNum { get; set; }

    public bool isOver { get; set; }

    public bool isCard { get; set; }

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
        soundManager = SoundManager.Instance;
        isBattle = true;
        isActiveAI = true;
        isOver = false;
        isCard = false;
        ScenesNum = 0;
        soundManager.PlayMusic(1);
        TrunNum = 0;
    }

    // Update is called once per frame
    void Update()
    {
 
    }



    void ControlBackGroundSound()
    {
        switch(ScenesNum)
        {
            case 0:
                soundManager.PlayMusic(1);
                break;
            case 1:
                soundManager.PlayMusic(2);
                break;
            case 2:
                soundManager.PlayMusic(3);
                break;

        }
    }

    public void ControlSFX()
    {

    }
}
