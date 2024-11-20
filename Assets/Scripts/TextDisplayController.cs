using System.Collections;
using UnityEngine;
using TMPro;

public class TextDisplayController : MonoBehaviour
{
    public TMP_Text textMeshPro;

    [TextArea]
    private string fullText;
    public float typeSpeed = 0.1f;

    private void OnEnable()
    {
        fullText = GameState.Instance.GetBubbleText();
        StartCoroutine(ShowText());
        StartCoroutine(Helper.Delay(CloseText, 2.0f));
    }

    private IEnumerator ShowText()
    {
        textMeshPro.text = "";
        textMeshPro.gameObject.SetActive(true);

        for (int i = 0; i < fullText.Length; i++)
        {
            textMeshPro.text += fullText[i];

            yield return new WaitForSeconds(typeSpeed);
        }
    }

    private void CloseText()
    {
        textMeshPro.gameObject.SetActive(false);
    }
}