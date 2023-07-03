using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "NewMapData", menuName = "Data/Maps/Map")]
public class MapData : ScriptableObject
{
    public MapRef MapRef => mapRef;
    [SerializeField] private MapRef mapRef;

    public string Title => mapName;
    [Space][SerializeField] private string mapName;

    public string Description => description;
    [SerializeField] private string description;

    public Sprite Image => image;
    [Space][SerializeField] private Sprite image;

    public GameObject TeamSelectBG => teamSelectBG;
    [Space][SerializeField] private GameObject teamSelectBG;

    public string SceneName => sceneName;
    [Space][SerializeField] private string sceneName;
}
