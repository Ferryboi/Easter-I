using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    [SerializeField] private Transform target;

    [SerializeField] private bool faceTarget = false;
    
    // Update is called once per frame
    void Update()
    {
        Vector3 offset = transform.position - target.position;
        if(offset.sqrMagnitude < 0.0001f)
        {
            transform.position = target.position;
            return;
        }
        
        if(faceTarget && transform.position != target.position)
        {
            transform.up = offset;
        }

        transform.position = Vector3.Lerp(transform.position, target.position, 0.025f);
    }
}
