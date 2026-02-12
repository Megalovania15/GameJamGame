using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    private InputAction exitAction;

    void Awake()
    {
        exitAction = InputSystem.actions.FindAction("ExitGame");

        exitAction.performed += _ => { LoadNextScene("TitleScene"); };
    }

    public void LoadNextScene(string sceneName)
    { 
        SceneManager.LoadScene(sceneName);
    }

    public void QuitGame()
    { 
        Application.Quit();
    }

    
}
