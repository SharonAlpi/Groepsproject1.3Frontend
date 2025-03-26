using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PageManager : MonoBehaviour
{
    public SceneAsset[] Scene;

    public void Scene1()
    {
        LoadPage(Scene[0].name);
    }
    public void Scene2()
    {
        LoadPage(Scene[1].name);
    }
    public void Scene3()
    {
        LoadPage(Scene[2].name);
    }

    public void LoadPage(string scene){
        SceneManager.LoadScene(scene,LoadSceneMode.Single);
    }
}
