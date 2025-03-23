using UnityEngine;
using System.Linq;
using UnityEngine.UI; // Needed for layout refresh

public class AppointmentSorter : MonoBehaviour
{
    public Transform parentTransform;
    public GridLayoutGroup gridLayoutGroup;

    public void SortAppointments()
    {
        var sortedAppointments = parentTransform.Cast<Transform>()
            .Select(child => child.GetComponent<AppointmentData>())
            .Where(appointment => appointment != null)
            .OrderBy(appointment => appointment._date)
            .ToList();

        for (int i = 0; i < sortedAppointments.Count; i++)
        {
            sortedAppointments[i].transform.SetSiblingIndex(i);
        }

        StartCoroutine(RefreshLayout());
    }

    private System.Collections.IEnumerator RefreshLayout()
    {
        yield return null; // Wait 1 frame
        gridLayoutGroup.enabled = false;
        gridLayoutGroup.enabled = true;
    }
}

