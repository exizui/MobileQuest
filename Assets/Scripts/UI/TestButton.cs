using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TestButton : MonoBehaviour
{
    public GameObject panel;
    public UnityEvent OnClicked;
    //private string buttonText = "ButtonTest";
    public void SetActive()
    {
        panel.SetActive(false);
        OnClicked?.Invoke();
    }

    public void Print()
    {
        print("text");
    }
}
