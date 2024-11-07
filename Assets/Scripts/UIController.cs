using UnityEngine;

public class UIController : MonoBehaviour
{
    public GameObject panel;
    public GameObject highlight;
    public GameObject player;
    public GameObject[] Map;
    public GameObject[] mapPuzzle;
    public int level;

    Vector2[] map_Position;
    Vector2[] mapPuzzle_Position;
    int mapCount;
    public bool isMapOpen = false;
    int currentIndexOfMap = 0;
    int selectedMapIndex = -1;
    float count = 1.0f;
    void Start()
    {
        CloseMapPuzzle();
        GetMapInfo();
    }

    void Update()
    {
        if (GameState.Instance.GetMapPuzzleActive() && Input.GetKeyDown(KeyCode.M))
        {
            if (isMapOpen)
            {
                CloseMapPuzzle();
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
                mapPuzzle[selectedMapIndex].transform.GetChild(0).gameObject.SetActive(true);
            }
            else
            {
                ExchangeMapPuzzle();
                ExchangeMap();
            }
        }

        if (level == 1)
        {
            Level1Control();
        }
        else
        {
            Level2Control();
        }

        highlight.transform.position = mapPuzzle[currentIndexOfMap].transform.position;
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

    void CloseMapPuzzle()
    {
        panel.SetActive(false);
        isMapOpen = false;
        selectedMapIndex = -1;
    }

    void GetMapInfo()
    {
        mapCount = Map.Length;
        map_Position = new Vector2[mapCount];
        mapPuzzle_Position = new Vector2[mapCount];
        for (int i = 0; i < mapCount; i++)
        {
            map_Position[i] = Map[i].transform.position;
            mapPuzzle_Position[i] = mapPuzzle[i].transform.position;
        }
    }

    void SetCurrentMapIndex()
    {
        var level1XPosition = new float[3] { -14, 13, 39 };
        if (level == 1)
        {
            var playerPosition = player.transform.position.x;
            for (int i = 0; i < mapCount; i++)
            {
                if (playerPosition < level1XPosition[i])
                {
                    currentIndexOfMap = i;
                    break;
                }
            }

        }
        else
        {
            var playerPosition = player.transform.position;
            if (playerPosition.x < 13)
            {
                currentIndexOfMap = playerPosition.y > -7 ? 0 : 2;
            }
            else
            {
                currentIndexOfMap = playerPosition.y > -7 ? 1 : 3;
            }
        }
    }

    void ExchangeMapPuzzle()
    {
        mapPuzzle[selectedMapIndex].transform.GetChild(0).gameObject.SetActive(false);
        var tempPosition = mapPuzzle[selectedMapIndex].transform.position;
        var selected = mapPuzzle[selectedMapIndex];
        var current = mapPuzzle[currentIndexOfMap];
        selected.transform.position = mapPuzzle[currentIndexOfMap].transform.position;
        current.transform.position = tempPosition;
        mapPuzzle[selectedMapIndex] = current;
        mapPuzzle[currentIndexOfMap] = selected;
        selectedMapIndex = -1;
        // count = 0.0f;
    }

    void ExchangeMap()
    {
        for (int i = 0; i < mapCount; i++)
        {
            var name = mapPuzzle[i].name;
            var index = int.Parse(name.Substring(name.Length - 1)) - 1;
            Map[index].transform.position = map_Position[i];
        }
    }

    void Level2Control()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (currentIndexOfMap % 2 != 0)
            {
                currentIndexOfMap--;
            }
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (currentIndexOfMap % 2 == 0)
            {
                currentIndexOfMap++;
            }
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (currentIndexOfMap < 4 && currentIndexOfMap > 1)
            {
                currentIndexOfMap -= 2;
            }

        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (currentIndexOfMap >= 0 && currentIndexOfMap < 2)
            {
                currentIndexOfMap += 2;
            }
        }
        // highlight.transform.position = mapPuzzle[currentIndexOfMap].transform.position;
    }
    void Level1Control()
    {
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
                // highlight.transform.position = mapPuzzle[currentIndexOfMap].transform.position;
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
                // highlight.transform.position = mapPuzzle[currentIndexOfMap].transform.position;
            }
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
}
