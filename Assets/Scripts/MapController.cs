using UnityEngine;

public class MapController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        EventManager.instance.Map2RuneActiveEvent += ActiveMap2Rune;
    }

    // Update is called once per frame
    void Update()
    {

    }
    void ActiveMap2Rune()
    {
        var Map2_Rune = transform.Find("Map2_Rune");
        Map2_Rune?.gameObject.SetActive(true);
    }
}
