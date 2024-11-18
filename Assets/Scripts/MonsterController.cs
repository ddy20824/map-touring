using UnityEngine;

public class MonsterController : MonoBehaviour
{
    Animator anim;
    bool isAutoMon;
    bool isHiddenMon;
    Vector3 position1;
    Vector3 position2;
    int direction = -1;
    bool isAttacking = false;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        isAutoMon = CompareTag("autoMon");
        isHiddenMon = CompareTag("hiddenMon");
        if (isAutoMon)
        {
            anim.SetBool("IsRun", true);
            position1 = new Vector3(-13, transform.position.y, 0);
            position2 = new Vector3(-2, transform.position.y, 0);
        }
        else if (isHiddenMon)
        {
            StartCoroutine(Helper.Delay(SetMonsterIdleAnimation, 0.1f));
        }
    }
    private float duration = 0;
    void Update()
    {
        //start -13, end -2
        if (isAutoMon && !isAttacking)
        {
            position1.x = -13 + Helper.FindParentWithTag(this.gameObject, "map").transform.position.x;
            position2.x = -2 + Helper.FindParentWithTag(this.gameObject, "map").transform.position.x;
            if (direction == -1)
            {
                if (duration < 2.5)
                {
                    transform.localScale = new Vector3(direction * 5, 5, 5);
                    transform.position = Vector3.Lerp(transform.position, position2, 0.001f);
                    duration += Time.deltaTime;

                }
                else
                {
                    direction = 1;
                    duration = 0;
                }
            }
            else
            {
                if (duration < 2.5)
                {
                    transform.localScale = new Vector3(direction * 5, 5, 5);
                    transform.position = Vector3.Lerp(transform.position, position1, 0.001f);
                    duration += Time.deltaTime;
                }
                else
                {
                    direction = -1;
                    duration = 0;
                }
            }
        }
    }

    void SetMonsterIdleAnimation()
    {
        anim.SetBool("IsAppear", false);
        GameState.Instance.SetPlayerFronze(false);
    }

    void StopMonsterAnimation()
    {
        anim.SetBool("IsAttack", false);
        isAttacking = false;
        GameState.Instance.SetPlayerDie(true);
    }

    void MonsterDisappear()
    {
        GameState.Instance.SetPlayerFronze(false);
        gameObject.SetActive(false);
        EventManager.instance.TriggerScrollMapActive();
    }


    void OnCollisionEnter2D(Collision2D other)
    {
        if (GameState.Instance.GetCurrentLevel() == 2 && GameState.Instance.GetHasWine() && isHiddenMon)
        {
            GameState.Instance.SetPlayerFronze(true);
            anim.SetTrigger("Disappear");
            StartCoroutine(Helper.Delay(MonsterDisappear, 1f));
        }
        else
        {
            if (other.gameObject.name == "Player")
            {
                if (isAutoMon)
                {
                    direction = (this.transform.position.x > other.transform.position.x) ? 1 : -1;
                    transform.localScale = new Vector3(direction * 5, 5, 5);
                }

                anim.SetBool("IsAttack", true);
                isAttacking = true;
                StartCoroutine(Helper.Delay(StopMonsterAnimation, 0.7f));
            }
        }
    }
}
