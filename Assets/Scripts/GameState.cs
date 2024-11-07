public class GameState
{
    private static GameState instance = null;
    private bool mapPuzzleActive;
    private bool monsterActive;
    private bool playerFronze;
    private int currentLevel;
    private bool hasWine;

    private GameState()
    {
        mapPuzzleActive = false;
        currentLevel = 1;
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
}