using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollMapController : ItemController, IDataPersistent
{

    public override void Update()
    {
        if (isTrigger)
        {
            base.Update();
            if (Input.GetKeyDown(KeyCode.F))
            {
                gameObject.SetActive(false);
                GameState.Instance.SetActiveScrollMap(false);
                GameState.Instance.UpdateGameState(gameObject.name);
                EventManager.instance.TriggerMap2RuneActive();
            }
        }
    }

    public override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
        if (other.name == "Ground")
        {
            Destroy(gameObject.GetComponent<Rigidbody2D>());
        }

    }

    public void LoadData(GameState data)
    {
        if (GameState.Instance.GetActiveMap2Rune())
        {
            EventManager.instance.TriggerMap2RuneActive();
        }
    }

    public void SaveData()
    {
    }
}
