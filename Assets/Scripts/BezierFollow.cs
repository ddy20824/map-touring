using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierFollow : MonoBehaviour
{
    [SerializeField]
    private Transform[] routes;

    private int routeToGo;

    private float tParam;

    private Vector2 objectPosition;

    private float speedModifier;

    private bool coroutineAllowed;
    Animator anim;
    int direction = -1;
    bool isAttacking = false;
    public AudioSource audioSource;
    public AudioClip effectSound;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetBool("IsRun", true);
        routeToGo = 0;
        tParam = 0f;
        speedModifier = 0.2f;
        coroutineAllowed = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (coroutineAllowed && !isAttacking)
        {
            StartCoroutine(GoByTheRoute(routeToGo));
        }
    }

    private IEnumerator GoByTheRoute(int routeNum)
    {
        if (!isAttacking)
        {

            direction = routeNum == 0 ? -1 : 1;
            transform.localScale = new Vector3(direction * 5, 5, 5);
            coroutineAllowed = false;

            Vector2 p0 = routes[routeNum].GetChild(0).position;
            Vector2 p1 = routes[routeNum].GetChild(1).position;
            Vector2 p2 = routes[routeNum].GetChild(2).position;
            Vector2 p3 = routes[routeNum].GetChild(3).position;

            while (tParam < 1 && !isAttacking)
            {
                tParam += Time.deltaTime * speedModifier;

                objectPosition = Mathf.Pow(1 - tParam, 3) * p0 + 3 * Mathf.Pow(1 - tParam, 2) * tParam * p1 + 3 * (1 - tParam) * Mathf.Pow(tParam, 2) * p2 + Mathf.Pow(tParam, 3) * p3;

                transform.position = objectPosition;
                yield return new WaitForEndOfFrame();
            }

            if (!isAttacking) // Only reset parameters if not attacking
            {
                tParam = 0f;
                routeToGo += 1;

                if (routeToGo > routes.Length - 1)
                {
                    routeToGo = 0;
                }

            }
            coroutineAllowed = true;
        }

    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.name == "Player")
        {
            direction = (transform.position.x > other.transform.position.x) ? 1 : -1;
            transform.localScale = new Vector3(direction * 5, 5, 5);
            GameState.Instance.SetPlayerFronze(true);
            SetMonsterAttackAnimation();
        }
    }
    void SetMonsterAttackAnimation()
    {
        anim.SetBool("IsAttack", true);
        audioSource.PlayOneShot(effectSound);
        isAttacking = true;
        StartCoroutine(Helper.Delay(StopMonsterAnimation, 0.5f));
    }

    void StopMonsterAnimation()
    {
        GameState.Instance.SetPlayerFronze(false);
        anim.SetBool("IsAttack", false);
        isAttacking = false;
        GameState.Instance.SetPlayerDie(true);
    }
}
