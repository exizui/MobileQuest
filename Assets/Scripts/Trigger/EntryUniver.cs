using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EntryUniver : MonoBehaviour
{
    public LocationNavigator locNav;
    [SerializeField]private GameObject obj;

    private void Awake()
    {
        obj = gameObject;
    }
    //public void Init()
    //{
    //    obj.SetActive(true);
    //}
    public void OnClick()
    {
        locNav.LoadLocation(LocationID.Level_1);
        obj.SetActive(false);
    }
}
