using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static Action OnLoadScene;
    public void LoadScene(int index)
    {
        StartCoroutine(Load(index));
    }

    private IEnumerator Load(int index)
    {
        yield return Fader.instance.FadeOut();
        //OnLoadScene?.Invoke();
        LocationNavigator.Controller.Disable();
        //AsyncOperation async = SceneManager.LoadSceneAsync(index);
        //async.allowSceneActivation = true;
        AsyncOperation async = SceneManager.LoadSceneAsync(index, LoadSceneMode.Additive);

        while (!async.isDone)
            yield return null;

        Scene newScene = SceneManager.GetSceneByBuildIndex(index);
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


}
