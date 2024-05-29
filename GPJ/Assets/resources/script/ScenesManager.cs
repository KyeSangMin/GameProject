using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenesManager : MonoBehaviour
{
    private static ScenesManager _instance;
    private SoundManager soundManager;
    private GameManager gameManager;

    public static ScenesManager Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject go = new GameObject("main");
                _instance = go.AddComponent<ScenesManager>();
            }
            return _instance;
        }
    }
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }
        _instance = this;
        DontDestroyOnLoad(gameObject);
    }


    void Start()
    {
        gameManager = GameManager.Instance;
        soundManager = SoundManager.Instance;
        LoadScene("start");
    }

  
    void Update()
    {
        
    }



    public void LoadScene(string sceneName)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }

    public void LoadScene(int sceneIndex)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneIndex);
    }

    public void ReloadScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        //Application.Quit();
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                Application.Quit();
        #endif
    }

    public void LoadSceneWithLoadingScreen(string sceneName, float minLoadingTime)
    {

        float startTime = Time.time;


        soundManager.PauseMusic();
        UnityEngine.SceneManagement.SceneManager.LoadScene("LoadingScene");


        StartCoroutine(LoadSceneAsync(sceneName, startTime, minLoadingTime));
    }


    private IEnumerator LoadSceneAsync(string sceneName, float startTime, float minLoadingTime)
    {

        float elapsedTime = Time.time - startTime;
        float remainingTime = Mathf.Max(0f, minLoadingTime - elapsedTime);
        yield return new WaitForSeconds(remainingTime);
        AsyncOperation asyncLoad = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneName);
        soundManager.PlayMusic(gameManager.ScenesNum);

        
         while (!asyncLoad.isDone)
         {
             yield return null;
         }
        
    }

}


