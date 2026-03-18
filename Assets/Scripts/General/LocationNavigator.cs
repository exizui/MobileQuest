using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static LocationController;

public class LocationNavigator : MonoBehaviour
{

    public static LocationNavigator Controller;

    [SerializeField]
    private List<Locations> sceneLocations = new List<Locations>();


    private Dictionary<LocationID, Locations> sceneMap;
    private readonly HashSet<LocationID> _blockedNextButtLoc = new HashSet<LocationID>()
    {
        LocationID.Level_2B,
        LocationID.Level_3B,
        LocationID.Stairs
    };

    private BaseLocations activeLocation;
    private LocationID activeLocationID;
    private int activeIndex;


    public GameObject nextButton;
    public GameObject prevButton;
    public GameObject exitButton;

    private LocationID prevLocationID;
    private void Awake()
    {
        Controller = this;

        sceneMap = new Dictionary<LocationID, Locations>();

        foreach (Locations loc in sceneLocations)
        {
            //sceneMap.Add(loc.id, loc);
            sceneMap[loc.id] = loc;
            loc.gameObject.SetActive(false);
        }


        //foreach (var pair in sceneMap)
        //{
        //    Debug.Log("В словаре есть: " + pair.Key + " -> " + pair.Value.name);
        //}
    }

    private void Start()
    {
        LoadLocation(LocationID.Street);
    }

    public void LoadLocation(LocationID idLoc)
    {
        if (activeLocation != null)
        {
            prevLocationID = activeLocationID;
            //activeLocation.gameObject.SetActive(false);
            print(prevLocationID.ToString());
            activeLocation.Exit();
        }


        activeLocationID = idLoc;
        //activeIndex = (int)id;
        activeIndex = sceneLocations.FindIndex(loc => loc.id == idLoc);

        activeLocation = sceneMap[idLoc];
        activeLocation.Entry();

        Debug.Log("Текущая локация: " + activeLocationID);
        CheckDeadEnd(idLoc);
    }

    public void PrevLocation()
    {
        if (CheckStairs()) return;

        if (activeIndex > 0)
        {
            activeIndex--;
            //LoadLocation((LocationID)activeIndex);
            LoadLocation((sceneLocations[activeIndex]).id);
        }
    }

    public void NextLocation()
    {
        //if (CheckStairs()) return;


        if (activeIndex < sceneLocations.Count - 1)
        {
            activeIndex++;
            //LoadLocation((LocationID)activeIndex);
            LoadLocation((sceneLocations[activeIndex]).id);
        }
    }

    private bool CheckStairs()
    {
        if (activeLocationID == LocationID.Level_2A)
        {
            LoadLocation(LocationID.Stairs);
            return true;
        }

        if (activeLocationID == LocationID.Level_3A)
        {
            LoadLocation(LocationID.Stairs);
            return true;
        }

        return false;
    }

    public void CheckRoomUI()
    {
        if (activeLocation.CompareTag("Audience"))
        {
            nextButton.SetActive(false);
            prevButton.SetActive(false);
            exitButton.SetActive(true);
        }
        else
        {
            nextButton.SetActive(true);
            prevButton.SetActive(true);
            exitButton.SetActive(false);
        }
    }

    public void SetPrevLocation(LocationID id)
    {
        prevLocationID = id;
    }

    public void ExitRoom()
    {
        LoadLocation(prevLocationID);
    }

    private void CheckDeadEnd(LocationID currentLoc)
    {
        bool isBlocked = _blockedNextButtLoc.Contains(currentLoc);
        nextButton.SetActive(!isBlocked);
    }

    public void LoadAudience(Audience aud)
    {
        if (activeLocation != null)
        {
            prevLocationID = activeLocationID;
            activeLocation.Exit();
        }

        activeLocation = aud;
        activeLocation.Entry();

    }
}