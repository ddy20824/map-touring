using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

public class DataPersistentManager : MonoBehaviour
{
    public static DataPersistentManager instance { get; private set; }
    private List<IDataPersistent> dataPersistentObjects;
    private FileHandler fileHandler;
    private GameState gameState;
    // Start is called before the first frame update
    void Start()
    {
        this.dataPersistentObjects = FindAllDataPersistentObjects();
        this.fileHandler = new FileHandler("Save.sav");
        SceneManager.sceneLoaded += OnSceneLoaded;
        //NewGame();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log("error");
        }
        instance = this;
    }

    private void OnDestroy()
    {
        // 取消訂閱，避免記憶體洩漏
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    public void NewGame()
    {
        if (GameState.Instance.GetCurrentLevel() == 2)
        {
            SceneManager.LoadScene("Level1");
        }
        GameState.Instance.CreateNewGame();
        foreach (IDataPersistent dataPersistent in dataPersistentObjects)
        {
            dataPersistent.LoadData(GameState.Instance);
        }
    }

    public void LoadGame()
    {
        gameState = fileHandler.Load();
        Debug.Log(gameState);
        if (gameState == null)
        {
            Debug.Log("No gameState");
            NewGame();
        }

        GameState.Instance.SetGameState(gameState);
        SceneManager.sceneLoaded += OnSceneLoaded;
        if (GameState.Instance.GetCurrentLevel() == 1)
        {
            LoadLevel("Level1");
        }
        else
        {
            LoadLevel("Level2");
        }
    }

    public void LoadLevel(string sceneName)
    {
        // 加載場景
        SceneManager.LoadScene(sceneName);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // 確保場景已加載完畢，可以安全地操作場景中的物件
        Debug.Log(scene.name + " Loaded");
        this.dataPersistentObjects = FindAllDataPersistentObjects();
        foreach (IDataPersistent dataPersistent in dataPersistentObjects)
        {
            dataPersistent.LoadData(gameState);
        }
    }

    public void SaveGame()
    {
        foreach (IDataPersistent dataPersistent in dataPersistentObjects)
        {
            dataPersistent.SaveData();
        }
        fileHandler.Save(GameState.Instance);
    }

    List<IDataPersistent> FindAllDataPersistentObjects()
    {
        IEnumerable<IDataPersistent> dataPersistents = FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistent>();
        return new List<IDataPersistent>(dataPersistents);
    }
}
