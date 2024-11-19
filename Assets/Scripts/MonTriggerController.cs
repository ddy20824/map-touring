using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonTriggerController : MonoBehaviour, IDataPersistent
{
    public GameObject monster;
    public GameObject scrollMap;

    void Start()
    {
        EventManager.instance.ScrollMapActiveEvent += ScrollActive;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (!GameState.Instance.GetMonsterActive() && !GameState.Instance.GetActiveScrollMap() && !GameState.Instance.GetActiveMap2Rune())
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

    public void LoadData(GameState data)
    {
        if (GameState.Instance.GetActiveScrollMap())
        {
            scrollMap.SetActive(true);
        }
        else
        {
            scrollMap.SetActive(false);
        }

        if (GameState.Instance.GetMonsterActive())
        {
            monster.SetActive(true);
        }
        else
        {
            monster.SetActive(false);
        }
    }

    public void SaveData()
    {
    }
}
