using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeKey : MonoBehaviour
{
    public MazeManager mazeManager;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            mazeManager.EndMaze();   
        }  
    }

}
