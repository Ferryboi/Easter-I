using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DayNightCycle : MonoBehaviour
{
    [SerializeField] private Color morning;
    [SerializeField] private Color day;
    [SerializeField] private Color evening;
    [SerializeField] private Color night;

    private Tilemap[] tilemaps;
    private SpriteRenderer[] renderers;
    
    // Start is called before the first frame update
    void Start()
    {
        tilemaps = GetComponentsInChildren<Tilemap>();
        renderers = GetComponentsInChildren<SpriteRenderer>();

        StartCoroutine(Cycle());
    }

    private IEnumerator Cycle()
    {
        for(int i = 0; i < tilemaps.Length; i++)
        {
            tilemaps[i].color = morning;
        }

        for(int i = 0; i < renderers.Length; i++)
        {
            renderers[i].material.color = morning;
        }

        float interval = GameManager.Instance.GetLevelSettings().GetRoundDuration() / 3;

        yield return TransitionToColor(morning, day, interval);
        yield return TransitionToColor(day, evening, interval);
        yield return TransitionToColor(evening, night, interval);
    }

    private IEnumerator TransitionToColor(Color from, Color to, float duration)
    {
        for (float time = 0; time < duration; time += Time.deltaTime)
        {
            for (int i = 0; i < tilemaps.Length; i++)
            {
                tilemaps[i].color = Color.Lerp(from, to, time / duration);
            }

            for (int i = 0; i < renderers.Length; i++)
            {
                renderers[i].material.color = Color.Lerp(from, to, time / duration);
            }

            yield return 0;
        }
    }
}
