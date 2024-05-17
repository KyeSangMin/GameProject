using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UiButtonEvent : MonoBehaviour
{

    private ScenesManager scenesManager;
    private GameObject UIobject;
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.Instance;
        scenesManager = GameObject.Find("Camera").GetComponent<ScenesManager>();
        UIobject = GameObject.Find("SettingButton");
        if(UIobject != null)
        {
            if (UIobject.activeSelf == true)
            {
                UIobject.SetActive(false);
            }
        }
       
     
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    public void QuitButton()
    {
        scenesManager.QuitGame();
    }


    public void StartButton()
    {
        string name = "BattleScenes1";
        scenesManager.LoadSceneWithLoadingScreen(name, 2.5f);
    }

    public void SettingButton()
    {
        if(UIobject.activeSelf == false)
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

   
}
    