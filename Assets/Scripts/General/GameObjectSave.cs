using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

//public class GameObjectSave : MonoBehaviour, ISaveable
//{
//    [SerializeField] private string saveID;
//    [SerializeField] private GameObject target; // ← посилання на об'єкт який вимикається

//    public string SaveID => saveID;
//    public object CaptureState()
//    {
//        return target.activeSelf; // зберігаємо стан target
//    }

//    public void RestoreState(object state)
//    {
//        target.SetActive((bool)state);
//    }

//}


[Serializable]
public class ObjectState
{
    public string id;
    public bool active;
}
public class GameObjectSave : MonoBehaviour
{
    public string id;
}