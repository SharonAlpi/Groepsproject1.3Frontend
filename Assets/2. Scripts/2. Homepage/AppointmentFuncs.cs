using UnityEngine;

public class AppointmentFuncs : MonoBehaviour
{
    // Verwijzing naar het paneelbeheer
    public PanelManager panel;

    public void GoToInfo()
    {
        // TODO: Implementeren van navigatie naar het informatiescherm
    }

    public void GoToEdit()
    {
        // Toon het bewerkingspaneel
        panel.ShowPanel(1);
        Debug.Log("Bewerkingspaneel geopend");
    }

    public void GoToCompletion()
    {
        // Toon het voltooiingspaneel
        panel.ShowPanel(2);
        Debug.Log("Voltooiingspaneel geopend");
    }
}
