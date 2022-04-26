using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


[CreateAssetMenu(fileName = "NewLevelManager", menuName = "Scene Data/Manager")]
public class LevelManager : ScriptableObject
{
    [SerializeField] private List<Level> levels = new List<Level>();
    [SerializeField] private List<MenuScene> menus = new List<MenuScene>();

    [Header("               Current Level")]
    [SerializeField] private int CurrentLevelIndex = 1;

    [Header("               Events")]
    [SerializeField] private GameEventSO OnUnloadPauseScene;


    #region LEVELS

    // Load a scene with a given index
    public void LoadLevelWithIndex(int index)
    {
        if (index <= levels.Count)
        {
            //Load the level
            if (index == 1)
            {
                SceneManager.LoadSceneAsync("MainScene");
            }
            else if (index == 2)
            {
                SceneManager.LoadSceneAsync("AlternateScene");
            }
        }
        //reset the index if we have no more levels or overflows during testing
        else
        {
            CurrentLevelIndex = 1;
        }
    }

    // Start next level
    public void NextLevel()
    {
        CurrentLevelIndex++;
        LoadLevelWithIndex(CurrentLevelIndex);
    }

    // Start previous level
    public void PreviousLevel()
    {
        CurrentLevelIndex--;
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

    // Quit game
    public void QuitGame()
    {
        // Set index to Main menu index '0'
        CurrentLevelIndex = 0;

        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
            Application.Quit();
    }

    #endregion LEVELS

    #region MENUS

    // Load Main Menu
    public void LoadMainMenu()
    {
        SceneManager.LoadSceneAsync(menus[(int)Type.Main_Menu].SceneName);
    }

    // Load Pause Menu additively on top of level scene
    public void LoadPauseMenu()
    {
        SceneManager.LoadSceneAsync(menus[(int)Type.Pause_Menu].SceneName, LoadSceneMode.Additive);
    }
    
    // Unload Pause Menu
    public void UnloadPauseMenu()
    {
        SceneManager.UnloadSceneAsync(menus[(int)Type.Pause_Menu].SceneName);

        // Raise event to InputManager to update controls state
        OnUnloadPauseScene.Raise();
    }

    #endregion MENUS
}