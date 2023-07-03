using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewGamemodeGroup", menuName = "Data/Gamemodes/Group")]
public class GamemodeGroup : ScriptableObject
{
    [SerializeField] private GamemodeData[] gamemodes;

    public GamemodeData GetGamemode(GamemodeRef gamemodeRef)
    {
        for(int i = 0; i < gamemodes.Length; i++)
        {
            if(gamemodes[i].GamemodeRef == gamemodeRef)
            {
                return gamemodes[i];
            }
        }

        return null;
    }

    public GamemodeData[] GetGamemodes() { return gamemodes; }
}
