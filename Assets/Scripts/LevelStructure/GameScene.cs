using UnityEngine;

/// <summary>
/// Stores data relating to a scene within a level
/// </summary>
/// 
public class GameScene : ScriptableObject
{
    [Header("Description")]
    [SerializeField] private string sceneName;
    [SerializeField] private string shortDescription;

    [Header("Audio")]
    [SerializeField] protected AudioClip music;

    [Range(0.0f, 1.0f)]
    [SerializeField] private float musicVolume;


    public string SceneName { get => sceneName; set => sceneName = value; }
    public float MusicVolume { get => musicVolume; set => musicVolume = value; }
}