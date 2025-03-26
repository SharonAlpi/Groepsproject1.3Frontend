using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AppointmentLockController : MonoBehaviour
{
    // UI-elementen voor invoer en bewerken
    public TMP_InputField nameInput;
    public TMP_InputField dateInput;
    public Button editButton;


    public void SetInteractable(bool nameEditable, bool dateEditable)
    {
        // Schakelt bewerking van het naamveld in of uit
        if (nameInput != null)
            nameInput.interactable = nameEditable;

        // Schakelt bewerking van het datumveld in of uit
        if (dateInput != null)
            dateInput.interactable = dateEditable;

        // Schakelt de bewerkingsknop in of uit, afhankelijk van of er iets bewerkbaar is
        if (editButton != null)
            editButton.interactable = dateEditable;
    }
}