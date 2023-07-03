using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetachFromGameobject : MonoBehaviour
{
    private void Awake()
    {
        transform.parent = null;
    }
}
