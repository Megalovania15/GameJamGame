using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class GameTimer : MonoBehaviour
{
    public TMP_Text timerText;

    [SerializeField] private float totalGameTime = 300f;

    public static UnityEvent<bool> OnTimeUp = new();

    private TimeSpan gameSpan;

    //private float elapsedGameTime;
    private bool timerActivated = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameSpan = TimeSpan.FromSeconds(totalGameTime);
        timerText.text = gameSpan.ToString(@"mm\:ss");
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (timerActivated)
        {
            totalGameTime -= Time.deltaTime;
            gameSpan = TimeSpan.FromSeconds(totalGameTime);
            timerText.text = gameSpan.ToString(@"mm\:ss");

            if (totalGameTime <= 0)
            {
                timerActivated = false;
                OnTimeUp.Invoke(totalGameTime <= 0);
                Debug.Log("Time up event triggered.");
            }
        }
    }

    public void ActivateTimer()
    {
        if (!timerActivated)
        {
            timerActivated = true;
        }
    }
}
