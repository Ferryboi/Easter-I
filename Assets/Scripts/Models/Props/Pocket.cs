using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pocket : MonoBehaviour
{
    [HideInInspector] public bool CanCollect = true;

    public delegate void OnChangeDelegate(int change);
    public OnChangeDelegate OnChange;

    public int GetEggCount() { return eggCount; }
    private int eggCount = 0;

    public int GetPrevEggCount() { return prevEggCount; }
    private int prevEggCount = 0;

    public void AddToPocket(int eggs)
    {
        prevEggCount = eggCount;
        eggCount += eggs;

        OnChange?.Invoke(eggs);
    }

    public int RemoveFromPocket(int eggs)
    {
        prevEggCount = eggCount;
        int removed;

        if(eggCount <= eggs)
        {
            removed = EmptyPocket();
        }
        else
        {
            eggCount -= eggs;
            removed = eggs;

            OnChange?.Invoke(-removed);
        }

        return removed;
    }

    public int EmptyPocket()
    {
        prevEggCount = eggCount;

        int removed = eggCount;
        eggCount = 0;

        OnChange?.Invoke(removed);
        return removed;
    }
}
