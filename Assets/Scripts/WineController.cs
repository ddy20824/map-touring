using UnityEngine;
using UnityEngine.UI;

public class WineController : ItemController
{
    [SerializeField]
    public Text displayText;
    public override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        if (isTrigger)
        {
            base.Update();
            if (Input.GetKeyDown(KeyCode.F))
            {
                gameObject.SetActive(false);
                GameState.Instance.UpdateGameState(gameObject.name);
                displayText.text = "1";
            }
        }
    }
}
