using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour, IDataPersistence
{
    public static SceneManager instance;

    public int currentLevel = 0;
    public int unlockedLevels = 1;
    public bool[] keysCollected = new bool[3];

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        else
        {
            Destroy(gameObject);
        }
    }
    public void LoadNextScene(string sceneName)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }

    public void LoadLevel(int levelIndex)
    {
        if (levelIndex < unlockedLevels)
        {
            currentLevel = levelIndex;
            UnityEngine.SceneManagement.SceneManager.LoadScene(levelIndex);
        }

        else
        {
            Debug.Log("level " + levelIndex + " is locked");
        }
    }

    public void CompleteLevel(int levelIndex)
    {
        if (levelIndex < keysCollected.Length)
        {
            keysCollected[levelIndex] = true;

            if (unlockedLevels < levelIndex + 2 && levelIndex + 1 < keysCollected.Length)
            {
                unlockedLevels = levelIndex + 2;
            }

            if (AllKeysCollected())
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene("youWin");
            }

            else
            {
                LoadLevel(0); //Return to HUB
            }
        }
    }

    public bool AllKeysCollected()
    {
        foreach (bool collected in keysCollected)
        {
            if (!collected) return false;
        }

        return true;
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("The game has closed");
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
            Debug.Log("The game has closed");
        }
    }

    public void LoadData(GameData data)
    {
        currentLevel = data.currentLevel;
        unlockedLevels = data.unlockedLevels;
        keysCollected = data.keysCollected ?? new bool[3];
    }

    public void SaveData(ref GameData data)
    {
        data.currentLevel = currentLevel;
        data.unlockedLevels = unlockedLevels;
        data.keysCollected = keysCollected;
    }
}
