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
    private string _text;

    [SerializeField]
    private TextMeshProUGUI Delaytext;
    private string _delaytext;

    [SerializeField] private GameObject lining_Notification;
    [SerializeField] private GameObject lining_NotificationDelay;

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
        Delaytext.text = ""; 
    }

    #region Сповіщення про отримання предмету
    public void ItemNotification(string txt, ItemData item)
    {
        lining_Notification.SetActive(true);

        _text = txt + item.name;

        if (currentCoroutine != null)
            StopCoroutine(currentCoroutine);

        currentCoroutine = StartCoroutine(TextCoroutine());
    }
    #endregion

    #region Звичайний показ сповіщення 
    public void ShowMessage(string text)
    {
        if (isShowing) return;

        lining_Notification.SetActive(true);

        _text = text;
        isShowing = true;

        if (currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
        }
        currentCoroutine = StartCoroutine(TextCoroutine());
    }
    IEnumerator TextCoroutine()
    {
        Popuptext.text += "";

        foreach (char abc in _text)
        {
            Popuptext.text += abc;
            yield return new WaitForSecondsRealtime(waitbetchar);
        }
        yield return new WaitForSecondsRealtime(waitbefdelete);

        Popuptext.text = "";
        currentCoroutine = null;
        isShowing = false;

        lining_Notification.SetActive(false);
    }
    #endregion

    #region Показ з затримкою
    public void ShowMessage(string text, float delay)
    {
        if (isShowing) return;
        //Delaytext.gameObject.SetActive(true);
        //Delaytext.transform.parent.gameObject.SetActive(true);

        lining_NotificationDelay.SetActive(true);



        _delaytext = text;
        isShowing = true;

        if (currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
        }

        currentCoroutine = StartCoroutine(TextCoroutine(delay));
    }

    IEnumerator TextCoroutine(float delay)
    {
        Delaytext.text += "";

        foreach (char abc in _delaytext)
        {
            Delaytext.text += abc;
            yield return new WaitForSecondsRealtime(waitbetchar);
        }

        yield return new WaitForSecondsRealtime(delay);

        Delaytext.text = "";
        currentCoroutine = null;
        isShowing = false;

        //Delaytext.transform.parent.gameObject.SetActive(false);
        //Delaytext.gameObject.SetActive(false);
        lining_NotificationDelay.SetActive(false); 
    }
    #endregion


}
