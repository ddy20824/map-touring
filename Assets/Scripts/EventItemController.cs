using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EventItemController : ItemController
{
    public GameObject display;
    public override void Start()
    {
        base.Start();
        display.SetActive(false);
    }

    public override void Update()
    {
        if (isTrigger)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                base.Update();
                if (tag == "map")
                {
                    gameObject.SetActive(false);
                    display.SetActive(true);
                    GameState.Instance.UpdateGameState(gameObject.name);
                }
                if (tag == "goal")
                {
                    int currentLevel = GameState.Instance.GetCurrentLevel();
                    if (GameState.Instance.GetIsRuneStone() && currentLevel == 1)
                    {
                        display.SetActive(true);
                        StartCoroutine(Helper.Delay(MoveToNextLevel, 1.0f));

                    }
                    else
                    {
                        display.SetActive(true);
                        GameState.Instance.SetPlayerFronze(true);
                    }
                }
            }
        }
    }

    private void MoveToNextLevel()
    {
        GameState.Instance.SetCurrentLevel(2);
        SceneManager.LoadScene("Level2");
    }
}
