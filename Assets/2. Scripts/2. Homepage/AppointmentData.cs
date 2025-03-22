using UnityEngine;
using TMPro;

public class AppointmentData : MonoBehaviour
{
    public TMP_Text tmpDate;
    public TMP_Text tmpName;
    public int id;
    public string _name;
    public int _date; //TODO: turn into actual dates
    public int _sticker; //TODO: connect with stickers class

    private void Start()
    {
        tmpDate.text = _date.ToString();
        tmpName.text = _name;
    }

}
