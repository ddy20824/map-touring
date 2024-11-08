using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonTriggerController : MonoBehaviour
{
    public GameObject monster;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!GameState.Instance.GetMonsterActive())
        {
            GameState.Instance.UpdateGameState(name);
            GameState.Instance.SetPlayerFronze(true);
            monster.SetActive(true);
        }
    }
}
