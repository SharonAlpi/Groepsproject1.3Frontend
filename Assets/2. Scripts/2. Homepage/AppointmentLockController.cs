using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AppointmentLockController : MonoBehaviour
{
    // UI-elementen voor invoer en bewerken
    public TMP_InputField nameInput;
    public TMP_InputField dateInput;
    public Button editButton;

    public Button deleteButton;

    public void SetInteractable(bool nameEditable, bool dateEditable, bool deleteEditable)

    public Button completionButton;

    public void SetInteractable(bool nameEditable, bool dateEditable)
    {
        if (nameInput != null)
            deleteButton.interactable = deleteEditable;
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

    public void SetCompletion(bool buttonEditable)
    {
        // Schakelt de knop om de afspraak af te maken in of uit, afhankelijk van of er iets bewerkbaar is
        if (completionButton != null)
            completionButton.interactable = buttonEditable;
    }
}