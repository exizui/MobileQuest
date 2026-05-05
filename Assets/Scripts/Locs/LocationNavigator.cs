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
    public LocationID CurrentLocationID() => activeLocationID;
    public LocationID PrevLocationID() => prevLocationID;

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

    public Locations CurrentLocation => activeLocation as Locations;

    private void Awake()
    {

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

        nextButt = _next.GetComponent<Button>();
        prevButt = _prev.GetComponent<Button>();
        entryButt = _entryStreet.GetComponent<Button>();
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
        //LoadLocation(prevLocationID);
        LoadLocation(activeLocationID);
    }
    public void GoToLocation(LocationID targetLoc)
    {
        if (targetLoc == LocationID.None) return;
        SwitchInteract(false);
        StartCoroutine(NextRoutine(targetLoc));

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
        SwitchInteract(true);
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
        _next.SetActive(next);
        _prev.SetActive(prev);
        _entryStreet.SetActive(entry);
    }

    public void OffExitButton()
    {
        _exit.SetActive(false);
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

    public void SetSwipe(bool value)
    {
        swipeEnabled = value;
    }

    public bool IsSwipeEnabled()
    {
        return swipeEnabled;
    }

}