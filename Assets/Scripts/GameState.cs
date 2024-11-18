using System.Collections.Generic;
using System.Linq;
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
    private int[] deadCount;
    [SerializeField]
    private int[] gemCount;
    [SerializeField]
    private bool isRuneStone;
    [SerializeField]
    private string[] recordChestBoxName;
    [SerializeField]
    private int[] mapArrangement;
    [SerializeField]
    private bool hasWine;
    [SerializeField]
    private bool activeScrollMap;
    [SerializeField]
    private bool activeMap2Rune;
    private bool isPlayerFronze;
    private bool isPlayerDie;
    [SerializeField]
    private Vector3[] PlayerInitPosition = new Vector3[2] { new(-37.5f, -4.65f, 0), new(-11f, 0f, 0) };
    [SerializeField]
    private Vector3[] PlayerPosition = new Vector3[2] { new(-37.5f, -4.65f, 0), new(-11f, 0f, 0) };
    private HashSet<string> chestBoxName;

    private GameState()
    {
        mapPuzzleActive = false;
        monsterActive = false;
        currentLevel = 1;
        deadCount = new int[2] { 0, 0 };
        gemCount = new int[2] { 0, 0 };
        chestBoxName = new HashSet<string>();
        mapArrangement = new int[4] { 0, 1, 2, 3 };
        isRuneStone = false;
        hasWine = false;
        PlayerInitPosition = new Vector3[2] { new(-37.5f, -4.65f, 0), new(-11f, 0f, 0) };
        PlayerPosition = new Vector3[2] { new(-37.5f, -4.65f, 0), new(-11f, 0f, 0) };
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

    public void CreateNewGame()
    {
        instance = new GameState();
    }

    public void SetGameState(GameState gameState)
    {
        instance = gameState;
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
            case "Scroll":
                SetActiveMap2Rune(true);
                break;
            default:
                break;
        }
    }

    public Vector3 GetPlayerPosition()
    {
        return PlayerPosition[currentLevel - 1];
    }
    public void SetPlayerPosition(Vector3 position)
    {
        PlayerPosition[currentLevel - 1] = position;
    }
    public Vector3 GetPlayerInitPosition()
    {
        return PlayerInitPosition[currentLevel - 1];
    }
    public void SetPlayerInitPosition(Vector3 move)
    {
        PlayerInitPosition[currentLevel - 1] += move;
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
        isPlayerFronze = fronze;
    }

    public bool GetPlayerFronze()
    {
        return isPlayerFronze;
    }
    public void SetPlayerDie(bool die)
    {
        isPlayerDie = die;
    }

    public bool GetPlayerDie()
    {
        return isPlayerDie;
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
        deadCount[currentLevel - 1]++;
    }
    public int GetDeadCount()
    {
        return deadCount[currentLevel - 1];
    }

    public void SetGemCount(int count)
    {
        gemCount[currentLevel - 1] = count;
    }
    public int GetGemCount()
    {
        return gemCount[currentLevel - 1];
    }

    public void SetIsRuneStone(bool status)
    {
        isRuneStone = status;
    }
    public bool GetIsRuneStone()
    {
        return isRuneStone;
    }
    public void SetChestBoxName(string name)
    {
        chestBoxName.Add(name);
    }
    public bool CheckChestBoxNameExist(string name)
    {
        return chestBoxName.Contains(name);
    }
    public void CastCheckBoxNameToArray()
    {
        recordChestBoxName = chestBoxName.ToArray();
    }
    public void CastCheckBoxNameToSet()
    {
        chestBoxName = new HashSet<string>(recordChestBoxName);
    }
    public void SetMapArrangement(int[] mapNames)
    {
        mapArrangement = mapNames;
    }
    public int[] GetMapArrangement()
    {
        return mapArrangement;
    }
    public void SetActiveScrollMap(bool active)
    {
        activeScrollMap = active;
    }
    public bool GetActiveScrollMap()
    {
        return activeScrollMap;
    }

    public void SetActiveMap2Rune(bool active)
    {
        activeMap2Rune = active;
    }
    public bool GetActiveMap2Rune()
    {
        return activeMap2Rune;
    }
}