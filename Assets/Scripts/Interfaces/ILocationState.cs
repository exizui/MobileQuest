using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public interface ILocationState 
{
    void Enter(LocationNavigator navigator);
}

public class CorridorState : ILocationState
{
    public void Enter(LocationNavigator nav)
    {
        var loc = nav.CurrentLocation;

        bool hasNext = loc.next != LocationID.None;
        bool hasPrev = loc.prev != LocationID.None;

        nav.SetUI(
            next: hasNext,
            prev: hasPrev,
            entry: false
        );
        nav.OffExitButton();
        nav.SetSwipe(true);
    }
}


public class AudienceState : ILocationState
{
    public void Enter(LocationNavigator nav)
    {
        nav.SetUI(
            next: false,
            prev: false,  
            entry: false
        );
        nav.SetSwipe(false);
    }
}

public class StreetState : ILocationState
{
    public void Enter(LocationNavigator nav)
    {
        var loc = nav.GetCurrentLocation();

        bool hasPrev = GameState.instance.HasFlag("buyCoffee");

        if (hasPrev) {loc.prev = LocationID.Level_Shop;}

        nav.SetUI(
            next: false,
            prev: hasPrev,
            entry: true
        );
        nav.SetSwipe(false);
    }
}


