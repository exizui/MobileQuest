using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Audience : BaseLocations
{
    public override void Entry()
    {
        base.Entry();
    }

    public override void Exit()
    {
        //Controller.SetPrevLocation(id);
        base.Exit();
        //base.Exit();
    }
}
