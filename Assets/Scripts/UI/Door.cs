using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static LocationNavigator;
public class Door : MonoBehaviour
{
    public LocationID audience;
    public void OpenDoor()
    {
        if (!QuestManager.instance.CanEnter(audience))
        {
            Notification.instance.ShowMessage("СПОЧАТКУ ВИКОНАЙ ПОТОЧНИЙ КВЕСТ");
            return;
        }

        Controller.GoToLocation(audience);
    }


}
