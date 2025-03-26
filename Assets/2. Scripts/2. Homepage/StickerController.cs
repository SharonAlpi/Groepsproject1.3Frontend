using UnityEngine;

public class StickerController : MonoBehaviour
{
    // Verwijzing naar de bijbehorende afspraak
    public AppointmentData appointmentData;

    // Array met beschikbare sticker-prefabs
    public GameObject[] stickerPrefabs;

    // Huidige actieve sticker
    private GameObject currentSticker;

    private void Start()
    {
        // Stel de sticker in bij het starten van het script
        SetSticker();
    }

    public void SetSticker()
    {
        if (appointmentData != null)
        {
            int stickerIndex = appointmentData._sticker;
            Debug.Log("Sticker index: " + stickerIndex);

            // Verwijder de huidige sticker als er een actief is
            if (currentSticker != null)
            {
                Destroy(currentSticker);
            }

            // Controleer of de index binnen de geldige grenzen ligt en instantieer de juiste sticker
            if (stickerIndex >= 0 && stickerIndex < stickerPrefabs.Length)
            {
                currentSticker = Instantiate(stickerPrefabs[stickerIndex], transform);
            }
            else
            {
                // Gebruik een standaard sticker als de index ongeldig is
                currentSticker = Instantiate(stickerPrefabs[0], transform);
            }
        }
        else
        {
            // Gebruik een standaard sticker als er geen appointmentData is
            currentSticker = Instantiate(stickerPrefabs[0], transform);
        }
    }
}
