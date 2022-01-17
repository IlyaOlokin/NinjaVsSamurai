using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class LevelNumberPanel : MonoBehaviour
{
    [SerializeField] private Text levelNumber;

    void Start()
    {
        var sceneName = SceneManager.GetActiveScene().name;
        levelNumber.text = new string(sceneName.Where(c => char.IsDigit(c)).ToArray());
    }
}
