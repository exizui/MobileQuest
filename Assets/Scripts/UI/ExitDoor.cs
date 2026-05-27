using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
public class ExitDoor : MonoBehaviour
{
    public void Exit()
    {
        LocationNavigator.Controller.ExitRoom();
    }
}
