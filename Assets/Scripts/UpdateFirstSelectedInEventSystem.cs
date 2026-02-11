using UnityEngine;
using UnityEngine.EventSystems;

public class UpdateFirstSelectedInEventSystem : MonoBehaviour
{
    public GameObject firstSelectedOnPauseMenu, firstSelectedOnEndMenu;

    private EventSystem eventSystem;

    void OnEnable()
    {
        PauseMenuController.OnPause.AddListener(SelectFirstButtonOnPauseMenu);
        GameTimer.OnTimeUp.AddListener(SelectFirstButtonOnEndMenu);
        //Debug.Log("Listeners added to OnPause and TimeUp");
    }

    void OnDisable()
    {
        PauseMenuController.OnPause.RemoveListener(SelectFirstButtonOnPauseMenu);
        GameTimer.OnTimeUp.RemoveListener(SelectFirstButtonOnEndMenu);
        //Debug.Log("Listeners removed from OnPause and TimeUp");
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        eventSystem = GetComponent<EventSystem>();
    }

    public void SelectFirstButtonOnPauseMenu(bool isPaused)
    {
        if (isPaused)
        {
            Debug.Log("Button has been selected");
            eventSystem.firstSelectedGameObject = firstSelectedOnPauseMenu;
        }
        else
        {
            return;
        }
    }

    public void SelectFirstButtonOnEndMenu(bool isGameOver)
    {

        eventSystem.firstSelectedGameObject = firstSelectedOnEndMenu;

    }
}
