using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
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
        nav.exitButton.SetActive(false);
        nav.entryStreet.SetActive(false);
        nav.prevButton.SetActive(true);

        bool isNext = nav.IsNextBlocked();
        nav.nextButton.SetActive(!isNext);

        bool isPrev = nav.IsPrevBlocked();
        nav.prevButton.SetActive(!isPrev);

        nav.currentStateType = StateLocation.Corridor;
    }
}


public class AudienceState : ILocationState
{
    public void Enter(LocationNavigator nav)
    {
        nav.prevButton.SetActive(false);
        nav.nextButton.SetActive(false);
        //nav.entryStreet.SetActive(true);
        //nav.exitButton.SetActive(true);
        nav.currentStateType = StateLocation.Audience;
    }
}

public class StreetState : ILocationState
{
    public void Enter(LocationNavigator nav)
    {

        if (GameState.instance.HasFlag("buyCoffee"))
        {
            nav.prevButton.SetActive(true);
        }
        else
        {
            nav.prevButton.SetActive(false);
        }
        //nav.prevButton.SetActive(true);
        nav.entryStreet.SetActive(true);
        nav.nextButton.SetActive(false);

        nav.currentStateType = StateLocation.Street;
    }

}


