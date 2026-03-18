using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static LocationNavigator;
public abstract class Locations : BaseLocations
{
    public LocationID id;
    //public bool hideNavigationUI; 
    public override void Entry()
    {
        base.Entry();
    }

    public override void Exit()
    {
        //Controller.SetPrevLocation(id);
        base.Exit();
    }
}
