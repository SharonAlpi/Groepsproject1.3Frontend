using UnityEngine;

public class StickerController : MonoBehaviour
{
    public AppointmentData appointmentData;
    public GameObject[] stickerPrefabs;  

    private GameObject currentSticker; 

    private void Start()
    {
        SetSticker();
    }

    public void SetSticker()
    {
        if (appointmentData != null)
        {
            int stickerIndex = appointmentData._sticker;
            Debug.Log("Sticker index: " + stickerIndex); // Debug log to check the value of the sticker index

            if (currentSticker != null)
            {
                Destroy(currentSticker);
            }

            if (stickerIndex >= 0 && stickerIndex < stickerPrefabs.Length)
            {
                currentSticker = Instantiate(stickerPrefabs[stickerIndex], transform);
                Debug.Log("Sticker is set.");
            }
            else
            {
                currentSticker = Instantiate(stickerPrefabs[0], transform);
                Debug.LogError("Sticker index is out of range.");
            }
        }
        else
        {
            currentSticker = Instantiate(stickerPrefabs[0], transform);
            Debug.LogError("AppointmentData is not assigned.");
        }
    }

}
