using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataSaver : MonoBehaviour
{
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        
        StarSystem.SetAllLevelStars(PlayerPrefs.GetString("Stars"));
    }
}
