using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PauseMenuController : MonoBehaviour
{
    public static UnityEvent<bool> OnPause = new();

    private GameObject pauseMenu;

    private InputAction pauseAction;

    private bool isPaused = false;

    void Awake()
    {
        pauseMenu = GameObject.Find("PauseMenuContainer").
            transform.GetChild(0).gameObject;
        pauseAction = InputSystem.actions.FindAction("TogglePause");
        //characterDeathToggles = GetComponent<CharacterDeathToggles>();

        if (!isPaused)
        {
            pauseMenu.SetActive(false);
        }

        pauseAction.performed += _ => { TogglePauseMenu(); };
    }

    public void TogglePauseMenu()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            Debug.Log("Game has been paused");
            PauseGame();
            //characterDeathToggles.DisableCharacterComponents();
        }
        else
        {
            Debug.Log("Game is unpaused");
            ResumeGame();
            
            //characterDeathToggles.EnableCharacterComponents();
        }

        OnPause.Invoke(isPaused);
    }

    /*public void SwitchActionMap(bool hasOpenUI, PlayerInput inputs)
    {
        if (!isPaused)
        {
            input.SwitchCurrentActionMap("BasicActions");
        }
        else
        {
            input.SwitchCurrentActionMap("UI");
        }
    }*/

    void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }
}
