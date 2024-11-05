public class GameState
{
    private static GameState instance = null;
    private bool mapPuzzleActive;

    private GameState()
    {
        mapPuzzleActive = false;
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

    public void setMapPuzzleActive(bool active)
    {
        mapPuzzleActive = active;
    }
    public bool getMapPuzzleActive()
    {
        return mapPuzzleActive;
    }
}