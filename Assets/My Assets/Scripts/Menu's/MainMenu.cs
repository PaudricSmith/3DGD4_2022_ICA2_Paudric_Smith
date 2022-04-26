using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;


public class MainMenu : MonoBehaviour
{
    //List of the scenes to load from Main Menu
    List<AsyncOperation> scenesToLoad = new List<AsyncOperation>();


    private void Awake()
    {
    }


    public void StartGame()
    {
        HideMenu();
    }


    public void StartGameSO()
    {
        HideMenu();

        //Load the Scene asynchronously in the background
        scenesToLoad.Add(SceneManager.LoadSceneAsync("Level1"));
    }


    public void HideMenu()
    {
        
    }


    public void ExitGame()
    {

#if UNITY_EDITOR

        UnityEditor.EditorApplication.isPlaying = false;

#endif

        Application.Quit();
    }
}
