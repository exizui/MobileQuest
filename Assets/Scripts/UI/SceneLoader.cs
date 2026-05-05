using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static Action OnLoadScene;
    public void LoadScene(string name)
    {
        StartCoroutine(Load(name));
    }

    private IEnumerator Load(string name)
    {
        yield return Fader.instance.FadeOut();
        //OnLoadScene?.Invoke();


        //AsyncOperation async = SceneManager.LoadSceneAsync(index);
        //async.allowSceneActivation = true;

        AsyncOperation async = SceneManager.LoadSceneAsync(name, LoadSceneMode.Additive);
        LocationNavigator.Controller.Disable();
    
        while (!async.isDone)
            yield return null;

        //Scene newScene = SceneManager.GetSceneByBuildIndex(index);
        Scene newScene = SceneManager.GetSceneByName(name);
        SceneManager.SetActiveScene(newScene);


    }
    public void Unload(GameObject caller)
    {
        StartCoroutine(UnloadRoutine(caller)); 
    }

    private IEnumerator UnloadRoutine(GameObject caller)
    {
        yield return Fader.instance.FadeOut();

        Scene scene = caller.scene;

        LocationNavigator.Controller.LoadPrevLocation();

        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            Scene s = SceneManager.GetSceneAt(i);

            if (s != scene && s.isLoaded)
            {
                SceneManager.SetActiveScene(s);
                break;
            }
        }
        AsyncOperation op = SceneManager.UnloadSceneAsync(scene);

        //while (op != null && !op.isDone)
        //    yield return null;


        yield return Fader.instance.FadeIn();

    }

    public void LoadGame(string name)
    {
        StartCoroutine(Game(name));
    }

    private IEnumerator Game(string name)
    {
        yield return Fader.instance.FadeOut();
        SceneManager.LoadScene(name);
        yield return Fader.instance.FadeIn();
    }
}
