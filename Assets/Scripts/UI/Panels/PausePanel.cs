using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PausePanel : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject soundCheckMark;
    [SerializeField] private Text levelNumberText;
    
    public void OnButtonRestart()
    {
        Resume();
        FindObjectOfType<AudioManager>().Play("ButtonClick");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnButtonMenu()
    {
        Resume();
        FindObjectOfType<AudioManager>().Play("ButtonClick");
        SceneManager.LoadScene("LevelMenu" + ChooseMenuVer());
    }

    public void OnButtonClose()
    {
        Resume();
        FindObjectOfType<AudioManager>().Play("ButtonClick");
    }

    public void OnButtonSound()
    {
        soundCheckMark.SetActive(!soundCheckMark.activeInHierarchy);
        FindObjectOfType<AudioManager>().Play("ButtonClick");
    }

    public void Pause()
    {
        Time.timeScale = 0;
        pausePanel.SetActive(true);
        var pi = FindObjectOfType<PlayerInput>();
        if (pi != null) pi.enabled = false;
        FindObjectOfType<AudioManager>().Play("ButtonClick");
    }

    public void Resume()
    {
        Time.timeScale = 1;
        var pi = FindObjectOfType<PlayerInput>();
        if (pi != null) pi.enabled = true;
        pausePanel.SetActive(false);
    }

    private void Start()
    {
        Resume();
        levelNumberText.text = GetLevelNumber();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pausePanel.activeInHierarchy)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
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
