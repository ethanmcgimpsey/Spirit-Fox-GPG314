using UnityEngine;

public class ChangeScene : MonoBehaviour
{
    public void LoadNewScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("ForestScene2");
    }
}
