using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestScrupt : MonoBehaviour
{
    public void OnClick()
    {
        SceneManager.LoadScene(1, LoadSceneMode.Additive);
    }

    public void Unload()
    {
        SceneManager.UnloadSceneAsync(1);
    }
}
