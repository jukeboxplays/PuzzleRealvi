using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    public Transform timer;
    bool execute = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerPrefs.GetInt("passLevel2") == 1 && execute)
        {
            this.GetComponent<Animator>().enabled = true;
            timer.localPosition = new Vector3(-8f, 2f, 48f);
            timer.rotation = Quaternion.identity;

            execute = false;
        }
    }
}
