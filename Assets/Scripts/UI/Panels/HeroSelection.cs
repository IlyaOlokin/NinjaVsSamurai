using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroSelection : MonoBehaviour
{
    [SerializeField] private GameObject chooseHero, heroSelection;
    [SerializeField] private GameObject playerPos;

    [SerializeField] private PausePanel pause;
    [SerializeField] private GameObject playerCanvas;

    [SerializeField] private GameObject heroNinja, heroSumo, heroUdzu;
    [SerializeField] private GameObject infoNinfa, infoSumo, infoUdzu;
    private GameObject chosen;

    public bool heroSelected = false;

    private void Start()
    {
        chosen = heroNinja;
    }

    public void OnButtonChoose()
    {
        chooseHero.SetActive(false);
        heroSelection.SetActive(true);
        FindObjectOfType<AudioManager>().Play("ButtonClick");
    }

    public void OnButtonClose()
    {
        chooseHero.SetActive(true);
        heroSelection.SetActive(false);
        FindObjectOfType<AudioManager>().Play("ButtonClick");
    }

    public void OnButtonHeroLeft()
    {
        chosen = heroNinja;
        infoNinfa.SetActive(true);
        infoSumo.SetActive(false);
        infoUdzu.SetActive(false);
        FindObjectOfType<AudioManager>().Play("ButtonClick");
    }

    public void OnButtonHeroMid()
    {
        chosen = heroSumo;
        infoNinfa.SetActive(false);
        infoSumo.SetActive(true);
        infoUdzu.SetActive(false);
        FindObjectOfType<AudioManager>().Play("ButtonClick");
    }

    public void OnButtonHeroRight()
    {
        chosen = heroUdzu;
        infoNinfa.SetActive(false);
        infoSumo.SetActive(false);
        infoUdzu.SetActive(true);
        FindObjectOfType<AudioManager>().Play("ButtonClick");
    }

    public void OnButtonAccept()
    {
        SpawnHero(chosen, playerPos.transform.localPosition);
        heroSelection.SetActive(false);
        //chosen.SetActive(true);
        pause.enabled = true;
        heroSelected = true;
        FindObjectOfType<AudioManager>().Play("ButtonClick");
    }

    private void SpawnHero(GameObject prefab, Vector3 position)
    {
        var childObj = Instantiate(prefab);
        childObj.transform.SetParent(playerCanvas.transform);
        childObj.transform.localPosition = position;
        childObj.transform.localRotation = Quaternion.identity;
        childObj.transform.localScale = Vector3.one;
    }
}
