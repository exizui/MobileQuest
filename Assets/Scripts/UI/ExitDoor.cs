using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static LocationNavigator;
public class ExitDoor : MonoBehaviour
{
    public static ExitDoor instance;
 
    public void Exit()
    {
        Controller.ExitRoom();
    }
}
