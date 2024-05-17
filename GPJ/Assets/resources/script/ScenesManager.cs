using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenesManager : MonoBehaviour
{
    private static ScenesManager _instance;
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

    // Start is called before the first frame update
    void Start()
    {
        LoadScene("start");
    }

    // Update is called once per frame
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
        // Record the start time
        float startTime = Time.time;

        // Load the loading scene first
        UnityEngine.SceneManagement.SceneManager.LoadScene("LoadingScene");

        // Start loading the target scene in the background
        StartCoroutine(LoadSceneAsync(sceneName, startTime, minLoadingTime));
    }

    // Coroutine to load the target scene asynchronously with minimum loading time
    private IEnumerator LoadSceneAsync(string sceneName, float startTime, float minLoadingTime)
    {
        // Start loading the target scene asynchronously
        
        Debug.Log("内风凭1");
        // Wait until the minimum loading time has passed
        float elapsedTime = Time.time - startTime;
        float remainingTime = Mathf.Max(0f, minLoadingTime - elapsedTime);
        Debug.Log(remainingTime);
        yield return new WaitForSeconds(remainingTime);
        AsyncOperation asyncLoad = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneName);
        Debug.Log("内风凭2");
        // Wait until the target scene is fully loaded
        
         while (!asyncLoad.isDone)
         {
             yield return null;
         }
        
    }

}


