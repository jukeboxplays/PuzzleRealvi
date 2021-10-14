using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = target.transform.position + new Vector3(0f, 20f, 0f);
    }
}
