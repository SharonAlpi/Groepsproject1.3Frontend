using UnityEngine;
using UnityEngine.SceneManagement;
public class UniversalSceneLoader : MonoBehaviour
{
    [SerializeField]
    string _sceneName;

    public void Click()
    {
        SceneManager.LoadScene( _sceneName);
    }
}
