using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DataPersistentManager : MonoBehaviour
{
    public static DataPersistentManager instance { get; private set; }
    private List<IDataPersistent> dataPersistentObjects;
    private FileHandler fileHandler;
    // Start is called before the first frame update
    void Start()
    {
        this.dataPersistentObjects = FindAllDataPersistentObjects();
        this.fileHandler = new FileHandler("Save.sav");
        NewGame();
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
    public void NewGame()
    {
        GameState.Instance.CreateNewGame();
    }

    public void LoadGame()
    {
        var gameState = fileHandler.Load();
        if (gameState == null)
        {
            Debug.Log("No gameState");
            NewGame();
        }

        GameState.Instance.SetGameState(gameState);
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
