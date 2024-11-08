using System.Numerics;

public class GameState
{
    private static GameState instance = null;
    private bool mapPuzzleActive;
    private bool monsterActive;
    private bool playerFronze;
    private int currentLevel;
    private int deadCount;
    private int gemCount;
    private bool hasWine;
    public Vector2[] playerLevelPosition = new Vector2[2] { new(-37.5f, -4.65f), new(-11f, 0f) };

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

    public Vector2 GetPlayerInitPosition()
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

    public void SetCurrentLevel(int level)
    {
        currentLevel = level;
    }
    public int GetCurrentLevel()
    {
        return currentLevel;
    }
    public void SetDeadCount(int count)
    {
        deadCount = count;
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