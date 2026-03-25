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
        Controller.CheckRoomUI();
    }


    public void ShowDoor()
    {
        gameObject.SetActive(true);
    }

    public void HideDoor()
    {
        gameObject.SetActive(false);
    }
    public void ShowExitDoor(bool isShow)
    {
        if (isShow)
        {
            
        }
        else
        {
           
        }
        
    }  
}
