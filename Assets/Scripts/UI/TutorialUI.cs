using UnityEngine;

public class TutorialUI : MonoBehaviour
{
    public GameObject tutorialUI;

    public void DisableUI()
    { 
        tutorialUI.SetActive(false);
    }
}
