using UnityEngine;
using System.Linq;
using UnityEngine.UI; // Nodig voor het vernieuwen van de layout

public class AppointmentSorter : MonoBehaviour
{
    // Verwijzing naar de ouder van de afspraken
    public Transform parentTransform;

    // GridLayoutGroup voor visuele ordening
    public GridLayoutGroup gridLayoutGroup;

    public void SortAppointments()
    {
        // Sorteer de afspraken op datum en converteer naar een lijst
        var sortedAppointments = parentTransform.Cast<Transform>()
            .Select(child => child.GetComponent<AppointmentData>())
            .Where(appointment => appointment != null)
            .OrderBy(appointment => appointment._date)
            .ToList();

        // Pas de volgorde van de afspraken in de UI aan
        for (int i = 0; i < sortedAppointments.Count; i++)
        {
            sortedAppointments[i].transform.SetSiblingIndex(i);
        }

        // Vernieuw de layout zodat de wijzigingen zichtbaar zijn
        StartCoroutine(RefreshLayout());
    }

    private System.Collections.IEnumerator RefreshLayout()
    {
        yield return null; // Wacht één frame voordat de layout wordt vernieuwd
        gridLayoutGroup.enabled = false;
        gridLayoutGroup.enabled = true;
    }
}

