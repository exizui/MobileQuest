using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{

    public GameObject pauseButt;
    public GameObject panelforOtherButt;
    public GameObject blackPanel;

    private bool isPause = false;
    private bool timeStop = false;
    private void Start()
    {
        panelforOtherButt.SetActive(false);
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
}
