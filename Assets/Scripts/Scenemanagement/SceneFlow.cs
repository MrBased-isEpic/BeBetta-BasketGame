using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneFlow : MonoBehaviour
{

    #region Singleton

    public static SceneFlow Instance;

    public void Awake()
    {
        if (Instance == null) Instance = this;
    }

    #endregion
    
    public LoadingScreen loadingScreen;

    public string gameScene;
    public string mainMenuScene;

    private AsyncOperation loader;
    
    private Scene currentScene;

    private void Start()
    {
        LoadMainMenuScene();
    }

    public void LoadGameScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        Debug.Log(currentScene.path);
        if(currentScene.path == mainMenuScene)
            SceneManager.UnloadSceneAsync(mainMenuScene);

        loader = SceneManager.LoadSceneAsync(gameScene, LoadSceneMode.Additive);

        loader.completed += operation =>
        {
            currentScene = SceneManager.GetSceneByPath(gameScene);
            SceneManager.SetActiveScene(currentScene);
            loadingScreen.Hide();
        };
        
        loadingScreen.Show();
    }

    public void LoadMainMenuScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        if(currentScene.path == gameScene)
            SceneManager.UnloadSceneAsync(gameScene);
        
        
        loader = SceneManager.LoadSceneAsync(mainMenuScene, LoadSceneMode.Additive);

        loader.completed += operation =>
        {
            currentScene = SceneManager.GetSceneByPath(mainMenuScene);
            SceneManager.SetActiveScene(currentScene);
            loadingScreen.Hide();
        };
        
        loadingScreen.Show();
    }
}
