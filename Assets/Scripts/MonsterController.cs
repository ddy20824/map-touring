using UnityEngine;

public class MonsterController : MonoBehaviour
{
    Animator anim;
    // bool isAutoMon;
    bool isHiddenMon;
    // Vector3 position1;
    // Vector3 position2;
    // int direction = -1;
    // bool isAttacking = false;
    public AudioSource audioSource;
    public AudioClip effectSound;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        // isAutoMon = CompareTag("autoMon");
        isHiddenMon = CompareTag("hiddenMon");
        // if (isAutoMon)
        // {
        //     anim.SetBool("IsRun", true);
        //     position1 = new Vector3(transform.localPosition.x, transform.localPosition.y, 0);
        //     position2 = new Vector3(transform.localPosition.x + 11, transform.localPosition.y, 0);
        // }
        // else 
        if (isHiddenMon)
        {
            StartCoroutine(Helper.Delay(SetMonsterIdleAnimation, 0.1f));
        }
    }
    // private float duration = 0;
    void Update()
    {
        //start -13, end -2
        // if (isAutoMon && !isAttacking)
        // {
        //     position1.x = -13 + Helper.FindParentWithTag(this.gameObject, "map").transform.localPosition.x;
        //     position2.x = -2 + Helper.FindParentWithTag(this.gameObject, "map").transform.localPosition.x;
        //     if (direction == -1)
        //     {
        //         if (duration < 2.5)
        //         {
        //             transform.localScale = new Vector3(direction * 5, 5, 5);
        //             transform.localPosition = Vector3.Lerp(transform.localPosition, position2, 0.005f);
        //             duration += Time.deltaTime;

        //         }
        //         else
        //         {
        //             direction = 1;
        //             duration = 0;
        //         }
        //     }
        //     else
        //     {
        //         if (duration < 2.5)
        //         {
        //             transform.localScale = new Vector3(direction * 5, 5, 5);
        //             transform.localPosition = Vector3.Lerp(transform.localPosition, position1, 0.005f);
        //             duration += Time.deltaTime;
        //         }
        //         else
        //         {
        //             direction = -1;
        //             duration = 0;
        //         }
        //     }
        // }
    }

    void SetMonsterIdleAnimation()
    {
        anim.SetBool("IsAppear", false);
        StartCoroutine(Helper.Delay(ActiveMonsterAppearText, 0.7f));
    }
    void ActiveMonsterAppearText()
    {
        GameState.Instance.SetPlayerFronze(false);
        GameState.Instance.SetBubbleState("taskRequest");
        EventManager.instance.TriggerMap2AppearMonster();
    }

    void SetMonsterAttackAnimation()
    {
        anim.SetBool("IsAttack", true);
        audioSource.PlayOneShot(effectSound);
        // isAttacking = true;
        StartCoroutine(Helper.Delay(StopMonsterAnimation, 0.7f));
    }

    void StopMonsterAnimation()
    {
        GameState.Instance.SetPlayerFronze(false);
        anim.SetBool("IsAttack", false);
        // isAttacking = false;
        GameState.Instance.SetPlayerDie(true);
    }
    void MonsterDisappear()
    {
        anim.SetTrigger("Disappear");
        StartCoroutine(Helper.Delay(AfterMonsterDisappear, 1f));
    }

    void AfterMonsterDisappear()
    {
        GameState.Instance.SetPlayerFronze(false);
        gameObject.SetActive(false);
        GameState.Instance.SetMonsterActive(false);
        EventManager.instance.TriggerScrollMapActive();
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (GameState.Instance.GetCurrentLevel() == 2 && GameState.Instance.GetHasWine() && isHiddenMon)
        {
            GameState.Instance.SetPlayerFronze(true);
            GameState.Instance.SetBubbleState("taskComplete");
            EventManager.instance.TriggerMap2AppearMonster();
            StartCoroutine(Helper.Delay(MonsterDisappear, 1f));
        }
        else
        {
            if (other.gameObject.name == "Player")
            {
                // if (isAutoMon)
                // {
                //     direction = (this.transform.position.x > other.transform.position.x) ? 1 : -1;
                //     transform.localScale = new Vector3(direction * 5, 5, 5);
                // }
                GameState.Instance.SetPlayerFronze(true);
                if (isHiddenMon)
                {
                    GameState.Instance.SetBubbleState("taskRequest");
                    EventManager.instance.TriggerMap2AppearMonster();
                    StartCoroutine(Helper.Delay(SetMonsterAttackAnimation, 2.0f));
                }
                else
                {
                    SetMonsterAttackAnimation();
                }
            }
        }
    }
}
