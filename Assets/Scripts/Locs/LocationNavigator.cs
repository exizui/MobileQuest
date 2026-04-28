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
    private Dictionary<StateLocation, ILocationState> stateMap;

    private BaseLocations activeLocation;

    [Header("Стартова локація")]
    public LocationID startLocationID;

    private LocationID activeLocationID;
    private LocationID prevLocationID;
    private LocationID pendingLocationID;
    public LocationID CurrentLocationID() => activeLocationID;
    public LocationID PrevLocationID() => prevLocationID;

    public GameObject nextButton;
    public GameObject prevButton;
    public GameObject exitButton;
    public GameObject entryStreet;
    public GameObject fader;
    public InventoryUI inventoryUI;



    private const string LOCATION_KEY = "last location";

    private ILocationState currentState;

    [HideInInspector]
    public StateLocation currentStateType;

    public Locations CurrentLocation => activeLocation as Locations;

    public bool ACTIVEFADER = false;
    private void Awake()
    {
        //fader.SetActive(true);/////DEBUG!!!!!
        if (ACTIVEFADER)
        {
            fader.SetActive(true);
        }
        else
        {
            fader.SetActive(false);
        }

        Controller = this;

        sceneMap = new Dictionary<LocationID, Locations>();

        foreach (Locations loc in sceneLocations)
        {
            sceneMap[loc.id] = loc;
            loc.gameObject.SetActive(false);
        }

        stateMap = new Dictionary<StateLocation, ILocationState>()
        {
            { StateLocation.Corridor, new CorridorState() },
            { StateLocation.Audience, new AudienceState() },
            { StateLocation.Street, new StreetState() }
        };
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
        }
        
    }

    public void LoadLocation(LocationID idLoc)
    {
        //if (!sceneMap.ContainsKey(idLoc))
        //{
        //    Debug.LogError($"Location {idLoc} not found in current scene");
        //    return;
        //}
        if (activeLocation != null)
        {
            prevLocationID = activeLocationID;
            activeLocation.Exit();
        }
        inventoryUI.CloseInventory();

        activeLocationID = idLoc;
        activeLocation = sceneMap[idLoc];
        activeLocation.Entry();

        CheckState();

        SaveCurrentLocation();
    }

    public void LoadPrevLocation()
    {
        //GoToLocation(prevLocationID);
        LoadLocation(prevLocationID);
        //pendingLocationID = prevLocationID;
    }
    public void GoToLocation(LocationID targetLoc)
    {
        if (targetLoc == LocationID.None) return;
        if (ACTIVEFADER) { StartCoroutine(NextRoutine(targetLoc)); } else { LoadLocation(targetLoc); }
    }
    
    private void CheckState()
    {
        SetState(stateMap[activeLocation.stateType]);
    }

    public void NextLocation()
    {
        if (CurrentLocation.next != LocationID.None)
        {
            GoToLocation(CurrentLocation.next);
        }
    }

    private IEnumerator NextRoutine(LocationID targetLoc)
    {
        yield return Fader.instance.FadeOut();
        LoadLocation(targetLoc);
        yield return Fader.instance.FadeIn();
    }
 
    public void PrevLocation()
    {
        if (CurrentLocation.prev != LocationID.None)
        {
            GoToLocation(CurrentLocation.prev);
        }
    }

    public void ExitRoom()
    {
        GoToLocation(prevLocationID);     
    }

    private void SetState(ILocationState newState)
    {
        currentState = newState;
        currentState.Enter(this);
    }

    public void SetUI(bool next, bool prev, bool entry)
    {
        nextButton.SetActive(next);
        prevButton.SetActive(prev);
        entryStreet.SetActive(entry);
    }

    public void OffExitButton()
    {
        exitButton.SetActive(false);
    }
    public Locations GetCurrentLocation()
    {
        return activeLocation as Locations;
    }

    public void SetPrevLocation(LocationID id)
    {
        prevLocationID = id;
    }

    private void SaveCurrentLocation()
    {
        SaveSystem.instance.SaveLocation(LOCATION_KEY,(int)activeLocationID); //збереження індекса локації
    }

    private void OnEnable()
    {
        SceneLoader.OnLoadScene += Disable;
    }

    public void Disable()
    {
        if(activeLocation == null) return; //без цього не буде працювати!!!

        activeLocation.Exit();

        print(activeLocation);
        SceneLoader.OnLoadScene -= Disable;
        SceneLoader.OnLoadScene += Enable;
    }

    public void Enable()
    {
        RefreshLocations();
        activeLocation = sceneMap[activeLocationID];
        activeLocation.Entry();

        SceneLoader.OnLoadScene -= Enable;
    }

    private void RefreshLocations()
    {
        sceneMap.Clear();
        Locations[] locations = FindObjectsOfType<Locations>();
        foreach (Locations loc in locations)
        {
            sceneMap[loc.id] = loc;
        }
    }
}