using UnityEngine;

public class AppointmentLock : MonoBehaviour
{
    // Definieert verschillende vergrendelingsopties voor afspraken
    public enum LockType { NoEdit, NoNameEdit, Editable }
    public LockType lockType;

    // Verwijzingen naar andere scripts
    private AppointmentData appointmentData;
    private ValueManager valueManager;
    private AppointmentLockController lockController;

    private void Start()
    {
        // Haalt de benodigde componenten op
        appointmentData = GetComponent<AppointmentData>();
        valueManager = FindFirstObjectByType<ValueManager>();
        lockController = FindFirstObjectByType<AppointmentLockController>();
    }

    public void ApplyLockSettings()
    {
        // Zorgt ervoor dat de vereiste componenten beschikbaar zijn
        if (appointmentData == null || valueManager == null || lockController == null)
            return;

        // Controleert of deze afspraak is geselecteerd voordat de vergrendeling wordt toegepast
        if (valueManager.selectedPrefab == appointmentData.guidId)
        {
            switch (lockType)
            {
                case LockType.NoEdit:
                    // Volledige vergrendeling, niets kan worden bewerkt
                    lockController.SetInteractable(false, false, false);
                    break;
                case LockType.NoNameEdit:
                    // Alleen de datum kan worden aangepast, de naam blijft vast
                    lockController.SetInteractable(false, true, false);
                    break;
                case LockType.Editable:
                    // Beide velden zijn bewerkbaar
                    lockController.SetInteractable(false, true, true);
                    break;
            }
        }
    }
}
