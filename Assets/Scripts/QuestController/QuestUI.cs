using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class QuestUI : MonoBehaviour
{
    public static QuestUI instance { get; private set; }
    [SerializeField] private TextMeshProUGUI questText;
    [SerializeField] private TextMeshProUGUI descriptionText;

    [SerializeField] private GameObject headerObject;
    [SerializeField] private GameObject descriptObject;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        headerObject.SetActive(false);
        descriptObject.SetActive(false);
    }
    public void ActiveUI()
    {
        headerObject.SetActive(true);
        descriptObject.SetActive(true);
    }

    public void DisActiveUI()
    {
        headerObject.SetActive(false);
        descriptObject.SetActive(false);
    }


    public void ShowHeader(string text)
    {
        questText.text = text;

    }
    public void ShowDescription(string progress = "")
    {
        descriptionText.text = progress;
    }
}
