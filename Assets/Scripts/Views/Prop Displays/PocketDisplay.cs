using TMPro;
using UnityEngine;

public class PocketDisplay : MonoBehaviour
{
    [SerializeField] protected Pocket pocket;

    [Space]
    [SerializeField] protected TextMeshProUGUI eggCount;

    protected virtual void Start()
    {
        PopulateUI(0);

        pocket.OnChange += PopulateUI;
    }

    private void OnDestroy()
    {
        pocket.OnChange -= PopulateUI;
    }

    private void PopulateUI(int change)
    {
        if(pocket.GetEggCount() == 0)
        {
            eggCount.text = "";
            return;
        }

        eggCount.text = $"{pocket.GetEggCount()}";
    }
}
