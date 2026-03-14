using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestButton : MonoBehaviour
{
    public GameObject panel;
    //private string buttonText = "ButtonTest";

    public void SetActive()
    {
        panel.SetActive(false);
    }

    public void Print()
    {
        print("text");
    }
    

}
