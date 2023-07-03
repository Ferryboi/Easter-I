using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabyrinthToggle : MonoBehaviour
{
    [SerializeField] private Transform layouts;
    [SerializeField] private SpriteRenderer sensor;
    [SerializeField] private float chanceToToggle;

    private void Start()
    {
        sensor.material.color = new Color(0, 0, 0, 0);

        ChangeLayout();
    }

    private void OnBecameInvisible()
    {
        if(Random.Range(0, 100) < chanceToToggle)
        {
            ChangeLayout();
        }
    }

    private void ChangeLayout()
    {
        int index = Random.Range(0, layouts.childCount);

        for(int i = 0; i < layouts.childCount; i++)
        {
            if(index != i) layouts.GetChild(i).gameObject.SetActive(false);
            else layouts.GetChild(i).gameObject.SetActive(true);
        }
    }
}
