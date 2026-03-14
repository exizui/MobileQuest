using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationController : MonoBehaviour
{
    [SerializeField] private List<GameObject> levelPrefabs;
    [HideInInspector] public GameObject currentLevel;
    private int currentIndex = 0;
    private void Start()
    {
        LoadLevel(0);
    }

    public void LoadLevel(int index)
    {
        UnloadLevel();
        currentIndex = index;
        GameObject prefab = levelPrefabs[currentIndex];

        currentLevel = Instantiate(prefab);
    }

    public void PrevLevel()
    {
        if (currentIndex > 0)
        {
            LoadLevel(currentIndex - 1);
        }

    }

    public void NextLevel()
    {
        if(currentIndex < levelPrefabs.Count - 1)
        {
            LoadLevel(currentIndex + 1);
        }
    }

    public void UnloadLevel()
    {
        if (currentLevel != null)
        {
            Destroy(currentLevel);
        }
    }
}
