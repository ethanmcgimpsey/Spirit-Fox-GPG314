using UnityEngine;

public class ChangeScene : MonoBehaviour
{
    public int loadScene;

    public void LoadNewScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(loadScene);
    }
}
