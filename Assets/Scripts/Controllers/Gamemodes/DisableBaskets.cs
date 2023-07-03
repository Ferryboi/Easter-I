using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableBaskets : MonoBehaviour
{
    private void Start()
    {
        List<Basket> baskets = LevelManager.Instance.GetBaskets();

        for(int i = 0; i < baskets.Count; i++)
        {
            baskets[i].gameObject.SetActive(false);
        }
    }
}
