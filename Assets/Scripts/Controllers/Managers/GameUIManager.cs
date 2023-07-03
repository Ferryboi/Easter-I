using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameUIManager : Singleton<GameUIManager>
{
    [SerializeField] private TextMeshProUGUI timerDisplay;
    private Coroutine timer;
    [Space]
    [SerializeField] private TextMeshProUGUI primaryText;
    [SerializeField] private TextMeshProUGUI subText;
    private Coroutine textDisplay;
    private int currentTextPriority = int.MinValue;

    [Space]
    [SerializeField] private SwitchScreen screens;
    private bool paused;

    public bool CanPause = true;

    private void Start()
    {
        primaryText.text = "";
        subText.text = "";

        timerDisplay.text = "";

        paused = false;
    }

    public void StartTimer(float timeInSeconds, float finalCountdownTime = 0)
    {
        if(timer != null) StopCoroutine(timer);
        timer = StartCoroutine(TimerLoop(timeInSeconds, finalCountdownTime));
    }

    public void StopTimer()
    {
        if (timer != null)
        {
            StopCoroutine(timer);
            timer = null;
        }
    }

    private IEnumerator TimerLoop(float totalTime, float finalCountdownTime = 0)
    {
        for(float i = totalTime; i > 0; i -= 1f)
        {
            float minutes = Mathf.Floor(i / 60);
            float seconds = i % 60;

            if(i <= finalCountdownTime)
            {
                timerDisplay.text = "";
                DisplayText(1f, 0, seconds.ToString("0"));
            }
            else
            {
                timerDisplay.text = $"{minutes.ToString("00")}:{seconds.ToString("00")}";
            }

            yield return new WaitForSeconds(1f);
        }

        timerDisplay.text = "";
        timer = null;
    }

    public void DisplayText(float duration, int priority, string text1, string text2 = "")
    {
        if(true)//priority >= currentTextPriority)
        {
            if (textDisplay != null) StopCoroutine(textDisplay);

            currentTextPriority = priority;
            textDisplay = StartCoroutine(DisplayTextLoop(duration, text1, text2));
        }
    }

    private IEnumerator DisplayTextLoop(float duration, string text1, string text2)
    {
        primaryText.text = text1;
        subText.text = text2;

        yield return new WaitForSeconds(duration);

        primaryText.text = "";
        subText.text = "";

        currentTextPriority = int.MinValue;
        textDisplay = null;
    }

    public void DeleteText()
    {
        if(textDisplay != null)
        {
            StopCoroutine(textDisplay);
            textDisplay = null;

            primaryText.text = "";
            subText.text = "";
        }
    }

    public void TogglePause()
    {
        if (!CanPause) return;
        
        paused = !paused;
        Time.timeScale = paused == true ? 0 : 1;

        if (paused) screens.NavigateToScreen(ScreenRef.Pause);
        else screens.NavigateToScreen(ScreenRef.None);
    }
}
