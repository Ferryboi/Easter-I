using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChangeRoundDuration : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI buttonText;

    private List<KeyValuePair<string, float>> roundDurations;

    private void Awake()
    {
        roundDurations = new List<KeyValuePair<string, float>>();

        roundDurations.Add(new KeyValuePair<string, float>("2 Minutes", 120));
        roundDurations.Add(new KeyValuePair<string, float>("3 Minutes", 180));
        roundDurations.Add(new KeyValuePair<string, float>("4 Minutes", 240));
        roundDurations.Add(new KeyValuePair<string, float>("5 Minutes", 300));
        roundDurations.Add(new KeyValuePair<string, float>("10 Minutes", 600));

        float durationTime = GameManager.Instance.GetLevelSettings().GetRoundDuration();
        for (int i = 0; i < roundDurations.Count; i++)
        {
            if (roundDurations[i].Value == durationTime)
            {
                GameManager.Instance.GetLevelSettings().SetRoundDuration(durationTime);
                buttonText.text = roundDurations[i].Key;
                return;
            }
        }
    }

    public void ToggleRoundDuration()
    {
        float durationTime = GameManager.Instance.GetLevelSettings().GetRoundDuration();
        for (int i = 0; i < roundDurations.Count; i++)
        {
            if (roundDurations[i].Value == durationTime)
            {
                int nextIndex = i + 1;
                if (nextIndex >= roundDurations.Count) nextIndex = 0;

                GameManager.Instance.GetLevelSettings().SetRoundDuration(roundDurations[nextIndex].Value);
                buttonText.text = roundDurations[nextIndex].Key;

                PlayerPrefs.SetFloat(PreferenceKeys.ROUND_DURATION, roundDurations[nextIndex].Value);
                return;
            }
        }
    }
}
