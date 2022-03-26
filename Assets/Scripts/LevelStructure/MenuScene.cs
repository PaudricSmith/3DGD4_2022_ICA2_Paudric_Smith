using UnityEngine;


public enum Type
{
    Main_Menu,
    Pause_Menu
}


[CreateAssetMenu(fileName = "NewMenu", menuName = "Scene Data/Menu")]
public class Menu : GameScene
{
    // Choose which type of menu from the editor
    [Header("Menu specific")]
    [SerializeField] private Type type;
}