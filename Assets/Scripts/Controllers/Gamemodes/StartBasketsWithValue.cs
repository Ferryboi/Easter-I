using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartBasketsWithValue : MonoBehaviour
{
    [SerializeField] private int value;

    private void Start()
    {
        List<Basket> baskets = LevelManager.Instance.GetBaskets();

        for(int i = 0; i < baskets.Count; i++)
        {
            baskets[i].AddToPocket(value);
        }
    }
}
