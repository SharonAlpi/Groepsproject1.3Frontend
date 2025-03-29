using TMPro;
using UnityEngine;

public class MoreInfoManager : MonoBehaviour
{
    public void ToggleGameObject(GameObject obj)
    {
        obj.SetActive(!obj.activeSelf);
    }
}
