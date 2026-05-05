using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stairs : MonoBehaviour
{
    private LocationID location;

    public static Stairs instance;

    private void Awake()
    {
        instance = this;
    }

    public void Go_Level(string locationName)
    {
        if (System.Enum.TryParse(locationName, out LocationID loc))
        {
            LocationNavigator.Controller.GoToLocation(loc);
        }
        else
        {
            Debug.LogError("Error" +  locationName);
        }
    }
}

