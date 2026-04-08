using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Notification : MonoBehaviour
{
    private static Notification mInstance;
    public static Notification Instance => mInstance;
    [SerializeField]
    private TextMeshProUGUI Popuptext;
    private string text;

    private float waitbetchar = 0.005f;
    private float waitbefdelete = 1.7f;

    private void Awake()
    {
        if (mInstance == null) mInstance = this;
    }
    private void Start()
    {
        Popuptext.text = "";
    }
    public void ShowMessage(string text)
    {
        if (Popuptext.text == "")
        {
            this.text = text;
            StartCoroutine(TextCoroutine());
        }
    }

    public void ItemNotification(string txt, ItemData item)
    {
        if (Popuptext.text == "")
        {
            this.text = txt + item.name;
            StartCoroutine(TextCoroutine());
        }
    }
    IEnumerator TextCoroutine()
    {
        foreach (char abc in text)
        {
            Popuptext.text += abc;
            yield return new WaitForSeconds(waitbetchar);
        }
        yield return new WaitForSeconds(waitbefdelete);

        Popuptext.text = "";
    }
}
