using UnityEngine;
using TMPro;
using System;
using UnityEditor;

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
    private AppointmentLockController lockController;

    // Gegevens van de afspraak
    public Guid guidId;
    public string _name;
    public DateTime _date;
    public int _sticker;

    // Gevens van functies
    public bool isQueue;
    public int infoScene;

    private void Update()
    {
        if(!isQueue)
        {
            if (_date.Date <= DateTime.Now.Date.AddYears(1))
            {
                tmpDate.gameObject.SetActive(true);
            }
            else
            {
                tmpDate.gameObject.SetActive(false);
            }
        }
    }
    private void Start()
    {
        // vind de scripts van de manager
        panelManager = FindFirstObjectByType<PanelManager>();
        valueManager = FindFirstObjectByType<ValueManager>();
        lockController = FindFirstObjectByType<AppointmentLockController>();

        // Zoek de StickerController in de kinderen van dit GameObject
        stickerController = GetComponentInChildren<StickerController>();
        if (stickerController == null)
        {
            Debug.LogError("StickerController niet gevonden bij kinderen!");
        }
        UpdateUI();
        CheckIfCompleted();
    }

    private void CheckIfCompleted()
    {
        if (_date < DateTime.Now.Date && _sticker == 0)
        {
            lockController.SetCompletion(true);
        }
        else
        {
            lockController.SetCompletion(false);
        }
    }

    public void SelectPrefab()
    {
        // Controleer of selectie is toegestaan en stel de geselecteerde afspraak in
        if (valueManager.canSelect == true && !isQueue)
        {
            CheckIfCompleted();
            valueManager.selectedPrefab = guidId;

            // toont paneel
            if (panelManager != null)
            {
                panelManager.ShowPanel(0);  // Show panel 0 when the appointment is selected
            }

            valueManager.appointmentName = _name;
            valueManager.appointmentDate = _date;
            valueManager.sceneDirective = infoScene;
            Debug.Log($"Huidige geselecteerde appointment is {_name} op datum {_date} met de id {valueManager.selectedPrefab}");
        }
    }

    public void SetData(string name, DateTime date, int sticker)
    {
        Debug.Log($"SetData Aangeroepen: Naam={name}, Datum={date}, Sticker={sticker}");

        // Update alleen de data als deze afspraak is geselecteerd
        if (valueManager != null && valueManager.selectedPrefab == guidId)
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
        if (!isQueue)
        {
            tmpDate.text = _date.ToString("dd-MM-yyyy");
            tmpName.text = _name;
        }
    }
}
