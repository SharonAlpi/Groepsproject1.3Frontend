using UnityEngine;

public class PanelManager : MonoBehaviour
{
    public GameObject[] panels;
    [SerializeField] private int currentPanel;

    void Start()
    {
        HideAllPanels();
        panels[currentPanel].SetActive(true);
    }

    public void ShowPanel(int index)
    {
        HideAllPanels();
        if (index >= 0 && index < panels.Length)
        {
            panels[index].SetActive(true);
        }
    }

    public void EnablePanel(int index)
    {
        if (index >= 0 && index < panels.Length)
        {
            panels[index].SetActive(true);
        }
    }

    public void HidePanel(int index)
    {
        if (index >= 0 && index < panels.Length)
        {
            panels[index].SetActive(false);
        }
    }

    // Hide all panels
    public void HideAllPanels()
    {
        foreach (var panel in panels)
        {
            panel.SetActive(false);
        }
    }
}
