using UnityEngine;
using TMPro;

public class AppointmentData : MonoBehaviour
{
    public TMP_Text tmpDate;
    public TMP_Text tmpName;
    public StickerController stickerController;
    public ValueManager valueManager;
    public int id;
    public string _name;
    public int _date;
    public int _sticker;

    private void Start()
    {
        stickerController = GetComponentInChildren<StickerController>();
        if (stickerController == null)
        {
            Debug.LogError("StickerController not found in children!");
        }
        UpdateUI();
    }

    public void SelectPrefab()
    {
        if(valueManager.canSelect == true)
        {
            valueManager.selectedPrefab = id;
            Debug.Log($"current selected prefab is {valueManager.selectedPrefab}");
        }
    }

    public void SetData(string name, int date, int sticker)
    {
        Debug.Log($"SetData Called: Name={name}, Date={date}, Sticker={sticker}");

        // Check if the current appointment is the one selected for editing
        if (valueManager != null && valueManager.selectedPrefab == id)
        {
            _name = name;
            _date = date;
            _sticker = sticker;
        }

        // Update the UI with the new data
        UpdateUI();

        if (stickerController != null)
        {
            stickerController.SetSticker(); // Update sticker (if required)
        }

        // Debugging for missing references
        if (valueManager == null)
        {
            Debug.LogError("valueManager is NULL in SetData()");
        }

        if (stickerController == null)
        {
            Debug.LogError("stickerController is NULL in SetData()");
        }
    }



    private void UpdateUI()
    {
        tmpDate.text = _date.ToString();
        tmpName.text = _name;
    }
}
