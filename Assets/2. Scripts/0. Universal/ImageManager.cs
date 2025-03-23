using UnityEngine;

public class ImageManager : MonoBehaviour
{
    public GameObject[] images;  // Array om de afbeeldingen in op te slaan
    public ValueManager valueManager;

    private void Update()
    {

    }

    public void HideImage(int index)
    {
        if (index >= 0 && index < images.Length)
        {
            images[index].SetActive(false);  // Verberg de afbeelding op de opgegeven index
        }
    }

    // Toon alle afbeeldingen
    public void EnableAllImages()
    {
        foreach (var image in images)
        {
            image.SetActive(true);  // Activeer alle afbeeldingen
        }
    }
}
