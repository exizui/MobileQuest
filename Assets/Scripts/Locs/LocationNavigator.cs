using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LocationNavigator : MonoBehaviour
{

    public static LocationNavigator Controller;

    [SerializeField]
    private List<Locations> sceneLocations = new List<Locations>();

    private Dictionary<LocationID, Locations> sceneMap;

    private readonly HashSet<LocationID> _blockedNextButtLoc = new HashSet<LocationID>()
    {
        LocationID.Level_2C,
        LocationID.Level_3C,
        LocationID.Stairs
    };

    private BaseLocations activeLocation;

    private LocationID activeLocationID;
    private LocationID prevLocationID;
    public LocationID CurrentLocationID() => activeLocationID;
    public LocationID PrevLocationID() => prevLocationID;


    private int activeIndex =1;


    public GameObject nextButton;
    public GameObject prevButton;
    public GameObject exitButton;
    public GameObject entryStreet;

    public LocationID startLocationID;
    private const string LOCATION_KEY = "last location";

    private ILocationState currentState;

    public StateLocation currentStateType;


    private void Awake()
    {
        Controller = this;

        sceneMap = new Dictionary<LocationID, Locations>();

        foreach (Locations loc in sceneLocations)
        {
            sceneMap[loc.id] = loc;
            //Debug.Log(loc.name + " = " + loc.id);
            loc.gameObject.SetActive(false);
        }
        //foreach (var pair in sceneMap)
        //{
        //    Debug.Log("В словаре есть: " + pair.Key + " -> " + pair.Value.name);
        //}
    }

    private void Start()
    {
        if (PlayerPrefs.HasKey(LOCATION_KEY))
        {
            LocationID savedloc = (LocationID)PlayerPrefs.GetInt(LOCATION_KEY);
            LoadLocation(savedloc);
        }
        else
        {
            LoadLocation(startLocationID);
            //CheckStreet();
            //entryStreet.SetActive(true);
        }
    }

    public void LoadLocation(LocationID idLoc)
    {
        if (activeLocation != null)
        {
            prevLocationID = activeLocationID;
            activeLocation.Exit();
        }

        activeLocationID = idLoc;
        activeIndex = sceneLocations.FindIndex(loc => loc.id == idLoc);

        activeLocation = sceneMap[idLoc];
        activeLocation.Entry();

        CheckState();

        SaveCurrentLocation();
    }

    private void CheckState()
    {
        if(activeLocationID == LocationID.Street)
        {
            SetState(new StreetState());
        }
        else
        {
            SetState(new CorridorState());
        }
    }
    public void PrevLocation()
    {
        if (CheckStairs()) return;

        if (activeIndex > 0)
        {
            activeIndex--;
            LoadLocation((sceneLocations[activeIndex]).id);
        }
    }

    public void NextLocation()
    {
        if (activeIndex < sceneLocations.Count - 1)
        {
            activeIndex++;
            LoadLocation((sceneLocations[activeIndex]).id);
        }
    }

    private bool CheckStairs()
    {
        if (activeLocationID == LocationID.Level_2A || activeLocationID == LocationID.Level_3A)
        {
            LoadLocation(LocationID.Stairs);
            return true;
        }
        return false;
    }

    public void ExitRoom()
    {
        LoadLocation(prevLocationID);      
    }

    public void LoadAudience(LocationID aud) ///было QuestAudience!!!!!
    {
        if (activeLocation != null)
        {
            prevLocationID = activeLocationID;
            activeLocation.Exit();
        }

        //activeLocation = aud;
        activeLocationID = aud;
        activeIndex = sceneLocations.FindIndex(loc => loc.id == aud);

        activeLocation = sceneMap[aud];
        Debug.Log(activeLocation);
        activeLocation.Entry();


        SetState(new AudienceState());
        SaveCurrentLocation();
    }

    private void SetState(ILocationState newState)
    {
        currentState = newState;
        currentState.Enter(this);
    }

    public void SetEnumState(StateLocation type)
    {
        currentStateType = type;
        print("ЕнамСтейт" + currentStateType);
        switch (type)
        {
            case StateLocation.Corridor:
                SetState(new CorridorState());
                break;

            case StateLocation.Audience:
                SetState(new AudienceState()); 
                exitButton.SetActive(true);
                break;

            case StateLocation.Street:
                SetState(new StreetState());
                break;
        }
    }
    public bool IsNextBlocked()
    {
        return _blockedNextButtLoc.Contains(activeLocationID);
    }

    public bool IsPrevBlocked()
    {
        return activeLocationID == LocationID.Level_Shop;
    }
    public void SetPrevLocation(LocationID id)
    {
        prevLocationID = id;
    }
    private void SaveCurrentLocation()
    {
        SaveSystem.instance.SaveLocation(LOCATION_KEY,(int)activeLocationID); //збереження індекса локації
    }
}