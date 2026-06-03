using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStart : MonoBehaviour
{
    private SceneLoader sceneLoader;

    private void Awake()
    {
        sceneLoader = GetComponent<SceneLoader>();
    }
    public void StartGame()
    {
        sceneLoader.LoadGame("Base");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
