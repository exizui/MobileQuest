using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static LocationNavigator;
public class Door : MonoBehaviour
{
    [SerializeField]private QuestAudience audience;

    public void OpenDoor() 
    {
        Controller.LoadAudience(audience);
        Controller.CheckRoomUI();
    }

    
}
