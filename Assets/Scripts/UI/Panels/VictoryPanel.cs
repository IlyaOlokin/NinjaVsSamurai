using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;

public class VictoryPanel : MonoBehaviour
{
    [SerializeField] private GameObject victoryPanel;
    [SerializeField] private GameObject levelNumber;
    [SerializeField] private GameObject attemptsCount;
    [SerializeField] private GameObject currentStarsLeftToNextWorld;
    [SerializeField] private GameObject totalStarsLeftToNextWorld;
    [SerializeField] private StarsOnLevel starsOnLevel;
    [SerializeField] private StarsOnVictoryPanel starsOnVictoryPanel;

    private LevelManager levelManager;
    private bool levelCompleted;

    private void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
    }
    
    private void Update()
    {
        if (!levelCompleted) CheckLevelEnd();
    }

    private void CheckLevelEnd()
    {
        if (levelManager.IsWinCondition())
        {
            levelCompleted = true;
            victoryPanel.SetActive(true);
            YandexSDK SDK = FindObjectOfType<YandexSDK>();
            try
            {
                SDK.ShowCommonAdvertisment();
            }
            catch (Exception e)
            {
                Console.WriteLine("add");
            }
            StarSystem.SetLevelStars(int.Parse(GetLevelNumber()), starsOnLevel.GetStars());
            PlayerPrefs.SetString("Stars", StarSystem.SaveAllLevelStars());
            FillTexts();
            Time.timeScale = 0f;
            var pi = FindObjectOfType<PlayerInput>();
            if (pi != null) pi.enabled = false;
        }
    }
    
    public void OnButtonRestart()
    {
        victoryPanel.SetActive(false);
        FindObjectOfType<AudioManager>().Play("ButtonClick");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnButtonNext()
    {
        victoryPanel.SetActive(false);
        
        var lvlNum = int.Parse(GetLevelNumber());
        if (lvlNum % 6 == 0 && StarSystem.GetCurrentStarsAmount() < StarSystem.GetAllWorldBarriers()[lvlNum / 6 - 1])
        {
            Time.timeScale = 1;
            SceneManager.LoadScene("LevelMenu" + ChooseMenuVer());
            return;
        }
        FindObjectOfType<AudioManager>().Play("ButtonClick");
        //SceneManager.LoadScene("Level" + (lvlNum + 1) + ChooseMenuVer());
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void OnButtonMenu()
    {
        victoryPanel.SetActive(false);
        FindObjectOfType<AudioManager>().Play("ButtonClick");
        SceneManager.LoadScene("LevelMenu" + ChooseMenuVer());
    }
    
    public void FillTexts()
    {
        levelNumber.GetComponent<Text>().text = GetLevelNumber();
        attemptsCount.GetComponent<Text>().text = FindObjectOfType<PlayerInput>().GetInputAmount().ToString();
        currentStarsLeftToNextWorld.GetComponent<Text>().text = StarSystem.GetCurrentStarsAmount().ToString();
        totalStarsLeftToNextWorld.GetComponent<Text>().text = "/" + StarSystem.GetNextWorldBarrier();
        starsOnVictoryPanel.SetStars(starsOnLevel.GetStars());
    }
    
    private string GetLevelNumber()
    {
        var sceneName = SceneManager.GetActiveScene().name;
        return new string(sceneName.Where(c => char.IsDigit(c)).ToArray());
    }

    private string ChooseMenuVer()
    {
        return SceneManager.GetActiveScene().name.Contains("Sber")
                ? "Sber"
                : "";
    }
}
