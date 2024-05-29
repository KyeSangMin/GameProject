using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class UiButtonEvent : MonoBehaviour
{

    private ScenesManager scenesManager;
    private SoundManager soundManager;
    private GameObject UIobject;
    private GameObject gameOver;
    private GameManager gameManager;
    AudioSource source;
    public TextMeshProUGUI tmp;


    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.Instance;
        soundManager = SoundManager.Instance;
        scenesManager = GameObject.Find("Camera").GetComponent<ScenesManager>();
        UIobject = GameObject.Find("SettingButton");
        gameOver = GameObject.Find("Gameover");
        source = soundManager.sfxSorce.transform.GetChild(0).GetComponent<AudioSource>();
        
        if (UIobject != null)
        {
            if (UIobject.activeSelf == true)
            {
                UIobject.SetActive(false);
            }
        }
        if (gameOver != null)
        {
            if (gameOver.activeSelf == true)
            {
                gameOver.SetActive(false);
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.isBattle && tmp != null)
        {
            int trun = gameManager.TrunNum;
            string temp = trun.ToString();
            tmp.text = temp;
        }

        if(gameManager.isOver)
        {
            GameOver();
        }
    }



    public void QuitButton()
    {
        scenesManager.QuitGame();
    }


    public void StartButton()
    {
        soundManager.PlayEffect(source.clip);
        string name = "BattleScenes1";
        gameManager.ScenesNum = 1;
        gameManager.isActiveAI = true;
        gameManager.isBattle = true;
        scenesManager.LoadSceneWithLoadingScreen(name, 2.05f);
    }

    public void SettingButton()
    {
        soundManager.PlayEffect(source.clip);
        if (UIobject.activeSelf == false)
        {
            UIobject.SetActive(true);
            gameManager.isActiveAI = false;
            gameManager.isBattle = false;
        }
        else
        {
            UIobject.SetActive(false);
            gameManager.isActiveAI = true;
            gameManager.isBattle = true;

        }
    }

    public void GameOver()
    {
        gameOver.SetActive(true);
        gameManager.isActiveAI = false;
        gameManager.isBattle = false;
    }

    public void continueButton()
    {
        soundManager.PlayEffect(source.clip);
        string name = "start";
        gameManager.ScenesNum = 0;
        gameOver.SetActive(false);
        gameManager.isOver = false;
        scenesManager.LoadSceneWithLoadingScreen(name, 1.0f);
        
    }
   
}
    