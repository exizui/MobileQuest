using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using static LocationController;
using static LocationNavigator;

public class Stairs : MonoBehaviour
{

    public void Go_Level2()
    {
        //Instance.LoadLevel(LocationID.Level_2A);
        //print(LocationID.Level_2A);
        //Controller.LoadLocation(LocationID.Level_2A);]
        Controller.LoadLocation(LocationID.Level_2A);
    }

    public void Go_Level3()
    {
        //Instance.LoadLevel(LocationID.Level_3A);
        //print(LocationID.Level_3A);
        //Controller.LoadLocation(LocationID.Level_3A);
        Controller.LoadLocation(LocationID.Level_3A);
    }

    public void Go_Level1()
    {
        //Instance.LoadLevel(LocationID.Level_1);
        //print(LocationID.Level_1);
        //Controller.LoadLocation(LocationID.Level_1);
        Controller.LoadLocation(LocationID.Level_1);
    }
}
