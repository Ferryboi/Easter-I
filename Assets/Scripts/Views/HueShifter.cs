using UnityEngine;

public class HueShifter : MonoBehaviour
{
    [SerializeField] private float speed = 1;
    private Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    void Update()
    {
        rend.material.SetColor("_Color", Color.HSVToRGB(Mathf.PingPong(Time.time * speed, 1), 1, 1));
    }
}
