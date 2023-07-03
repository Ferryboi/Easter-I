using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewMapGroup", menuName = "Data/Maps/Group")]
public class MapGroup : ScriptableObject
{
    [SerializeField] private MapData[] maps;

    public MapData GetMap(MapRef mapRef)
    {
        for (int i = 0; i < maps.Length; i++)
        {
            if (maps[i].MapRef == mapRef)
            {
                return maps[i];
            }
        }

        return null;
    }

    public MapData[] GetMaps() { return maps; }
}
