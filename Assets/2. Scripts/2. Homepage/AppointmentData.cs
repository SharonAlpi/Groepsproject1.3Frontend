using UnityEngine;
using TMPro;

public class AppointmentData : MonoBehaviour
{
    // UI-elementen om de afspraakgegevens weer te geven
    public TMP_Text tmpDate;
    public TMP_Text tmpName;

    // Verwijzing naar prefab scripts
    public StickerController stickerController;

    //verwijzing naar manager scripts
    private ValueManager valueManager;
    private PanelManager panelManager;

    // Gegevens van de afspraak
    public int id;
    public string _name;
    public int _date;
    public int _sticker;

    private void Start()
    {
        // vind de scripts van de manager
        panelManager = FindFirstObjectByType<PanelManager>();
        valueManager = FindFirstObjectByType<ValueManager>();

        // Zoek de StickerController in de kinderen van dit GameObject
        stickerController = GetComponentInChildren<StickerController>();
        if (stickerController == null)
        {
            Debug.LogError("StickerController niet gevonden bij kinderen!");
        }
        UpdateUI();
    }

    public void SelectPrefab()
    {
        // Controleer of selectie is toegestaan en stel de geselecteerde afspraak in
        if (valueManager.canSelect == true)
        {
            valueManager.selectedPrefab = id;
            Debug.Log($"Huidige geselecteerde prefab is {valueManager.selectedPrefab}");

            // toont paneel
            if (panelManager != null)
            {
                panelManager.ShowPanel(0);  // Show panel 0 when the appointment is selected
            }
        }
    }

    public void SetData(string name, int date, int sticker)
    {
        Debug.Log($"SetData Aangeroepen: Naam={name}, Datum={date}, Sticker={sticker}");

        // Update alleen de data als deze afspraak is geselecteerd
        if (valueManager != null && valueManager.selectedPrefab == id)
        {
            _name = name;
            _date = date;
            _sticker = sticker;
        }

        UpdateUI();

        // Werk sticker bij
        if (stickerController != null)
        {
            stickerController.SetSticker();
        }

        // Debug-meldingen als er iets ontbreekt
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
        // Update de UI met de huidige data van de afspraak
        tmpDate.text = _date.ToString();
        tmpName.text = _name;
    }
}
