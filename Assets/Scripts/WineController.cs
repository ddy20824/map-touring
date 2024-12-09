using UnityEngine;
using UnityEngine.UI;

public class WineController : ItemController, IDataPersistent
{
    [SerializeField]
    public Text displayText;

    public void LoadData(GameState data)
    {
        if (GameState.Instance.GetHasWine())
        {
            gameObject.SetActive(false);
            displayText.text = "1";
        }
        else
        {
            gameObject.SetActive(true);
            displayText.text = "0";
        }
    }

    public void SaveData()
    {
    }

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
                audioSource.PlayOneShot(effectSound);
                gameObject.SetActive(false);
                GameState.Instance.UpdateGameState(gameObject.name);
                displayText.text = "1";
            }
        }
    }
}
