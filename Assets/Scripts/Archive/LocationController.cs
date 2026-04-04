using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

//public class LocationController : MonoBehaviour
//{
    //public enum LocationID
    //{
    //    Street,
    //    Level_1,
    //    Stairs,

    //    Level_2A,
    //    Level_2B,

    //    Level_3A,
    //    Level_3B,

    //}
    
    //private LocationID currentLocation;

    //public Dictionary<LocationID, Locations> locationMap;

    //public static LocationController Instance;
    //[SerializeField] 
    //private List<GameObject> levels = new List<GameObject>();
    //public GameObject currentLevel;

    //private int currentIndex = 0;
    //private void Awake()
    //{
    //    Instance = this;
    //}

    //private void Start()
    //{
    //    LoadLevel(LocationID.Street);
    //}

    //public void LoadLevel(LocationID id)
    //{
    //    DisableLevel();
        
    //    //GameObject prefab = levelPrefabs[(int)id];
    //    //currentLevel = Instantiate(prefab);
    //    currentLocation = id;
    //    currentIndex = (int)id;
    //    currentLevel = levels[currentIndex];
    //    currentLevel.SetActive(true);

    //}

    //public void DisableLevel()
    //{
    //    if (currentLevel != null)
    //    {
    //        currentLevel.SetActive(false);
    //    }
    //}

    //public void PrevLevel()
    //{
    //    if (StairsCheck()) return;


    //    print("назад" + currentLevel.ToString());
    //    if (currentIndex > 0)
    //    {
    //        LoadLevel((LocationID)(currentIndex - 1));
    //    }
        
        
    //}

    //public void NextLevel()
    //{
    //    //if (StairsCheck()) return;


    //    print("вперед" + currentLevel.ToString());
    //    if (currentIndex < levels.Count - 1)
    //    {
    //        LoadLevel((LocationID)(currentIndex + 1));
    //    }
        
    //}

    //private bool StairsCheck()
    //{
    //    if (currentLocation == LocationID.Level_2A || currentLocation == LocationID.Level_3A)
    //    {
    //        LoadLevel((LocationID.Stairs));
    //        print("stairs");
    //        return true;
            
    //    }
    //    return false;
    //}




    #region Вперед, назад, удаление

    //public void PrevLevel()
    //{
    //    if (currentIndex > 0)
    //    {
    //        LoadLevel(currentIndex - 1);
    //    }

    //}

    //public void NextLevel()
    //{
    //    if(currentIndex < levelPrefabs.Count - 1)
    //    {
    //        LoadLevel(currentIndex + 1);
    //    }
    //}

    //public void UnloadLevel()
    //{
    //    if (currentLevel != null)
    //    {
    //        Destroy(currentLevel);
    //    }
    //}
    #endregion
//}
