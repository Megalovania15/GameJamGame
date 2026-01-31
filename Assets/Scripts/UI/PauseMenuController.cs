using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenuController : MonoBehaviour
{
    private GameObject pauseMenu;
    [SerializeField]
    private CharacterDeathToggles characterDeathToggles;

    private bool isPaused = false;

    void Awake()
    {
        pauseMenu = GameObject.Find("PauseMenuContainer").
            transform.GetChild(0).gameObject;
        //characterDeathToggles = GetComponent<CharacterDeathToggles>();

        if (!isPaused)
        {
            pauseMenu.SetActive(false);
        }
    }

    public void TogglePausedMenu(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            isPaused = !isPaused;

            if (isPaused)
            {
                pauseMenu.SetActive(false);
                characterDeathToggles.DisableCharacterComponents();
            }
            else
            {
                pauseMenu.SetActive(true);
                characterDeathToggles.EnableCharacterComponents();
            }
        }
    }
}
