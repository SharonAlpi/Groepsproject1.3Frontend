using UnityEngine;
using System.Linq;

public class AppointmentSorter : MonoBehaviour
{
    private void Start()
    {
        SortAppointments();
    }

    public void SortAppointments()
    {
        var children = GetComponentsInChildren<AppointmentData>()
            .OrderBy(appointment => appointment._date)
            .ToList();

        for (int i = 0; i < children.Count; i++)
        {
            children[i].transform.SetSiblingIndex(i);
        }
    }
}
