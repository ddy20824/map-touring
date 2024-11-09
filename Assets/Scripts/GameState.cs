using System.IO;
using Unity.VisualScripting;
using UnityEngine;

public class GameState
{
    private static GameState instance = null;
    [SerializeField]
    private bool mapPuzzleActive;
    [SerializeField]
    private bool monsterActive;
    [SerializeField]
    private int currentLevel;
    [SerializeField]
    private int deadCount;
    [SerializeField]
    private int gemCount;
    [SerializeField]
    private bool hasWine;
    private bool playerFronze;
    private bool playerDie;
    public Vector3[] playerLevelPosition = new Vector3[2] { new(-37.5f, -4.65f, 0), new(-11f, 0f, 0) };

    private GameState()
    {
        mapPuzzleActive = false;
        currentLevel = 1;
        deadCount = 0;
        gemCount = 0;
    }

    public static GameState Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameState();
            }
            return instance;
        }
    }

    public void UpdateGameState(string objectName)
    {
        switch (objectName)
        {
            case "MapPuzzleItem":
                SetMapPuzzleActive(true);
                break;
            case "MonTrigger":
                SetMonsterActive(true);
                break;
            case "Wine Bottle":
                SetHasWine(true);
                break;
            default:
                break;
        }
    }

    public void SaveGame(string filename)
    {
        Debug.Log(JsonUtility.ToJson(Instance));
        string json = JsonUtility.ToJson(GameState.Instance);
        string path = Path.Combine(Application.persistentDataPath, filename);

        File.WriteAllText(path, json);
        Debug.Log("Game Saved at:" + path);
    }

    public Vector3 GetPlayerInitPosition()
    {
        return playerLevelPosition[currentLevel - 1];
    }
    public void SetHasWine(bool hasWine)
    {
        this.hasWine = hasWine;
    }
    public bool GetHasWine()
    {
        return hasWine;
    }

    public void SetMapPuzzleActive(bool active)
    {
        mapPuzzleActive = active;
    }
    public bool GetMapPuzzleActive()
    {
        return mapPuzzleActive;
    }

    public void SetMonsterActive(bool active)
    {
        monsterActive = active;
    }
    public bool GetMonsterActive()
    {
        return monsterActive;
    }

    public void SetPlayerFronze(bool fronze)
    {
        playerFronze = fronze;
    }

    public bool GetPlayerFronze()
    {
        return playerFronze;
    }
    public void SetPlayerDie(bool die)
    {
        playerDie = die;
    }

    public bool GetPlayerDie()
    {
        return playerDie;
    }


    public void SetCurrentLevel(int level)
    {
        currentLevel = level;
    }
    public int GetCurrentLevel()
    {
        return currentLevel;
    }
    public void AddDeadCount()
    {
        deadCount++;
    }
    public int GetDeadCount()
    {
        return deadCount;
    }

    public void SetGemCount(int count)
    {
        gemCount = count;
    }
    public int GetGemCount()
    {
        return gemCount;
    }
}