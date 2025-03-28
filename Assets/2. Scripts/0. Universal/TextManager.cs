using UnityEngine;
using TMPro;

public class TextManager : MonoBehaviour
{
    public TMP_Text[] textElements;
    [SerializeField] private int currentTextIndex;

    void Start()
    {
        HideAllText();
        UpdateText();
    }

    public void ShowText(int index)
    {
        HideAllText();
        if (index >= 0 && index < textElements.Length)
        {
            textElements[index].gameObject.SetActive(true);
            currentTextIndex = index;
            UpdateText();
        }
    }

    public void EnableText(int index)
    {
        if (index >= 0 && index < textElements.Length)
        {
            textElements[index].gameObject.SetActive(true);
            currentTextIndex = index;
            UpdateText();
        }
    }

    public void HideText(int index)
    {
        if (index >= 0 && index < textElements.Length)
        {
            textElements[index].gameObject.SetActive(false);
            UpdateText();
        }
    }

    public void HideAllText()
    {
        foreach (var textElement in textElements)
        {
            textElement.gameObject.SetActive(false);
        }
    }

    private void UpdateText()
    {
        Debug.Log($"Current Text Index: {currentTextIndex}");
    }
}
