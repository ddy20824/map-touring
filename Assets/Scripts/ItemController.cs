using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    private GameObject bubble;
    public GameObject display;
    // Start is called before the first frame update
    void Start()
    {
        // bubble = this.gameObject.transform.GetChild(0).gameObject;
        bubble = findChildByTag(this.transform, "bubble");
        bubble.SetActive(false);
        display.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Destroy(gameObject);
            display.SetActive(true);
            GameState.Instance.setMapPuzzleActive(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        bubble.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        bubble.SetActive(false);
    }

    private GameObject findChildByTag(Transform parent, string inputTag)
    {
        GameObject childWithTag = null;
        for (int i = 0; i < parent.childCount; i++)
        {
            if (parent.GetChild(i).CompareTag(inputTag))
            {
                childWithTag = parent.GetChild(i).gameObject;
                break;
            }
        }

        return childWithTag;
    }
}
