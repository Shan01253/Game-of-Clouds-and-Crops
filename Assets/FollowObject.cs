using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObject : MonoBehaviour
{
    public GameObject obj;
    Transform t;
    private void Start()
    {
        t = obj.transform;
    }
    private void Update()
    {
        transform.position = t.position;
        transform.rotation = t.rotation;
    }
}
