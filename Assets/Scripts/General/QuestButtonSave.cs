using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestButtonSave : MonoBehaviour, ISaveable
{
    [SerializeField] private GameObject questButton;

    public object CaptureState()
    {
        return questButton.activeSelf;
    }

    public void RestoreState(object state)
    {
        bool isActive = (bool)state;
        questButton.SetActive(isActive);
    }
}
