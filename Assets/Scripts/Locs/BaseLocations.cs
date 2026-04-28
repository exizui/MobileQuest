using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseLocations : MonoBehaviour
{
    //public LocationID LocationID;
    public StateLocation stateType;
    public virtual void Entry()
    {
        gameObject.SetActive(true);
        OnEnter();
    }
    

    public virtual void Exit()
    {
        gameObject.SetActive(false);
        OnExit();
    }

    protected virtual void OnEnter() { }
    protected virtual void OnExit() { }
}
