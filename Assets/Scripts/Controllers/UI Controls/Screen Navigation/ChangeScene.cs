using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public string SceneName;
    public float Delay;

    [SerializeField] private bool changeOnStart = true;

    // Start is called before the first frame update
    void Start()
    {
        if (changeOnStart) { BeginChangeScene(); }
    }

    public void BeginChangeScene()
    {
        StartCoroutine(ChangeToScene());
    }

    private IEnumerator ChangeToScene()
    {
        yield return new WaitForSeconds(Delay);

        SceneManager.LoadScene(SceneName);
    }
}
