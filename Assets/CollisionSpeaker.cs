using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionSpeaker : MonoBehaviour
{
    public GameObject listener;
    public int keyNumber;

    GameObject currentkey;

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(this.name + ":" + other.name);
        //Debug.Log(this.GetComponent<BoxCollider>().bounds.center);

        currentkey = other.transform.gameObject;
        other.transform.position = this.GetComponent<BoxCollider>().bounds.center;
        //other.transform.position = this.transform.TransformPoint(this.GetComponent<BoxCollider>().bounds.center) - new Vector3(0f,other.bounds.size.y,0f);
        listener.GetComponent<MastermindManager>().Receiver(other, keyNumber);

    }

    void OnTriggerStay(Collider other)
    {
        
        if (other.gameObject != currentkey)
        {
            //Debug.Log(other.name + " : " + currentkey.name);
            other.transform.position = this.GetComponent<BoxCollider>().bounds.center + new Vector3(0.0f, -2.0f, -2.0f);
        }
    }
}
