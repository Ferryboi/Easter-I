using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasketEggVisual : MonoBehaviour
{
    [SerializeField] private Basket basket;
    [Space]
    [SerializeField] private SpriteRenderer eggsDisplay;
    [SerializeField] private Sprite eggs1;
    [SerializeField] private Sprite eggs2;
    [SerializeField] private Sprite eggs3;
    [SerializeField] private Sprite eggs5;
    [SerializeField] private Sprite eggs10a;
    [SerializeField] private Sprite eggs10b;
    [Space]
    [SerializeField] private GameObject gainEffect;
    [SerializeField] private GameObject loseEffect;

    private void Start()
    {
        basket.OnChange += ChangeEggDisplay;
    }

    private void OnDestroy()
    {
        basket.OnChange -= ChangeEggDisplay;
    }

    private void ChangeEggDisplay(int change)
    {
        int eggAmount = basket.GetEggCount();

        if(eggAmount >= 100)
        {
            if(eggsDisplay.sprite != eggs10a)
            {
                eggsDisplay.sprite = eggs10a;
            }
            else
            {
                eggsDisplay.sprite = eggs10b;
            }
        }
        else if(eggAmount >= 50)
        {
            eggsDisplay.sprite = eggs5;
        }
        else if(eggAmount >= 30)
        {
            eggsDisplay.sprite = eggs3;
        }
        else if (eggAmount >= 20)
        {
            eggsDisplay.sprite = eggs2;
        }
        else if (eggAmount >= 1)
        {
            eggsDisplay.sprite = eggs1;
        }
        else
        {
            eggsDisplay.sprite = null;
        }

        StopAllCoroutines();
        gainEffect.SetActive(false);
        loseEffect.SetActive(false);
        if(change > 0)
        {
            StartCoroutine(TempEffect(gainEffect));
        }
        else if(change < 0)
        {
            StartCoroutine(TempEffect(loseEffect));
        }
    }

    private IEnumerator TempEffect(GameObject effect)
    {
        effect.SetActive(true);
        yield return new WaitForSeconds(1f);
        effect.SetActive(false);
    }
}
