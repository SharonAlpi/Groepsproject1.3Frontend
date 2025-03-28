using UnityEngine;

public class OptDirection : MonoBehaviour
{
    public TextManager explanation;
    public TextManager tips;
    public PanelManager video;

    private void Start()
    {
        setOpt(0);
    }

    public void setOpt(int opt)
    {
        explanation.ShowText(opt);
        tips.ShowText(opt);
        video.ShowPanel(opt);
    }

}
