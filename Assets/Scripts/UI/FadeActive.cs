using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeActive : MonoBehaviour
{
    public GameObject _fade;
    private void Awake()
    {
        _fade.SetActive(true);
    }
}
