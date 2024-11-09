
using UnityEngine;
using UnityEngine.UI;

public class ChestController : ItemController
{
    private Animator anim;
    private bool isOpen;

    [SerializeField]
    public Text displayText;
    public override void Start()
    {
        base.Start();
        anim = GetComponent<Animator>();
        isOpen = false;
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
                this.GetComponent<Collider2D>().enabled = false;
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
}
