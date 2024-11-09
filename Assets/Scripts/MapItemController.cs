using UnityEngine;

public class MapItemController : ItemController
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
                gameObject.SetActive(false);
                display.SetActive(true);
                GameState.Instance.UpdateGameState(gameObject.name);
            }
        }
    }
}
