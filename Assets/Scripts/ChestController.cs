
using UnityEngine;
using UnityEngine.UI;

public class ChestController : ItemController, IDataPersistent
{
    private Animator anim;
    private bool isOpen;

    [SerializeField]
    public Text displayText;
    public override void Start()
    {
        base.Start();
        isOpen = false;
    }

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    public override void Update()
    {
        if (!isOpen && isTrigger)
        {
            base.Update();
            if (Input.GetKeyDown(KeyCode.F))
            {
                isOpen = true;
                anim.SetBool("IsOpened", true);
                GetComponent<Collider2D>().enabled = false;
                GameState.Instance.SetChestBoxName(name);
                if (tag == "gemChest")
                {
                    UpdateGemCount();
                }
                if (tag == "runeChest")
                {
                    UpdateRuneCount();
                }
            }
        }
    }

    private void UpdateGemCount()
    {
        int gemCount = GameState.Instance.GetGemCount() + 1;
        GameState.Instance.SetGemCount(gemCount);
        displayText.text = gemCount.ToString();
    }

    private void UpdateRuneCount()
    {
        GameState.Instance.SetIsRuneStone(true);
        displayText.text = "1";
    }

    public void LoadData(GameState data)
    {
        if (GameState.Instance.CheckChestBoxNameExist(name))
        {
            isOpen = true;
            anim.SetBool("IsOpened", true);
            GetComponent<Collider2D>().enabled = false;
        }
        if (tag == "gemChest")
        {
            displayText.text = GameState.Instance.GetGemCount().ToString();
        }
        if (tag == "runeChest" && GameState.Instance.GetIsRuneStone())
        {
            displayText.text = "1";
        }
    }

    public void SaveData()
    {

    }
}
