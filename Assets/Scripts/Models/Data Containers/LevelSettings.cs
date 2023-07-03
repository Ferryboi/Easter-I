using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class LevelSettings
{
    [SerializeField] private float roundDuration;
    [SerializeField] private int numOfRounds;
    [SerializeField] private GamemodeRef gamemode;
    [SerializeField] private MapRef mapType;

    public const float DEFAULT_ROUND_DURATION = 300f;

    public void ImportSettings(float roundDuration, int numOfRounds, GamemodeRef gamemode, MapRef mapType)
    {
        SetRoundDuration(roundDuration);
        SetNumOfRounds(numOfRounds);
        SetGamemode(gamemode);
        SetMapType(mapType);
    }

    public void SetRoundDuration(float roundDuration)
    {
        this.roundDuration = roundDuration;
    }

    public float GetRoundDuration()
    {
        return roundDuration;
    }

    public void SetNumOfRounds(int numOfRounds)
    {
        this.numOfRounds = numOfRounds;
    }

    public int GetNumOfRounds()
    {
        return numOfRounds;
    }

    public void SetGamemode(GamemodeRef gameType)
    {
        this.gamemode = gameType;
    }

    public GamemodeRef GetGamemode()
    {
        return gamemode;
    }

    public void SetMapType(MapRef mapType)
    {
        this.mapType = mapType;
    }

    public MapRef GetMapType()
    {
        return mapType;
    }
}
