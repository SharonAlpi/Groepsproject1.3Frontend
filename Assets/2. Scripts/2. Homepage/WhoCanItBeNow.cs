using System.Collections.Generic;
using UnityEngine;

public class WhoCanItBeNow : MonoBehaviour
{
    public List<GameObject> characterImages = new List<GameObject>();

    private ValueManager valueManager;

    private void OnEnable()
    {
        valueManager = FindFirstObjectByType<ValueManager>();
        ShowSprite(valueManager.currentPerson);
    }

    public void ShowSprite(int index)
    {
        if (characterImages == null || characterImages.Count == 0)
        {
            Debug.LogWarning("Character list is empty!");
            return;
        }

        if (index < 0 || index >= characterImages.Count)
        {
            Debug.LogWarning("Index out of range!");
            return;
        }

        // Disable all GameObjects
        foreach (GameObject obj in characterImages)
        {
            obj.SetActive(false);
        }

        // Enable the correct one
        characterImages[index].SetActive(true);
    }
}
