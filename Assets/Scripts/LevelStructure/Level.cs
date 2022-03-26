using UnityEngine;


[CreateAssetMenu(fileName = "NewLevel", menuName = "Scene Data/Level")]
public class Level : GameScene
{
    [Header("Level specific")]
    [SerializeField] private int levelSpecificVariable;
}
