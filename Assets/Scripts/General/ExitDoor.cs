using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static LocationNavigator;
public class ExitDoor : MonoBehaviour
{
    public void Exit()
    {
        Controller.ExitRoom();
        Controller.CheckRoomUI();
    }
}
