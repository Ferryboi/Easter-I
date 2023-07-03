using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransparentOnInvincible : MonoBehaviour
{
    [SerializeField] private PlayerHealth health;
    [SerializeField] private List<SpriteRenderer> renderers;
    [SerializeField] private float transparancy;
    private bool invincible;

    private void Start()
    {
        invincible = !health.IsInvincible;
    }

    private void LateUpdate()
    {
        if(invincible != health.IsInvincible)
        {
            invincible = health.IsInvincible;
            ToggleTransparancy(invincible);
        }
    }

    private void ToggleTransparancy(bool invincible)
    {
        float a = invincible == true ? transparancy : 1;

        for (int i = 0; i < renderers.Count; i++)
        {
            Color color = renderers[i].color;

            renderers[i].color = new Color(color.r, color.g, color.b, a);
        }
    }

}
