using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader
{
    public static void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public static void LoadNextLevel()
    {
        int level = PlayerPrefsStrings.GetIntValue(PlayerPrefsStrings.Level);
        PlayerPrefs.SetInt(PlayerPrefsStrings.Level.Name, level + 1);
        PlayerPrefs.Save();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public static void LoadPreviousLevel()
    {
        int level = PlayerPrefsStrings.GetIntValue(PlayerPrefsStrings.Level);
        if (level > 1)
        {
            PlayerPrefs.SetInt(PlayerPrefsStrings.Level.Name, level - 1);
            PlayerPrefs.Save();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
