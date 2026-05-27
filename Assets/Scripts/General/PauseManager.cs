using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class PauseManager : MonoBehaviour 
{
    public static PauseManager instance;
    public GameObject panelforOtherButt;
    public GameObject blackPanel;
    public InventoryUI inventoryUI;
    private SceneLoader loader;

    public Button restartButton;
    public GameObject banRestart;

    private bool isPause = false;
    private bool timeStop = false;

    //private bool wasPaused = false;

    private bool canRestart = true;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        panelforOtherButt.SetActive(false);
        loader = GetComponent<SceneLoader>();
    }

    public void SetCanRestart(bool can)
    {
        canRestart = can;

        if (!canRestart)
        {
            restartButton.interactable = false;
            banRestart.SetActive(true);
        }
        else
        {
            restartButton.interactable = true;
            banRestart.SetActive(false);
        }
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
        StartCoroutine(RestartRoutine(1)); ///////DEBUG!!!!!!!!!!
    }

    public void Quit()
    {
        Application.Quit();
    }

    private IEnumerator RestartRoutine(int indexScene)
    {
        yield return Fader.instance.FadeOut();
        SceneManager.LoadScene(indexScene);
        yield return null;
        yield return Fader.instance.FadeIn();
    }


    //private void OnApplicationQuit()
    //{
    //    SaveSystem.instance.Save();
    //}
    private void OnApplicationPause(bool pauseMode)
    {
        if (pauseMode)
        {
            SaveSystem.instance.Save();
            PauseMode();
        }
    }

    private void OnApplicationFocus(bool focus)
    {
        if (focus) //&& wasPaused
        {
            isPause = true;
            //wasPaused = true;
            //inventoryUI.OpenInventory();
            SaveSystem.instance.Save();
            SetPause();
        }
    }

}
