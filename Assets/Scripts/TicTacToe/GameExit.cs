using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameExit : MonoBehaviour
{
    public SceneLoader sceneLoader;
    public void OnClick()
    {
        sceneLoader.Unload(gameObject);
    }
}
