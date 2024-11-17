using System;
using UnityEngine;

public class UIController : MonoBehaviour, IDataPersistent
{
    public GameObject panel;
    public GameObject highlight;
    public GameObject player;
    public GameObject Map2_Rune;
    public GameObject[] Map;
    public GameObject[] mapPuzzle;

    Vector3[] map_Position;
    Vector2[] mapPuzzle_Position;
    int[] mapIndex;
    int mapCount;
    bool isMapOpen = false;
    int currentIndexOfMap = 0;
    int selectedMapIndex = -1;
    int playerIndex = 0;
    float count = 1.0f;
    void Start()
    {
        CloseMapPuzzle();
        GetMapInfo();
        EventManager.instance.Map2RuneActiveEvent += ActiveMap2PuzzleRune;
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

        if (GameState.Instance.GetCurrentLevel() == 1)
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
        GameState.Instance.SetPlayerFronze(true);
        SetCurrentMapIndex();
    }

    void CloseMapPuzzle()
    {
        panel.SetActive(false);
        isMapOpen = false;
        GameState.Instance.SetPlayerFronze(false);
        selectedMapIndex = -1;
    }

    void GetMapInfo()
    {
        mapCount = Map.Length;
        map_Position = new Vector3[mapCount];
        mapPuzzle_Position = new Vector2[mapCount];
        mapIndex = new int[mapCount];
        for (int i = 0; i < mapCount; i++)
        {
            map_Position[i] = Map[i].transform.position;
            mapPuzzle_Position[i] = mapPuzzle[i].transform.position;
            mapIndex[i] = i;
        }
    }

    void SetCurrentMapIndex()
    {
        var playerPosition = player.transform.position;
        if (GameState.Instance.GetCurrentLevel() == 1)
        {
            var level1XPosition = new float[3] { -14, 13, 39 };
            for (int i = 0; i < mapCount; i++)
            {
                if (playerPosition.x < level1XPosition[i])
                {
                    currentIndexOfMap = i;
                    break;
                }
            }

        }
        else
        {
            if (playerPosition.x < 13)
            {
                currentIndexOfMap = playerPosition.y > -7 ? 0 : 2;
            }
            else
            {
                currentIndexOfMap = playerPosition.y > -7 ? 1 : 3;
            }
        }
        playerIndex = currentIndexOfMap;

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
            var moving = map_Position[i] - Map[index].transform.position;
            if (i == playerIndex)
            {
                player.transform.position -= moving;
            }
            if (index == 0 && moving != Vector3.zero)
            {
                GameState.Instance.SetPlayerInitPosition(moving);
            }
            Map[index].transform.position = map_Position[i];
            mapIndex[i] = index;
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
            }
        }
    }

    void ActiveMap2PuzzleRune()
    {
        if (GameState.Instance.GetCurrentLevel() == 2)
        {
            Map2_Rune.SetActive(true);
            mapPuzzle[1] = Map2_Rune;
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

    public void LoadData(GameState data)
    {
        mapIndex = GameState.Instance.GetMapArrangement();
        for (int i = 0; i < mapCount; i++)
        {
            Map[i].transform.position = map_Position[mapIndex[i]];
            mapPuzzle[i].transform.position = mapPuzzle_Position[mapIndex[i]];
        }
    }

    public void SaveData()
    {
        GameState.Instance.SetMapArrangement(mapIndex);
    }
}
