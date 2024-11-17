using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonTriggerController : MonoBehaviour
{
    public GameObject monster;
    public GameObject scrollMap;

    void Start()
    {
        EventManager.instance.ScrollMapActiveEvent += ScrollActive;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (!GameState.Instance.GetMonsterActive())
        {
            GameState.Instance.UpdateGameState(name);
            GameState.Instance.SetPlayerFronze(true);
            monster.SetActive(true);
        }
    }

    void ScrollActive()
    {
        GameState.Instance.SetActiveScrollMap(true);
        scrollMap.SetActive(true);
    }
}
