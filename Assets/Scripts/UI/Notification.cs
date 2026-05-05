using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Notification : MonoBehaviour
{
    private static Notification mInstance;
    public static Notification instance => mInstance;
    [SerializeField]
    private TextMeshProUGUI Popuptext;
    private string text;

    private float waitbetchar = 0.005f;
    private float waitbefdelete = 1.7f;

    public bool isShowing;

    private Coroutine currentCoroutine;
    private void Awake()
    {
       if (mInstance != null && mInstance != this)
       {
          Destroy(gameObject);
          return;
       }
       mInstance = this;
    }
    private void Start()
    {
        Popuptext.text = "";
    }
    public void ShowMessage(string text)
    {
        if (isShowing) return;

        this.text = text;
        isShowing = true;
        Debug.Log("ShowMessage called");
        if (currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
        }

        currentCoroutine = StartCoroutine(TextCoroutine());
    }

    public void ItemNotification(string txt, ItemData item)
    {
        this.text = txt + item.name;

        if (currentCoroutine != null)
            StopCoroutine(currentCoroutine);

        currentCoroutine = StartCoroutine(TextCoroutine());
    }
    IEnumerator TextCoroutine()
    {
        Popuptext.text += "";

        foreach (char abc in text)
        {
            Popuptext.text += abc;
            yield return new WaitForSecondsRealtime(waitbetchar);
        }
        yield return new WaitForSecondsRealtime(waitbefdelete);

        Popuptext.text = "";
        currentCoroutine = null;
        isShowing = false;
    }
}
