using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseLocations : MonoBehaviour
{
    
    public virtual void Entry()
    {
        gameObject.SetActive(true);
    }
    

    public virtual void Exit()
    {
        gameObject.SetActive(false);
    }
}
