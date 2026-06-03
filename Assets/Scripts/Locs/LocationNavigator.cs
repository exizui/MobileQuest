using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LocationNavigator : MonoBehaviour
{
    public static LocationNavigator Controller;

    [SerializeField]
    private List<Location> sceneLocations = new List<Location>();

    private Dictionary<LocationID, Location> sceneMap;
    private Dictionary<StateLocation, ILocationState> stateMap;

    private Location activeLocation;

    [Header("Стартова локація")]
    public LocationID startLocationID;

    private LocationID activeLocationID;
    private LocationID prevLocationID;
    public LocationID CurrentLocationID() => activeLocationID;
    public LocationID PrevLocationID() => prevLocationID;
    public Location GetCurrentLocation() => activeLocation;
    public Location CurrentLocation => activeLocation;

    [Header("UI КНОПКИ")]
    public GameObject _next;
    public GameObject _prev;
    public GameObject _exit;
    public GameObject _entryStreet;
    public GameObject _restartQuest;
    public InventoryUI inventoryUI;

    private Button nextButt;
    private Button prevButt;
    private Button entryButt;

    private bool swipeEnabled;


    private const string LOCATION_KEY = "last location";

    private ILocationState currentState;

    [HideInInspector]
    public StateLocation currentStateType;

    private void Awake()
    {
        Controller = this;

        sceneMap = new Dictionary<LocationID, Location>();

        foreach (Location loc in sceneLocations)
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

        nextButt = _next.GetComponent<Button>();
        prevButt = _prev.GetComponent<Button>();
        entryButt = _entryStreet.GetComponent<Button>();
    }
    private void Start()
    {
        if (activeLocation == null)
        {
            LoadLocation(startLocationID);
        }
        print("LOCNAVIGATOR" + currentStateType);
    }

    public void LoadLocation(LocationID idLoc)
    {
        if (activeLocation != null)
        {
            prevLocationID = activeLocationID;
            activeLocation.Exit();
        }
        inventoryUI.CloseInventory();

        activeLocationID = idLoc;
        activeLocation = sceneMap[idLoc];
     
        activeLocation.Entry();

        SetState(stateMap[activeLocation.stateType]);

        //SaveCurrentLocation(); //
    }

    public void LoadPrevLocation() => activeLocation.Entry(); //скорочений метод 
    public void ExitRoom() => GoToLocation(prevLocationID);
    public void HideExitDoor() => _exit.SetActive(false);

    public void GoToLocation(LocationID targetLoc)
    {
        if (targetLoc == LocationID.None) return;
        SwitchInteract(false);
        StartCoroutine(NextRoutine(targetLoc));
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
        SwitchInteract(true);
    }

    public void PrevLocation()
    {
        if (CurrentLocation.prev != LocationID.None)
        {
            GoToLocation(CurrentLocation.prev);
        }
    }

    private void SetState(ILocationState newState)
    {
        currentState = newState;
        currentState.Enter(this);
    }

    public void SetUI(bool next, bool prev, bool entry)
    {
        _next.SetActive(next);
        _prev.SetActive(prev);
        _entryStreet.SetActive(entry);
    }

    private void SwitchInteract(bool state)
    {
        if (state)
        {
            prevButt.interactable = true;
            nextButt.interactable = true;
            entryButt.interactable = true;
        }
        else
        {
            prevButt.interactable = false;
            nextButt.interactable = false;
            entryButt.interactable = false;
        }
    }
    public void SetPrevLocation(LocationID id) => prevLocationID = id;
    public void SetSwipe(bool value) => swipeEnabled = value;
    public bool IsSwipeEnabled() => swipeEnabled;

}