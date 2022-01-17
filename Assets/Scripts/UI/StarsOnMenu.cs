using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarsOnMenu : MonoBehaviour
{
    [SerializeField] private Text currentStars, totalStars;

    private void Start()
    {
        currentStars.text = StarSystem.GetCurrentStarsAmount().ToString();
        totalStars.text = "/" + StarSystem.TotalStars;
    }
}
