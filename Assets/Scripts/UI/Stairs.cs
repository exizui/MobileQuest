using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using static LocationController;
using static LocationNavigator;

public class Stairs : MonoBehaviour
{
    public LocationID location;
    private LocationNavigator navigator;

    private void Awake()
    {
        navigator = FindObjectOfType<LocationNavigator>();
    }
    public void Go_Level()
    {
        navigator.LoadLocation(location);

    }
}
