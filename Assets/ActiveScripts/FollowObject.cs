using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObject : MonoBehaviour
{
    public GameObject obj;
    Transform t;
    public Vector3 x;
    private void Start()
    {
        t = obj.transform;
    }
    private void Update()
    {
        transform.position = t.position + x;
        transform.rotation = t.rotation;
    }
}
//SHA : Added the Vector x variable and added it to the position so the bar shows on the right of the cloud