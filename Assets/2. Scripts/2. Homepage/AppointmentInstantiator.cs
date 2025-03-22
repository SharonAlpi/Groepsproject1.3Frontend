using UnityEngine;

public class AppointmentController : MonoBehaviour
{
    public GameObject panel;
    public GameObject[] appointmenPrefabs;

    private void Start()
    {
        for (int i = 0; i < appointmenPrefabs.Length; i++)
        {
            AppointmentData appointmentData = Instantiate(appointmenPrefabs[i], panel.transform).GetComponent<AppointmentData>();
        }
    }
}
