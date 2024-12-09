using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LoadongController : MonoBehaviour
{
    public GameObject loadingCanvas;
    public TMP_Text LoadingProgressText;

    private string fullText = "Loading...";

    void Start()
    {
        EventManager.instance.LoadingActiveEvent += DisplayLoadingProgress;
    }


    void DisplayLoadingProgress()
    {
        if (!loadingCanvas.activeSelf)
        {
            loadingCanvas.SetActive(true);
        }
        int nowTextLength = LoadingProgressText.text.Length;
        if (nowTextLength == fullText.Length)
        {
            LoadingProgressText.text = "Loading";
        }
        else if (nowTextLength < fullText.Length)
        {
            LoadingProgressText.text += fullText[nowTextLength];
        }

    }
}
