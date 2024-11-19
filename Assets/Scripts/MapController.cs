using UnityEngine;

public class MapController : MonoBehaviour, IDataPersistent
{
    Transform map2_rune;
    // Start is called before the first frame update
    void Start()
    {
        map2_rune = transform.Find("Map2_Rune");
        EventManager.instance.Map2RuneActiveEvent += ActiveMap2Rune;
    }

    // Update is called once per frame
    void Update()
    {

    }
    void ActiveMap2Rune()
    {
        map2_rune?.gameObject.SetActive(true);
    }

    public void LoadData(GameState data)
    {
        if (GameState.Instance.GetActiveMap2Rune())
        {
            map2_rune?.gameObject.SetActive(true);
        }
        else
        {
            map2_rune?.gameObject.SetActive(false);
        }
    }

    public void SaveData()
    {
    }
}
