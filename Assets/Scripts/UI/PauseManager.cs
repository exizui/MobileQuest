using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class PauseManager : MonoBehaviour 
{
    public GameObject panelforOtherButt;
    public GameObject blackPanel;

    private SceneLoader loader;

    private bool isPause = false;
    private bool timeStop = false;

    private bool wasPaused = false;
    private void Start()
    {
        panelforOtherButt.SetActive(false);
        loader = GetComponent<SceneLoader>();
    }
    private bool TimeStop(bool timestop)
    {
        timeStop = timestop;

        if (timestop)
        { 
            Time.timeScale = 0.0f;
        }
        else { 
            Time.timeScale = 1.0f; 
        }
        return timeStop;
    }
    public void SetPause()
    {
        isPause = !isPause;

        TimeStop(isPause);
        if (isPause) 
            PauseMode();
        else
            PauseStop();
    }

    public void PauseMode()
    {
        panelforOtherButt.SetActive(true);
        blackPanel.SetActive(true);
    }

    public void PauseStop()
    {
        panelforOtherButt.SetActive(false);
        blackPanel.SetActive(false);
    }

    public void RestartGame()
    {
        //StopAllCoroutines();
        SaveSystem.instance.DeleteSaves();
        //SceneManager.LoadScene(0);
        TimeStop(false);
        StartCoroutine(RestartRoutine(0)); ///////DEBUG!!!!!!!!!!

    }

    private IEnumerator RestartRoutine(int indexScene)
    {
        yield return Fader.instance.FadeOut();
        SceneManager.LoadScene(indexScene);
        yield return null;
        yield return Fader.instance.FadeIn();
    }
    public void QuitMenu()
    {
        TimeStop(false);
        print(false);
        StartCoroutine(RestartRoutine(2));
    }

    private void OnApplicationPause(bool pauseMode)
    {
        if (pauseMode)
        {
            PauseMode();
        }
    }

    private void OnApplicationFocus(bool focus)
    {
        if (focus && wasPaused)
        {
            isPause = true;
            wasPaused = true;
            TimeStop(true);
            PauseMode();
        }
    }
}
