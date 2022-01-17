using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorldBlockStars : MonoBehaviour
{
    [SerializeField] private Text starsLeft;

    public void SetText(int starsNeed)
    {
        starsLeft.text = StarSystem.GetCurrentStarsAmount() + "/" + starsNeed;
    }
}
