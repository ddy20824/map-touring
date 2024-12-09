using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EventItemController : ItemController, IDataPersistent
{
    public GameObject display;
    public GameObject button;

    public Text deadTotalText;
    public Text gemTotalText;

    public void LoadData(GameState data)
    {
        if (tag == "map")
        {
            if (GameState.Instance.GetMapPuzzleActive())
            {
                gameObject.SetActive(false);
                display.SetActive(true);
            }
            else
            {
                gameObject.SetActive(true);
                display.SetActive(false);
            }
        }
    }

    public void SaveData()
    {
    }

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
                    audioSource.PlayOneShot(effectSound);
                    gameObject.SetActive(false);
                    display.SetActive(true);
                    GameState.Instance.UpdateGameState(gameObject.name);
                }
                if (tag == "goal")
                {
                    int currentLevel = GameState.Instance.GetCurrentLevel();
                    if (GameState.Instance.GetIsRuneStone())
                    {
                        if (currentLevel == 1)
                        {
                            audioSource.PlayOneShot(effectSound);
                            display.SetActive(true);
                            StartCoroutine(Helper.Delay(MoveToNextLevel, 1.0f));
                        }
                        else if (currentLevel == 2)
                        {
                            audioSource.PlayOneShot(effectSound);
                            transform.Find("Glow").gameObject.SetActive(true);
                            GameState.Instance.SetPlayerFronze(true);
                            button.GetComponent<Button>().Select();
                            deadTotalText.text = GameState.Instance.GetTotalDeadCount().ToString();
                            gemTotalText.text = GameState.Instance.GetTotalGemCount().ToString();
                            display.SetActive(true);
                        }
                    }
                    else
                    {
                        transform.Find("GoalHint").gameObject.SetActive(true);
                        StartCoroutine(Helper.Delay(CloseGoalHint, 1.0f));
                    }
                }
            }
        }
    }

    private void MoveToNextLevel()
    {
        GameState.Instance.SetCurrentLevel(2);
        DataPersistentManager.instance.LoadLevel("Level2");
        GameState.Instance.SetIsRuneStone(false);
    }

    private void CloseGoalHint()
    {
        transform.Find("GoalHint").gameObject.SetActive(false);
    }
}
