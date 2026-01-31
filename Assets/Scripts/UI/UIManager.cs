using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public void LoadNextScene(string sceneName)
    { 
        SceneManager.LoadScene(sceneName);
    }

    public void QuitGame()
    { 
        Application.Quit();
    }
}
