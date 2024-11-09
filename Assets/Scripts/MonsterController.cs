using UnityEngine;

public class MonsterController : MonoBehaviour
{
    Animator anim;
    bool isAutoMon;
    Vector3 position1;
    Vector3 position2;
    int direction = -1;
    bool isAttacking = false;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        StartCoroutine(Helper.Delay(SetMonsterIdleAnimation, 0.1f));
        isAutoMon = CompareTag("autoMon");
        if (isAutoMon)
        {
            anim.SetBool("IsRun", true);
            position1 = new Vector3(-17, transform.position.y, 0);
            position2 = new Vector3(-9, transform.position.y, 0);
        }
    }
    private float duration = 0;
    void Update()
    {
        //start -17, end -9
        if (isAutoMon && !isAttacking)
        {
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


    void OnCollisionEnter2D(Collision2D other)
    {
        if (GameState.Instance.GetCurrentLevel() == 2 && GameState.Instance.GetHasWine())
        {
        }
        else
        {
            anim.SetBool("IsAttack", true);
            isAttacking = true;
            StartCoroutine(Helper.Delay(StopMonsterAnimation, 0.7f));
        }
    }
}
