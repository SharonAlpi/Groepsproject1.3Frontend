using TMPro;
using UnityEngine;

public class UserEventHandler : MonoBehaviour
{
    //verwijzing naar manager scripts
    public ValueManager valueManager;
    public PanelManager panelManager;
    public AppointmentEditor appointmentEditor;

    //Appointments
    public void CreateAppointment()
    {
        appointmentEditor.CreateAppointment();
    }

    public void EditAppointment()
    {
        appointmentEditor.EditAppointment();
    }

    public void EditSticker(int newSticker)
    {
        appointmentEditor.EditSticker(newSticker);
    }

    public void DeleteAppointment()
    {
        appointmentEditor.DeleteAppointment();
    }

    //UI -> Panels
    public void GoToScene(int index)
    {
        //TODO: go to scene
    }

    public void GoToPanel(int index)
    {
        panelManager.ShowPanel(index);
    }

    public void CloseAllPanels()
    {
        panelManager.HideAllPanels();
    }

    //UI -> Text
}
