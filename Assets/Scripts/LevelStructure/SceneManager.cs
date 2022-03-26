using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


[CreateAssetMenu(fileName = "sceneManager", menuName = "Scene Data/Manager")]
public class SceneData : ScriptableObject
{
    [SerializeField] private List<Level> levels = new List<Level>();
    [SerializeField] private List<MenuScene> menus = new List<MenuScene>();

    [Header("               Current Level")]
    [SerializeField] private int CurrentLevelIndex = 1;

    #region LEVELS

    // Load a scene with a given index
    public void LoadLevelWithIndex(int index)
    {
        if (index <= levels.Count)
        {
            //Load the level
            SceneManager.LoadSceneAsync("Level" + index.ToString());
        }
        //reset the index if we have no more levels or overflows during testing
        else CurrentLevelIndex = 1;
    }

    // Start next level
    public void NextLevel()
    {
        CurrentLevelIndex++;
        LoadLevelWithIndex(CurrentLevelIndex);
    }

    // Restart current level
    public void RestartLevel()
    {
        LoadLevelWithIndex(CurrentLevelIndex);
    }

    // Main Menu = 0, New game = 1, so load level 1
    public void NewGame()
    {
        CurrentLevelIndex = 1;
        LoadLevelWithIndex(CurrentLevelIndex);
    }

    #endregion LEVELS

    #region MENUS

    // Load Main Menu
    public void LoadMainMenu()
    {
        SceneManager.LoadSceneAsync(menus[(int)Type.Main_Menu].SceneName);
    }

    // Load Pause Menu
    public void LoadPauseMenu()
    {
        SceneManager.LoadSceneAsync(menus[(int)Type.Pause_Menu].SceneName);
    }

    #endregion MENUS
}