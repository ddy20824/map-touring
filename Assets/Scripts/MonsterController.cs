using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        StartCoroutine(StopMonsterAnimation("IsAppear"));
    }

    IEnumerator StopMonsterAnimation(string param)
    {
        yield return new WaitForSeconds(0.1f);
        anim.SetBool(param, false);
    }


    void OnCollisionEnter2D(Collision2D other)
    {
        if (!GameState.Instance.GetHasWine())
        {
            anim.SetBool("IsAttack", true);
            StartCoroutine(StopMonsterAnimation("IsAttack"));
        }
        else
        {

        }
    }
}
