using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetButtonRandom : MonoBehaviour
{
    public void UseButtonMap()
    {
        int numOfMaps = System.Enum.GetNames(typeof(MapRef)).Length;
        int index = Random.Range(0, numOfMaps - 1);

        GameManager.Instance.GetLevelSettings().SetMapType((MapRef)index);
    }

    public void UseButtonGamemode()
    {
        int numOfGamemodes = System.Enum.GetNames(typeof(GamemodeRef)).Length;
        int index = Random.Range(0, numOfGamemodes - 1);

        GameManager.Instance.GetLevelSettings().SetGamemode((GamemodeRef)index);
    }
}
