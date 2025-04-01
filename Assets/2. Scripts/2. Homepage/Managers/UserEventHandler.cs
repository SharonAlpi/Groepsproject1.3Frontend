using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UserEventHandler : MonoBehaviour
{
    //verwijzing naar manager scripts
    public ValueManager valueManager;
    public PanelManager panelManager;
    public AppointmentController controller;
    public string[] Scenes;
    //Appointments
    public void CreateAppointment()
    {
        controller.CreateAppointment();
    }

    public void EditAppointment()
    {
        controller.EditAppointment();
    }

    public void EditSticker(int newSticker)
    {
        controller.EditSticker(newSticker);
    }

    public void DeleteAppointment()
    {
        controller.DeleteAppointment();
    }

    //UI -> Panels
    public void GoToScene()
    {
        SceneManager.LoadScene(Scenes[valueManager.sceneDirective]);
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
