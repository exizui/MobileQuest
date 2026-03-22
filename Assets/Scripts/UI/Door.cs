using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static LocationNavigator;
public class Door : MonoBehaviour
{
    //public LocationID targetlocation;
    //public AudienceID audience;
    [SerializeField]
    private Audience audience;

    public void OpenDoor() 
    {
        //Controller.LoadLocation(targetlocation);
        //Controller.LoadAudience(audience);
        Controller.LoadAudience(audience);
        Controller.CheckRoomUI();
       
    }

    
}
