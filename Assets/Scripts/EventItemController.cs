using UnityEngine;
using UnityEngine.SceneManagement;

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
                    if (GameState.Instance.GetIsRuneStone())
                    {
                        display.SetActive(true);
                        StartCoroutine(Helper.Delay(MoveToNextLevel, 1.0f));

                    }
                }
            }
        }
    }

    private void MoveToNextLevel()
    {
        SceneManager.LoadScene("Level2");
    }
}
