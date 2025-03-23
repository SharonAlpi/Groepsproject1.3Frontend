using UnityEngine;

public class AppointmentFuncs : MonoBehaviour
{
    public PanelManager panel;

    public void GoToInfo()
    {
        //TODO: Next Scene
    }

    public void GoToEdit()
    {
        panel.ShowPanel(1);
        Debug.Log("pressed panel 1");
    }

    public void GoToCompletion()
    {
        panel.ShowPanel(2);
        Debug.Log("pressed panel 2");
    }
}
