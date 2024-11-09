
using UnityEngine;
using UnityEngine.UI;

public class ChestController : ItemController
{
    private Animator anim;
    private bool isOpen;

    [SerializeField]
    public Text gemText;
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
                UpdateGemCount();
            }
        }
    }

    private void UpdateGemCount()
    {
        int gemCount = GameState.Instance.GetGemCount() + 1;
        GameState.Instance.SetGemCount(gemCount);
        gemText.text = gemCount.ToString();
    }
}
