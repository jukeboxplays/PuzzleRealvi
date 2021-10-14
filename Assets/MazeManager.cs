using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MazeManager : MonoBehaviour
{
    public Transform cube;
    public Transform mainCamera;
    public Transform player;

    // Update is called once per frame
    void Update()
    {
        mainCamera.transform.position = player.transform.position + new Vector3(0f, 20f, 0f);

        Vector3 i = new Vector3(-Input.GetAxis("Vertical"), 0.0f, -Input.GetAxis("Horizontal"));
        cube.transform.Rotate(i * 10f * Time.deltaTime);

        //FUTURAMENTE FORMULAR MANEIRA DE TRAZER GameplayManager para o mazescene
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                //___________________________________________________________________
                if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Reset"))
                {
                    hit.transform.gameObject.GetComponent<ResetLevel>().LevelReset();
                }

            }
        }
    }

    public void EndMaze()
    {
        PlayerPrefs.SetInt("passLevel2", 1);
        //SceneManager.LoadScene("mainscene");
        SceneManager.UnloadSceneAsync("mazescene");
    }

}
