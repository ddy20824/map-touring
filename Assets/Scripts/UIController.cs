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
    public int mapCount;

    GameObject[] mapPuzzle_Level1;
    float[] map_XPosition_Level1;
    public bool isMapOpen = false;
    int currentIndexOfMap = 0;
    int selectedMapIndex = -1;
    float count = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        closePanel();
        GetMapInfo();
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
                mapPuzzle_Level1[selectedMapIndex].transform.GetChild(0).gameObject.SetActive(true);
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
                var expectIndex = currentIndexOfMap - 1;
                if (expectIndex == selectedMapIndex)
                {
                    if (expectIndex != 0)
                    {
                        expectIndex--;
                    }
                    else
                    {
                        expectIndex++;
                    }
                }
                currentIndexOfMap = expectIndex;
                highlight.transform.position = mapPuzzle_Level1[currentIndexOfMap].transform.position;
            }
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (currentIndexOfMap < mapCount - 1)
            {
                var expectIndex = currentIndexOfMap + 1;
                if (expectIndex == selectedMapIndex)
                {
                    if (expectIndex < mapCount - 1)
                    {
                        expectIndex++;
                    }
                    else
                    {
                        expectIndex--;
                    }
                }
                currentIndexOfMap = expectIndex;
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

    void GetMapInfo()
    {
        var map = panel.transform.GetChild(0).gameObject;
        mapPuzzle_Level1 = new GameObject[mapCount];
        map_XPosition_Level1 = new float[mapCount];
        for (int i = 0; i < mapCount; i++)
        {
            mapPuzzle_Level1[i] = map.transform.GetChild(i).gameObject;
            map_XPosition_Level1[i] = Map[i].transform.position.x;
        }
    }

    void SetCurrentMapIndex()
    {
        var level1XPosition = new float[3] { -14, 13, 39 };
        var playerPosition = player.transform.position.x;
        for (int i = 0; i < mapCount; i++)
        {
            if (playerPosition < level1XPosition[i])
            {
                currentIndexOfMap = i;
                highlight.transform.position = mapPuzzle_Level1[currentIndexOfMap].transform.position;
                break;
            }
        }
    }

    void ExchangeMapPuzzle()
    {
        mapPuzzle_Level1[selectedMapIndex].transform.GetChild(0).gameObject.SetActive(false);
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
