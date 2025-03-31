using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.Sprites;
using UnityEngine;

public class PlayVideo : MonoBehaviour
{
    private VideoPlayer player;
    public Sprite startSprite;
    public Sprite stopSprite;
    public Button button;
    void Start()
    {
        player = GetComponent<VideoPlayer>();
    }

    
    void Update()
    {
        
    }
    public void StartVideo()
    {
        player.Play();
    }
    public void PauseVideo() 
    {
        player.Pause();
    }
    public void ChangeStartStop()
    {
        if (player.isPlaying == false)
        {
            player.Play();
            button.image.sprite = stopSprite;
        }
        else {
            button.image.sprite = startSprite;
            player.Pause();
        }
    }
}
