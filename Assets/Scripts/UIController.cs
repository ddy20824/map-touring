using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public GameObject panel;
    public GameObject highlight;
    public GameObject player;
    public GameObject[] Map;

    GameObject[] mapPuzzle_Level1;
    float[] map_XPosition_Level1 = new float[3] { -26, 0, 26 };
    int level1MapCount = 3;
    GameObject map;
    public bool isMapOpen = false;
    int currentIndexOfMap = 0;
    int childCount = 0;
    int selectedMapIndex = -1;
    float count = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        closePanel();
        map = panel.transform.GetChild(0).gameObject;
        childCount = map.transform.childCount;
        GetLevel1MapPuzzle();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            if (isMapOpen)
            {
                closePanel();
            }
            else
            {
                OpenMapPuzzle();
            }
        }
        if (Input.GetKeyDown(KeyCode.Return) && isMapOpen)
        {
            if (selectedMapIndex == -1)
            {
                selectedMapIndex = currentIndexOfMap;

            }
            else
            {
                ExchangeMapPuzzle();
                ExchangeMap();
            }
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (currentIndexOfMap != 0)
            {
                currentIndexOfMap--;
                highlight.transform.position = mapPuzzle_Level1[currentIndexOfMap].transform.position;
            }
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (currentIndexOfMap < childCount - 1)
            {
                currentIndexOfMap++;
                highlight.transform.position = mapPuzzle_Level1[currentIndexOfMap].transform.position;
            }
        }
        // if (count < 1.0f)
        // {
        //     var point1 = mapPuzzle_Level1[selectedMapIndex].transform.position;
        //     var point2 = mapPuzzle_Level1[currentIndexOfMap].transform.position;
        //     BezierMove(mapPuzzle_Level1[currentIndexOfMap], mapPuzzle_Level1[selectedMapIndex], point1, point2, Vector3.up);
        // }
        // else
        // {
        //     ExchangeMap();
        // }
    }

    void OpenMapPuzzle()
    {
        panel.SetActive(true);
        isMapOpen = true;
        SetCurrentMapIndex();
    }

    void GetLevel1MapPuzzle()
    {
        mapPuzzle_Level1 = new GameObject[childCount];
        for (int i = 0; i < childCount; i++)
        {
            mapPuzzle_Level1[i] = map.transform.GetChild(i).gameObject;
        }
    }

    void SetCurrentMapIndex()
    {
        var playerPosition = player.transform.position.x;
        for (int i = 0; i < level1MapCount; i++)
        {
            if (playerPosition < map_XPosition_Level1[i])
            {
                currentIndexOfMap = i;
                highlight.transform.position = mapPuzzle_Level1[currentIndexOfMap].transform.position;
                break;
            }
        }
    }

    void ExchangeMapPuzzle()
    {
        var tempPosition = mapPuzzle_Level1[selectedMapIndex].transform.position;
        var selected = mapPuzzle_Level1[selectedMapIndex];
        var current = mapPuzzle_Level1[currentIndexOfMap];
        selected.transform.position = mapPuzzle_Level1[currentIndexOfMap].transform.position;
        current.transform.position = tempPosition;
        mapPuzzle_Level1[selectedMapIndex] = current;
        mapPuzzle_Level1[currentIndexOfMap] = selected;
        selectedMapIndex = -1;
        // count = 0.0f;
    }

    void ExchangeMap()
    {
        for (int i = 0; i < mapPuzzle_Level1.Length; i++)
        {
            var name = mapPuzzle_Level1[i].name;
            var index = int.Parse(name.Substring(name.Length - 1)) - 1;
            Map[index].transform.position = new Vector2(map_XPosition_Level1[i], Map[index].transform.position.y);
        }
    }

    void BezierMove(GameObject gameObject1, GameObject gameObject2, Vector3 startingPoint, Vector3 endingPoint, Vector3 move)
    {
        var controlPoint1 = startingPoint + (endingPoint - startingPoint) / 2 + Vector3.up * 1.0f;
        var controlPoint2 = endingPoint + (startingPoint - endingPoint) / 2 + Vector3.down * 1.0f;
        if (count < 1.0f)
        {
            count += 1.0f * Time.deltaTime;

            Vector3 m1 = Vector3.Lerp(startingPoint, controlPoint1, count);
            Vector3 m2 = Vector3.Lerp(controlPoint1, endingPoint, count);
            gameObject1.transform.position = Vector3.Lerp(m1, m2, count);

            Vector3 m3 = Vector3.Lerp(endingPoint, controlPoint2, count);
            Vector3 m4 = Vector3.Lerp(controlPoint2, startingPoint, count);
            gameObject2.transform.position = Vector3.Lerp(m3, m4, count);
        }
    }

    void closePanel()
    {
        panel.SetActive(false);
        isMapOpen = false;
        selectedMapIndex = -1;
    }
}
