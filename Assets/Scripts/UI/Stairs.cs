using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stairs : MonoBehaviour
{
    public LocationID location;

    public void Go_Level()
    {
        LocationNavigator.Controller.GoToLocation(location);

    }
}
