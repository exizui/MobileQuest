using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public class RoomNavigator : MonoBehaviour
//{
//    public static RoomNavigator Controller;

//    [SerializeField]
//    private List<Locations> rooms = new List<Locations>();

//    private Dictionary<LocationID, Locations> roomMap;

//    private void Awake()
//    {
//        Controller = this;

//        roomMap = new Dictionary<LocationID, Locations>();

//        foreach (Locations room in rooms)
//        {
//            roomMap.Add(room.id, room);
//            room.gameObject.SetActive(false);
//        }
//    }

//    public void EnterRoom(LocationID id)
//    {
//        if (!roomMap.ContainsKey(id))
//        {
//            Debug.LogWarning("Комната не найдена: " + id);
//            return;
//        }

//        LocationNavigator.Controller.LoadLocation(id);
//    }
//}
